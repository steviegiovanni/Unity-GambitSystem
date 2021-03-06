﻿namespace GameSystems.GambitSystem.Database{
	/// <summary>
	/// utility class to instantiate the corresponding gambit asset when needed
	/// </summary>
	static public class GambitUtility{
		static public GambitAsset CreateAssetOfType(string gambitAssetType){
			if (typeof(GambitAsset).Name == gambitAssetType) {
				return new GambitAsset ();
			} else if (typeof(HighestEnmityGambitAsset).Name == gambitAssetType) {
				return new HighestEnmityGambitAsset ();
			} else if (typeof(PositionGambitAsset).Name == gambitAssetType) {
				return new PositionGambitAsset ();
			}
			return null;
		}
	}
}