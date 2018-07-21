using System.Collections;
using System.Collections.Generic;
using GameSystems.Effects;
using UnityEngine;
using GameSystems.StatSystem;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// base class for Skill
	/// </summary>
	public class Skill: IHasEffects{
		#region IHasEffects implementation

		public GameObject GetOwner ()
		{
			return Owner;
		}

		#endregion

		/// <summary>
		/// The owner using the skill
		/// </summary>
		private GameObject _owner;
		public GameObject Owner{
			get{ return _owner;}
			set{ _owner = value;}
		}

		/// <summary>
		/// the name of the skill
		/// </summary>
		private string _name;
		public string Name{
			get{ return _name;}
			set{ _name = value;}
		}

		/// <summary>
		/// skill cooldown
		/// </summary>
		private float _cooldown;
		public float Cooldown{
			get{ return _cooldown; }
			set{ _cooldown = value; }
		}

		/// <summary>
		/// current cooldown of this skill.
		/// incremented by delta time every update by the skillcollection
		/// </summary>
		private float _currentCooldown;
		public float CurrentCooldown{
			get{ return _currentCooldown;}
			set{ _currentCooldown = value;}
		}

		/// <summary>
		/// The cast time for using the skill
		/// </summary>
		public float _castTime;
		public float CastTime{
			get{ return _castTime;}
			set{ _castTime = value;}
		}

		/// <summary>
		/// Total duration the skill is running
		/// </summary>
		public float _delay;
		public float Delay{
			get{ return _delay;}
			set{ _delay = value;}
		}

		/// <summary>
		/// whether the skill can be interrupted
		/// </summary>
		public bool _interruptable;
		public bool Interruptable{
			get{ return _interruptable;}
			set{ _interruptable = value;}
		}

		/// <summary>
		/// The range.
		/// </summary>
		private float _range = 0.5f;
		public float Range{
			get{ return _range;}
			set{ _range = value;}
		}

		/// <summary>
		/// the level required to unlock this skill
		/// </summary>
		private int _requiredLevel = 0;
		public int RequiredLevel{
			get{ return _requiredLevel;}
			set{ _requiredLevel = value;}
		}

		/// <summary>
		/// The effects.
		/// </summary>
		private List<Effect> _effects;
		public List<Effect> Effects{
			get{ 
				if (_effects == null)
					_effects = new List<Effect> ();
				return _effects;
			}
		}

		/// <summary>
		/// list of prerequisites to be able to use the skill
		/// </summary>
		private List<SkillPrerequisite> _prerequisites;
		public List<SkillPrerequisite> Prerequisites{
			get{
				if (_prerequisites == null)
					_prerequisites = new List<SkillPrerequisite> ();
				return _prerequisites;
			}
		}
			
		/// <summary>
		/// constructor with skill asset as input
		/// </summary>
		public Skill(SkillAsset skillAsset){
			Owner = null;
			Name = skillAsset.Name;
			Cooldown = skillAsset.Cooldown;
			Interruptable = skillAsset.Interruptable;
			CastTime = skillAsset.CastTime;
			Range = skillAsset.Range;
			Delay = skillAsset.Delay;
			CurrentCooldown = Cooldown;
			RequiredLevel = skillAsset.RequiredLevel;

			foreach (var prerequisite in skillAsset.Prerequisites) {
				this.Prerequisites.Add (prerequisite.CreateInstance ());
			}

			foreach (var effect in skillAsset.Effects) {
				this.Effects.Add (effect.CreateInstance ());
				this.Effects [this.Effects.Count - 1].Source = this;
			}
			Effects.Sort ((e1, e2) => e1.Delay.CompareTo (e2.Delay));
		}

		/// <summary>
		/// Skill coroutine.
		/// go through all skill effects and apply the effect at the appropriate time.
		/// </summary>
		public IEnumerator SkillCoroutine(){
			if (!IsValid ())
				yield return null;

			// apply all prerequisites
			IHasStats statOwner = Owner.GetComponent<IHasStats> ();
			if (statOwner == null)
				yield return null;
			for (int i = 0; i < Prerequisites.Count; i++) {
				Prerequisites [i].ApplyPrerequisite (statOwner);
			}

			Debug.Log ("Using " + Name);
			// apply all effects
			float startTime = Time.time;
			float curDelay = 0.0f;
			for (int i = 0; i < Effects.Count; i++) {
				yield return new WaitForSeconds (Effects [i].Delay - curDelay);
				Effects [i].ApplyEffect ();
				curDelay = Effects[i].Delay;
			}

			yield return new WaitForSeconds(Delay - (Time.time - startTime));
		}

		/// <summary>
		/// check whether skill is valid or not (has target, etc.) override this function for each skill extension
		/// </summary>
		public virtual bool IsValid(){
			return true;
		}

		/// <summary>
		/// check if all prerequisites to use the skill is met
		/// </summary>
		public bool IsPrerequisitesMet(){
			IHasStats statOwner = Owner.GetComponent<IHasStats> ();
			if (statOwner == null)
				return false;

			bool retval = true;
			foreach (var prereq in Prerequisites) {
				retval = retval && prereq.CheckPrerequisite (statOwner);
				if (!retval)
					break;
			}

			return retval;
		}
	}
}