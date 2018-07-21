namespace GameSystems.Effects{
	/// <summary>
	/// positional effect asset.
	/// </summary>
	public class PositionEffectAsset : EffectAsset {
		public override Effect CreateInstance(){
			return new PositionEffect (this);
		}
	}
}
