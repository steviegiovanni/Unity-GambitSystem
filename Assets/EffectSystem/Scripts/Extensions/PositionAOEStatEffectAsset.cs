using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems{
	/// <summary>
	/// positional effect asset.
	/// </summary>
	public class PositionAOEStatEffectAsset : PositionEffectAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}
		public float Radius{get; set;}

		public override Effect CreateInstance(){
			return new PositionAOEStatEffect (this);
		}
	}
}
