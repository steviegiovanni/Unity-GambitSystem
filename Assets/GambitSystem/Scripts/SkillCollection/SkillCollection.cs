using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem.Database;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// the base class used to define a collection of skills
	/// </summary>
	public class SkillCollection : MonoBehaviour {
		[SerializeField]
		private int _skillCollectionId = -1;
		public int SkillCollectionId {
			get { return _skillCollectionId;}
			set{ _skillCollectionId = value;}
		}

		private bool _isCollectionSetup = false;
		public bool IsCollectionSetup{
			get { return _isCollectionSetup;}
			set{ _isCollectionSetup = value;}
		}

		private Dictionary<string, Skill> _skillDict;
		public Dictionary<string, Skill> SkillDict{
			get{ 
				if (_skillDict == null)
					_skillDict = new Dictionary<string, Skill> ();
				return _skillDict;
			}
		}

		public void SetupCollection(){
			var collection = SkillSystemDatabase.SkillCollections.Get (SkillCollectionId);
			if (collection != null) {
				SetupCollection (collection);	
			}
		}

		public void SetupCollection(SkillCollectionAsset collectionAsset){
			IsCollectionSetup = true;

			if (collectionAsset != null) {
				SkillDict.Clear ();

				// add all skills to the collection
				foreach (var skillAsset in collectionAsset.Skills) {
					Debug.Log ("adding skill " + skillAsset.Name);
					if (!SkillDict.ContainsKey (skillAsset.Name)) {
						SkillDict.Add (skillAsset.Name, skillAsset.CreateInstance ());
					} else {
						Debug.Log ("attempted to add a skill with the same name...");
					}
				}
			}
		}
			
		public bool ContainSkill(string skillName){
			return SkillDict.ContainsKey (skillName);
		}

		public Skill GetSkill(string skillName){
			if (ContainSkill (skillName)) {
				return SkillDict [skillName];
			}
			return null;
		}

		public T GetSkill<T>(string skillName) where T: Skill{
			return GetSkill (skillName) as T;
		}

		public bool TryGetSkill<T>(string skillName, out T skill) where T: Skill{
			skill = GetSkill<T> (skillName);
			return skill != null;
		}

		public void Awake(){
			SetupCollection ();
		}

		void Update(){
			foreach (var key in SkillDict.Keys) {
				SkillDict [key].CurrentCooldown = Mathf.Clamp (SkillDict [key].CurrentCooldown + Time.deltaTime, 0.0f, SkillDict [key].Cooldown);
			}
		}
	}
}
