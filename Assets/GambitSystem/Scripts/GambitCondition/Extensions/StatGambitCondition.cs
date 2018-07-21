namespace GameSystems.GambitSystem{
	/// <summary>
	/// condition where a specific stat is considered
	/// </summary>
	public class StatGambitCondition : GambitCondition {
		private string _statName;
		public string StatName{
			get{ return _statName;}
			set{ _statName = value;}
		}

		private float _statValue;
		public float StatValue{
			get{ return _statValue;}
			set{ _statValue = value;}
		}

		public StatGambitCondition (StatGambitConditionAsset asset) : base (asset){
			StatName = asset.StatName;
			StatValue = asset.StatValue;
		}
	}
}
