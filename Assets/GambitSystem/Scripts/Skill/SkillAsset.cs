using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	public class SkillAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public string Name { get; set;}
		public float Cooldown{ get; set;}
		public float CastTime{ get; set;}
		public float Range{ get; set;}
		public float Delay{ get; set;}
		public bool Interruptable{ get; set;}

		public virtual Skill CreateInstance(){
			return new Skill (this);
		}


		#region IXmlOnLoadAsset implementation

		public void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Skill":
				{
					Name = reader.GetAttrString ("Name",string.Empty);
					Cooldown = reader.GetAttrFloat ("Cooldown",0.0f);
					CastTime = reader.GetAttrFloat ("Casttime",0.0f);
					Range = reader.GetAttrFloat ("Range",0.0f);
					Delay = reader.GetAttrFloat ("Delay",0.0f);
					Interruptable = reader.GetBoolAttribute ("Interruptable",false);
				}
				break;
			}
		}

		#endregion

		#region IXmlOnSaveAsset implementation

		public void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("Name", Name);
			writer.SetAttr ("Cooldown", Cooldown);
			writer.SetAttr ("CastTime", CastTime);
			writer.SetAttr ("Range", Range);
			writer.SetAttr ("Delay", Delay);
			writer.SetAttr ("Interruptable", Interruptable);
		}

		#endregion


	}
}
