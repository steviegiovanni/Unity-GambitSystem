using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// Base class for a character gambit
	/// </summary>
	public class Gambit{
		/// <summary>
		/// The owner of the gambit
		/// </summary>
		private GameObject _owner;
		public GameObject Owner{
			get{ return _owner;}
			set{ _owner = value;}
		}

		/// <summary>
		/// Execution priority of gambit
		/// </summary>
		private int _priority;
		public int Priority{
			get{ return _priority;}
			set{ _priority = value;}
		}

		/// <summary>
		/// skill id associated with this gambit
		/// </summary>
		private string _skillId;
		public string SkillId{
			get{ return _skillId;}
			set{ _skillId = value;}
		}

		/// <summary>
		/// skill associated with this gambit
		/// assigned during initialization
		/// </summary>
		private Skill _skill;
		public Skill Skill{
			get{ return _skill;}
			set{ _skill = value;}
		}

		/// <summary>
		/// whether the gambit is ready
		/// </summary>
		private bool _isReady = false;
		public bool IsReady{
			get{ return _isReady;}
			set{ _isReady = value;}
		}

		/// <summary>
		/// number of possible use of this gambit
		/// -1 means no limitation
		/// </summary>
		private int _maxUse = -1;
		public int MaxUse{
			get{ return _maxUse;}
			set{ _maxUse = value;}
		}

		/// <summary>
		/// number of usage of this gambit
		/// </summary>
		private int _usageNumber;
		public int UsageNumber{
			get{ return _usageNumber;}
			set{ _usageNumber = value;}
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="Gambit"/> class.
		/// </summary>
		public Gambit(){
			Priority = 0;
			SkillId = "";
			Owner = null;
			UsageNumber = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Gambit"/> class.
		/// </summary>
		public Gambit(GameObject owner, int priority){
			Owner = owner;
			Priority = priority;
			SkillId = "";
			UsageNumber = 0;
		}

		/// <summary>
		/// constructor that takes a gambit asset
		/// </summary>
		public Gambit(GambitAsset asset){
			Owner = null;
			Priority = asset.Priority;
			SkillId = asset.SkillId;
			MaxUse = asset.MaxUse;
			UsageNumber = 0;
		}

		/// <summary>
		/// Coroutine associated to this gambit when it's active
		/// </summary>
		public virtual IEnumerator GambitCoroutine(){
			if (Skill != null)
				yield return new WaitForSeconds (Skill.CastTime);
			else {
				Debug.LogWarning ("No skill associated with this gambit!");
				yield return null;
			}
		}

		/// <summary>
		/// Initialize this gambit. find the corresponding skill
		/// </summary>
		public void Initialize(){
			SkillCollection skillCollection = Owner.GetComponent<SkillCollection> ();
			if (skillCollection != null) {
				Skill = skillCollection.GetSkill<Skill> (SkillId);
				if (Skill == null) {
					Debug.LogWarning ("Can't find the skill");
				}
			} else {
				Debug.LogError ("No skill collection attached to the owner of this gambit!");
			}
		}
			
		/// <summary>
		/// Checks the conditions of the gambit. updates IsReady
		/// </summary>
		public void CheckConditions(){
			if ((MaxUse != -1) && (UsageNumber >= MaxUse)) {
				IsReady = false;
				return;
			}

			if (Skill == null) {
				IsReady = false;
				return;
			}

			if (Skill.CurrentCooldown < Skill.Cooldown) {
				IsReady = false;
				return;
			}

			IsReady = true;
		}
	}
}
