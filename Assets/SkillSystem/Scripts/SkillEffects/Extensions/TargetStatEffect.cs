using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable effect
	/// </summary>
	public class TargetStatEffect : TargetEffect{
		public TargetStatEffect():base(){}
		public TargetStatEffect(float delay):base(delay){}
		public TargetStatEffect(TargetStatEffectAsset asset):base(asset){}

		public override void ApplyEffect ()
		{
			Debug.Log ("applying target specific stat effect");
		}
	}
}
