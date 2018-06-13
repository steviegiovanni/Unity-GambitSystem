using System;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Utility class. Instantiate a
	/// </summary>
	static public class SkillEffectUtility{
		static public SkillEffectAsset CreateAssetOfType(string skillEffectAssetType){
			if (typeof(SkillEffectAsset).Name == skillEffectAssetType) {
				return new SkillEffectAsset ();
			} else if (typeof(TargetableSkillEffectAsset).Name == skillEffectAssetType) {
				return new TargetableSkillEffectAsset ();
			}
			return null;
		}
	}
}
