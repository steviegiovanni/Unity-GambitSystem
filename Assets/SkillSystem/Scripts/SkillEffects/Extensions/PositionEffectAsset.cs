using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// positional effect asset.
	/// </summary>
	public class PositionEffectAsset : EffectAsset {
		public override Effect CreateInstance(){
			return new PositionEffect (this);
		}
	}
}
