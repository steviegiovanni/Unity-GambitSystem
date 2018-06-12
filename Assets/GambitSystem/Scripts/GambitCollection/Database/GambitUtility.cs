using System;
using UnityEngine;

namespace GameSystems.SkillSystem.Database{
	static public class GambitUtility{
		static public GambitAsset CreateAssetOfType(string gambitAssetType){
			if (typeof(GambitAsset).Name == gambitAssetType) {
				return new GambitAsset ();
			} else if (typeof(HighestEnmityGambitAsset).Name == gambitAssetType) {
				return new HighestEnmityGambitAsset ();
			}
			return null;
		}
	}
}