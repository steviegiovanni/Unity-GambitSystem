using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems{
	/// <summary>
	/// Targetable effect asset.
	/// </summary>
	public class TargetAOEStatEffectAsset : TargetStatEffectAsset {
		public int TargetType { get; set;}
		public bool IncludeTarget { get; set;}
		public float Radius{get; set;}

		public override Effect CreateInstance(){
			return new TargetAOEStatEffect (this);
		}
	}
}
