using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable skill asset, derived from based skill asset
	/// </summary>
	public class TargetableSkillAsset : SkillAsset {
		public override Skill CreateInstance(){
			return new TargetableSkill (this);
		}
	}
}
