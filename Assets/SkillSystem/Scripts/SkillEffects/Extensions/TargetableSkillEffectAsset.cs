using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable skill effect asset.
	/// </summary>
	public class TargetableSkillEffectAsset : SkillEffectAsset {
		public override SkillEffect CreateInstance(){
			return new TargetableSkillEffect (this);
		}
	}
}
