namespace GameSystems.GambitSystem{
	public class StatGreaterThanGCAsset : StatGambitConditionAsset {
		public override GambitCondition CreateInstance(){
			return new StatGreaterThanGC (this);
		}
	}
}