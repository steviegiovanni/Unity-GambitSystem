using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems{
	/// <summary>
	/// effect asset that affects stats of entities room wide
	/// </summary>
	public class StatGlobalEffectAsset : EffectAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}

		public override Effect CreateInstance(){
			return new StatGlobalEffect (this);
		}
	}
}
