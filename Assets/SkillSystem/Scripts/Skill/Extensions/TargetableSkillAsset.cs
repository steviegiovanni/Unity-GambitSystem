using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Targetable skill asset, derived from based skill asset
	/// </summary>
	public class TargetableSkillAsset : SkillAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}

		public override Skill CreateInstance(){
			return new TargetableSkill (this);
		}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Skill":
				{
					TargetType = reader.GetAttrInt ("TargetType", 0);
					IncludeSelf = reader.GetBoolAttribute("IncludeSelf", false);
				}
				break;
			default:
				{
				}
				break;
			}
		}

		#endregion

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("TargetType", TargetType);
			writer.SetAttr ("IncludeSelf", IncludeSelf);

			base.OnSaveAsset (writer);
		}

		#endregion
	}
}
