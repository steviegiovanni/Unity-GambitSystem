﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;
using GameSystems.GambitSystem.Database;
using GameSystems.PerceptionSystem;
using GameSystems.Movement;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// monobehaviour that contains the list of gambits
	/// </summary>
	public class GambitCollection : MonoBehaviour {
		/// <summary>
		/// gambit collection id
		/// </summary>
		[SerializeField]
		private int _gambitCollectionId = -1;
		public int GambitCollectionId {
			get { return _gambitCollectionId;}
			set{ _gambitCollectionId = value;}
		}

		/// <summary>
		/// skill collection monobehaviour
		/// </summary>
		[SerializeField]
		private SkillCollection _skillCollection;
		public SkillCollection SkillCollection{
			get{ 
				if (_skillCollection == null)
					_skillCollection = GetComponent<SkillCollection> ();
				if (_skillCollection == null) {
					Debug.LogError ("No skill collection component found!");
				}
				return _skillCollection;
			}
		}

		/// <summary>
		/// flag that determines whether a gambit is running
		/// </summary>
		private bool _isGambitRunning = false;
		public bool IsGambitRunning{
			get{ return _isGambitRunning;}
			set{_isGambitRunning = value; }
		}

		/// <summary>
		/// flag whether the collection is setup
		/// </summary>
		private bool _isCollectionSetup = false;
		public bool IsCollectionSetup{
			get { return _isCollectionSetup;}
			set{ _isCollectionSetup = value;}
		}

		/// <summary>
		/// setup the collection, will check whether the skill collection id is
		/// the same as it is assigned in the skill collection component
		/// if not, reassign skill collection id and setup skill collection
		/// </summary>
		public void SetupCollection(){
			var collection = SystemDatabase.GambitCollections.Get (GambitCollectionId);
			if (collection != null) {
				if (collection.SkillCollectionId != SkillCollection.SkillCollectionId) 
					Debug.LogError ("Non matching skill collection id!");
				else
					SetupCollection (collection);	
			}
		}

		/// <summary>
		/// setup the collection based on a collection asset
		/// </summary>
		public void SetupCollection(GambitCollectionAsset collectionAsset){
			IsCollectionSetup = true;

			if (collectionAsset != null) {
				Gambits.Clear ();
				foreach (var gambitAsset in collectionAsset.Gambits) {
					Gambits.Add (gambitAsset.CreateInstance ());
					Gambits [Gambits.Count - 1].Owner = this.gameObject;
					Gambits [Gambits.Count - 1].Initialize ();

					foreach (var gambitCondition in gambitAsset.Conditions) {
						Gambits [Gambits.Count - 1].Conditions.Add (gambitCondition.CreateInstance ());
						Gambits [Gambits.Count - 1].Conditions [Gambits [Gambits.Count - 1].Conditions.Count - 1].Owner = this.gameObject;
					}
				}
			}
		}

		/// <summary>
		/// the collection of gambits
		/// </summary>
		private List<Gambit> _gambits;
		public List<Gambit> Gambits{
			get{
				if (_gambits == null) {
					_gambits = new List<Gambit> ();
				}
				return _gambits;
			}
		}

		/// <summary>
		/// The active gambit identifier.
		/// </summary>
		private int _activeGambitId = -1;
		public int ActiveGambitId{
			get{ return _activeGambitId;}
			set{ _activeGambitId = value;}
		}

		/// <summary>
		/// perception component
		/// </summary>
		private Perception _perception;
		public Perception Perception{
			get{
				_perception = GetComponent<Perception> ();
				if(_perception == null)
					_perception = this.gameObject.AddComponent<Perception>();
				return _perception;
			}
			set{
				_perception = value;
			}
		}

		// Use this for initialization
		void Awake () {
			SetupCollection();
		}
			
		// Update is called once per frame
		public virtual void Update () {
			int potentialGambit = FindPotentialGambit (); // find potential runnable gambit

			if ((potentialGambit == -1) && !IsGambitRunning) { // no runnable gambit
				StopAllCoroutines();
				ActiveGambitId = -1;
				return;
			}

			// if runnable gambit has higher priority than active gambit, and not locked, switch active gambit
			if (((ActiveGambitId != potentialGambit) && !IsGambitRunning) 
				|| ((ActiveGambitId < potentialGambit) && (Gambits[ActiveGambitId].Skill != null) && Gambits[ActiveGambitId].Skill.Interruptable)) {
				StopAllCoroutines();
				ActiveGambitId = potentialGambit;
				StartCoroutine(RunGambit(ActiveGambitId));
			}
		}

		/// <summary>
		/// run the appropriate gambit and run the skill coroutine associated with the gambit
		/// </summary>
		public IEnumerator RunGambit(int gambitId){
			while (true) {
				IsGambitRunning = true;
				//Gambits [ActiveGambitId].UsageNumber++;
				yield return StartCoroutine (Gambits [ActiveGambitId].GambitCoroutine ());
				Skill skillToExecute = Gambits [ActiveGambitId].Skill;
				if (Gambits [ActiveGambitId].Skill != null) {
					skillToExecute.CurrentCooldown = 0.0f;
					yield return StartCoroutine (skillToExecute.SkillCoroutine ());
				}
				Gambits [ActiveGambitId].UsageNumber++;
				IsGambitRunning = false;
				yield return null;
			}
		}

		/// <summary>
		/// Finds the potential gambit.
		/// </summary>
		public int FindPotentialGambit(){
			// find the highest priority runnable gambit
			int highestPriority = 0;
			int highestIndex = -1; // case where there's no runnable gambit
			for (int i = 0; i < Gambits.Count; i++) {
				Gambits [i].CheckConditions ();
				if ((Gambits [i].Priority >= highestPriority)
					&& Gambits[i].IsReady) {
					highestIndex = i;
					highestPriority = Gambits [i].Priority;
				}
			}
			//Debug.Log ("Highest Index: "+highestIndex);
			return highestIndex;
		}

		/// <summary>
		/// when the gambit collection is disabled, make sure to stop all running coroutine
		/// also IsGambitRunning should be set back to false
		/// </summary>
		void OnDisable(){
			StopAllCoroutines ();
			IsGambitRunning = false;
			IMovable owner = GetComponent<IMovable> ();
			if (owner != null)
				owner.Stopped = true;
		}

		/// <summary>
		/// when the gambit is reenabled, refind potential gambit
		/// runs anything that is possible
		/// </summary>
		void OnEnable(){
			ActiveGambitId = FindPotentialGambit ();
			if(ActiveGambitId != -1)
				StartCoroutine (RunGambit(ActiveGambitId));
		}
	}
}