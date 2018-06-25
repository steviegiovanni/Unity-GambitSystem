using System;
using UnityEngine;

namespace GameSystems.SkillSystem.Database{
	/// <summary>
	/// utility class. instantiate a new skill asset for the database according to asset type name
	/// </summary>
	static public class SkillUtility{
		static public SkillAsset CreateAssetOfType(string skillAssetType){
			if (typeof(SkillAsset).Name == skillAssetType) {
				return new SkillAsset ();
			} else if (typeof(TargetableSkillAsset).Name == skillAssetType) {
				return new TargetableSkillAsset ();
			}else if (typeof(PositionSkillAsset).Name == skillAssetType) {
				return new PositionSkillAsset ();
			}
			return null;
		}
	}
}
