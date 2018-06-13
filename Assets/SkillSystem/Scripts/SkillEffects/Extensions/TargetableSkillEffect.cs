using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable skill effect
	/// </summary>
	public class TargetableSkillEffect : SkillEffect, ITargetableEffect {
		/// <summary>
		/// the target of the effect
		/// </summary>
		private GameObject _target;

		#region ITargetableEffect implementation

		public GameObject Target {
			get {return _target;}
			set {_target = value;}
		}

		#endregion

		public TargetableSkillEffect():base(){}
		public TargetableSkillEffect(float delay):base(delay){}
		public TargetableSkillEffect(TargetableSkillEffectAsset asset):base(asset){}
	}
}
