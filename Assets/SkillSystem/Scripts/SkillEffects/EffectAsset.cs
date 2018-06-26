using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Skill effect asset. Represents an xml entry on the skill collection database
	/// </summary>
	public class EffectAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public float Delay{ get; set;}

		public virtual Effect CreateInstance(){
			return new Effect (this);
		}

		#region IXmlOnSaveAsset implementation

		public virtual void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("Delay", Delay);
		}

		#endregion

		#region IXmlOnLoadAsset implementation

		public virtual void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Effect":
				{
					Delay = reader.GetAttrFloat ("Delay", 0.0f);
				}
				break;
			}
		}

		#endregion
	}
}
