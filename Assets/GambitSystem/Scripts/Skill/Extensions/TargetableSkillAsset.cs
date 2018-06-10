using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	public class TargetableSkillAsset : SkillAsset {
		public override Skill CreateInstance(){
			return new TargetableSkill (this);
		}
	}
}
