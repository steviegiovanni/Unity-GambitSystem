using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable effect
	/// </summary>
	public class TargetEffect : Effect{
		public TargetEffect():base(){}
		public TargetEffect(float delay):base(delay){}
		public TargetEffect(TargetEffectAsset asset):base(asset){}
	}
}
