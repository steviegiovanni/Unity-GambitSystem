using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems{
	/// <summary>
	/// Targetable effect asset.
	/// </summary>
	public class TargetEffectAsset : EffectAsset {
		public override Effect CreateInstance(){
			return new TargetEffect (this);
		}
	}
}
