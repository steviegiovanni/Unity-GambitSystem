using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// base class for skill asset entry in a skill collection asset
	/// </summary>
	public class SkillAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public string Name { get; set;}
		public float Cooldown{ get; set;}
		public float CastTime{ get; set;}
		public float Range{ get; set;}
		public float Delay{ get; set;}
		public bool Interruptable{ get; set;}
		public int RequiredLevel{ get; set;}
		public List<EffectAsset> Effects{ get; private set;}

		public virtual Skill CreateInstance(){
			return new Skill (this);
		}

		#region IXmlOnLoadAsset implementation

		public virtual void OnLoadAsset (XmlReader reader)
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
					RequiredLevel = reader.GetAttrInt ("Level", 0);
				}
				break;
			case "Effect":
				{
					// get the skill effect type
					string skillEffectAssetType = reader.GetAttrString ("AssetType", "");

					// create an instance of the skill asset
					var asset = SkillEffectUtility.CreateAssetOfType(skillEffectAssetType);
					if (asset != null) {
						Effects.Add (asset);
						// initialize the value
						Effects[Effects.Count-1].OnLoadAsset(reader);
					}
				}
				break;
			default:
				{
					if (Effects.Count > 0)
						Effects [Effects.Count - 1].OnLoadAsset (reader);
				}
				break;
			}
		}

		#endregion

		#region IXmlOnSaveAsset implementation

		public virtual void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("Name", Name);
			writer.SetAttr ("Cooldown", Cooldown);
			writer.SetAttr ("CastTime", CastTime);
			writer.SetAttr ("Range", Range);
			writer.SetAttr ("Delay", Delay);
			writer.SetAttr ("Interruptable", Interruptable);
			writer.SetAttr ("Level", RequiredLevel);
			foreach(var effect in Effects){
				writer.WriteStartElement ("Effect");
				writer.SetAttr ("AssetType", effect.GetType ().Name);
				effect.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}

		#endregion

		public SkillAsset():base(){Effects = new List<EffectAsset> ();}
	}
}
