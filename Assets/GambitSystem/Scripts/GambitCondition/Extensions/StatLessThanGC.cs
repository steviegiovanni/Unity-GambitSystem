namespace GameSystems.GambitSystem{
	/// <summary>
	/// condition where a specific stat is less than some value
	/// </summary>
	public class StatLessThanGC : StatGambitCondition {
		public StatLessThanGC (StatLessThanGCAsset asset) : base (asset){}

		public override bool GetStatus(){
			if (Owner == null)
				return false;

			IHasStats ownerStat = Owner.GetComponent<IHasStats> ();
			float curValue;
			if (!ownerStat.TryGetStatPercentValue (StatName, out curValue))
				return false;
			else
				return curValue < StatValue;
		}
	}
}
