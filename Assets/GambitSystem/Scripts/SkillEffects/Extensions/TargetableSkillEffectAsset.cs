using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	public class TargetableSkillEffectAsset : SkillEffectAsset {
		public override SkillEffect CreateInstance(){
			return new TargetableSkillEffect (this);
		}
	}
}
