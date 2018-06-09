using System;
using UnityEngine;

namespace GameSystems.SkillSystem.Database{
	static public class SkillUtility{
		static public SkillAsset CreateAssetOfType(string skillAssetType){
			return new SkillAsset ();
		}
	}
}
