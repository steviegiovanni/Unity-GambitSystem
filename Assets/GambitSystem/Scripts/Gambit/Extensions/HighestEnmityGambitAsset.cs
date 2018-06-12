using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	public class HighestEnmityGambitAsset : TargetGambitAsset {
		public override Gambit CreateInstance(){
			return new HighestEnmityGambit (this);
		}
	}
}