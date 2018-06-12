using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;

namespace GameSystems.SkillSystem{
	public class GambitAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public int Priority { get; set;}
		public SkillAsset Skill{ get; set;}
		public string SkillId{ get; set;}

		public virtual Gambit CreateInstance(){
			return new Gambit (this);
		}

		#region IXmlOnLoadAsset implementation

		public virtual void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Gambit":
				{
					Priority = reader.GetAttrInt ("Priority", 0);
					SkillId = reader.GetAttrString ("Skill", "");
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
		public virtual void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("Priority", Priority);
			writer.SetAttr ("Skill", SkillId);
		}
		#endregion

		public GambitAsset():base(){SkillId = "";}
	}
}
