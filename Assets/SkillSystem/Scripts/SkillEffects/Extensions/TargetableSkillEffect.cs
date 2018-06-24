using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable skill effect
	/// </summary>
	public class TargetableSkillEffect : SkillEffect{
		public TargetableSkillEffect():base(){}
		public TargetableSkillEffect(float delay):base(delay){}
		public TargetableSkillEffect(TargetableSkillEffectAsset asset):base(asset){}
	}
}
