using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// a skill that has a target associated to it
	/// </summary>
	public class TargetableSkill : Skill, ITargetableSkill {
		/// <summary>
		/// the target of the skill
		/// </summary>
		private GameObject _target;

		#region IHasTarget implementation

		public GameObject Target {
			get {return _target;}
			set {_target = value;}
		}

		#endregion

		public TargetableSkill(GameObject owner, string name, float cooldown, bool interruptable, float castTime, float range, float delay):base(owner,name,cooldown,interruptable,castTime,range,delay){}

		/// <summary>
		/// constructor with skill asset as input
		/// </summary>
		public TargetableSkill(TargetableSkillAsset skillAsset): base(skillAsset){
		}
	}
}
