using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// a skill that has a target associated to it
	/// </summary>
	public class TargetableSkill : Skill, ITargetableSkill {
		/// <summary>
		/// include self when finding a target
		/// </summary>
		private bool _includeSelf;
		public bool IncludeSelf{
			get{ return _includeSelf;}
			set{ _includeSelf = value;}
		}

		/// <summary>
		/// The type of the target.
		/// </summary>
		private int _targetType;
		public int TargetType{
			get{ return _targetType;}
			set{ _targetType = value;}
		}

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
			IncludeSelf = skillAsset.IncludeSelf;
			TargetType = skillAsset.TargetType;
		}
	}
}
