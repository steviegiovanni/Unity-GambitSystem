using System;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Utility class. Instantiate a
	/// </summary>
	static public class SkillEffectUtility{
		static public EffectAsset CreateAssetOfType(string skillEffectAssetType){
			if (typeof(EffectAsset).Name == skillEffectAssetType) {
				return new EffectAsset ();
			} else if (typeof(TargetEffectAsset).Name == skillEffectAssetType) {
				return new TargetEffectAsset ();
			} else if (typeof(PositionEffectAsset).Name == skillEffectAssetType) {
				return new PositionEffectAsset ();
			} else if (typeof(StatGlobalEffectAsset).Name == skillEffectAssetType) {
				return new StatGlobalEffectAsset ();
			} 
			return null;
		}
	}
}
