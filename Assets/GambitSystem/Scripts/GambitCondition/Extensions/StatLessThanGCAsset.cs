namespace GameSystems.GambitSystem{
	public class StatLessThanGCAsset : StatGambitConditionAsset {
		public override GambitCondition CreateInstance(){
			return new StatLessThanGC (this);
		}
	}
}