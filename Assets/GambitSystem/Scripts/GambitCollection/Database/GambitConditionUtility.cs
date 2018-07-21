namespace GameSystems.GambitSystem{
	/// <summary>
	/// utility class to instantiate the corresponding gambit conditionasset when needed
	/// </summary>
	static public class GambitConditionUtility{
		static public GambitConditionAsset CreateAssetOfType(string gambitConditionAssetType){
			if (typeof(GambitConditionAsset).Name == gambitConditionAssetType) {
				return new GambitConditionAsset ();
			}else if(typeof(StatLessThanGCAsset).Name == gambitConditionAssetType){
				return new StatLessThanGCAsset ();
			}else if(typeof(StatGreaterThanGCAsset).Name == gambitConditionAssetType){
				return new StatGreaterThanGCAsset ();
			}
			return null;
		}
	}
}