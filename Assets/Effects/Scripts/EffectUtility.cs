using System;
using UnityEngine;

namespace GameSystems.Effects{
	/// <summary>
	/// Utility class. Instantiate a
	/// </summary>
	static public class EffectUtility{
		static public EffectAsset CreateAssetOfType(string skillEffectAssetType){
			if (typeof(EffectAsset).Name == skillEffectAssetType) {
				return new EffectAsset ();
			} else if (typeof(TargetEffectAsset).Name == skillEffectAssetType) {
				return new TargetEffectAsset ();
			} else if (typeof(PositionEffectAsset).Name == skillEffectAssetType) {
				return new PositionEffectAsset ();
			} else if (typeof(StatGlobalEffectAsset).Name == skillEffectAssetType) {
				return new StatGlobalEffectAsset ();
			} else if (typeof(TargetStatEffectAsset).Name == skillEffectAssetType) {
				return new TargetStatEffectAsset ();
			} else if (typeof(TargetAOEStatEffectAsset).Name == skillEffectAssetType) {
				return new TargetAOEStatEffectAsset ();
			}else if (typeof(PositionAOEStatEffectAsset).Name == skillEffectAssetType) {
					return new PositionAOEStatEffectAsset ();
			} else if (typeof(TargetPrefabEffectAsset).Name == skillEffectAssetType) {
				return new TargetPrefabEffectAsset ();
			} else if (typeof(PositionPrefabEffectAsset).Name == skillEffectAssetType) {
				return new PositionPrefabEffectAsset ();
			} 
			return null;
		}
	}
}
