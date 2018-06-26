using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable effect asset.
	/// </summary>
	public class TargetStatEffectAsset : TargetEffectAsset {
		public override Effect CreateInstance(){
			return new TargetStatEffect (this);
		}
	}
}
