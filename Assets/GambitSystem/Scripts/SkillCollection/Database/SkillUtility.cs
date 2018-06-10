using System;
using UnityEngine;

namespace GameSystems.SkillSystem.Database{
	static public class SkillUtility{
		static public SkillAsset CreateAssetOfType(string skillAssetType){
			if (typeof(SkillAsset).Name == skillAssetType) {
				return new SkillAsset ();
			} else if (typeof(TargetableSkillAsset).Name == skillAssetType) {
				return new TargetableSkillAsset ();
			}
			return null;
		}
	}
}
