namespace GameSystems.Effects{
	/// <summary>
	/// Targetable effect asset.
	/// </summary>
	public class TargetEffectAsset : EffectAsset {
		public override Effect CreateInstance(){
			return new TargetEffect (this);
		}
	}
}
