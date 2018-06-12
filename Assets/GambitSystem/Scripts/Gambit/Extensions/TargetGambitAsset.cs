using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	public class TargetGambitAsset : GambitAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}

		public override Gambit CreateInstance(){
			return new TargetGambit (this);
		}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Gambit":
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
			base.OnSaveAsset (writer);
			writer.SetAttr ("TargetType", TargetType);
			writer.SetAttr ("IncludeSelf", IncludeSelf);
		}
		#endregion
	}
}
