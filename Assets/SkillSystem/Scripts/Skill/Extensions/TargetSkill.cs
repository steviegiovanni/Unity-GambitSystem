using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// a skill that has a target associated to it
	/// </summary>
	public class TargetSkill : Skill, IHasTargetEffects {
		#region IHasTargetEffects implementation

		public GameObject GetTarget ()
		{
			return Target;
		}

		#endregion

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
		public GameObject Target {
			get {return _target;}
			set {_target = value;}
		}

		/// <summary>
		/// constructor with skill asset as input
		/// </summary>
		public TargetSkill(TargetSkillAsset skillAsset): base(skillAsset){
			IncludeSelf = skillAsset.IncludeSelf;
			TargetType = skillAsset.TargetType;
		}

		public override bool IsValid(){
			return Target == null;
		}
	}
}
