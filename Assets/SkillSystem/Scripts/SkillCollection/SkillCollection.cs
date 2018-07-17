using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem.Database;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// the base class used to define a collection of skills
	/// </summary>
	public class SkillCollection : MonoBehaviour {
		/// <summary>
		/// the collection id
		/// </summary>
		[SerializeField]
		private int _skillCollectionId = -1;
		public int SkillCollectionId {
			get { return _skillCollectionId;}
			set{ _skillCollectionId = value;}
		}

		/// <summary>
		/// status whether the collection is setup
		/// </summary>
		private bool _isCollectionSetup = false;
		public bool IsCollectionSetup{
			get { return _isCollectionSetup;}
			set{ _isCollectionSetup = value;}
		}

		/// <summary>
		/// the skill dictionary. key is skill name
		/// </summary>
		private Dictionary<string, Skill> _skillDict;
		public Dictionary<string, Skill> SkillDict{
			get{ 
				if (_skillDict == null)
					_skillDict = new Dictionary<string, Skill> ();
				return _skillDict;
			}
		}

		/// <summary>
		/// Setups the collection
		/// </summary>
		public void SetupCollection(){
			var collection = SystemDatabase.SkillCollections.Get (SkillCollectionId);
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
					if (!SkillDict.ContainsKey (skillAsset.Name)) {
						Skill newSkill = skillAsset.CreateInstance ();
						newSkill.Owner = this.gameObject;
						SkillDict.Add (skillAsset.Name, newSkill);
					} else {
						Debug.Log ("attempted to add a skill with the same name...");
					}
				}
			}
		}

		/// <summary>
		/// Check whether the dictionary contains the specified skill
		/// </summary>
		public bool ContainSkill(string skillName){
			return SkillDict.ContainsKey (skillName);
		}

		/// <summary>
		/// Gets the skill form the skill dictionary
		/// </summary>
		public Skill GetSkill(string skillName){
			if (ContainSkill (skillName)) {
				return SkillDict [skillName];
			}
			return null;
		}

		/// <summary>
		/// generic version of getskill
		/// </summary>
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

		/// <summary>
		/// every update the skill collection updates the current cooldown of each skill
		/// </summary>
		void Update(){
			foreach (var key in SkillDict.Keys) {
				SkillDict [key].CurrentCooldown = Mathf.Clamp (SkillDict [key].CurrentCooldown + Time.deltaTime, 0.0f, SkillDict [key].Cooldown);
			}
		}
	}
}
