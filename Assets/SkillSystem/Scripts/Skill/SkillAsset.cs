using System.Collections.Generic;
using System.Xml;
using GameSystems.Effects;
using UtilitySystems.XmlDatabase;

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
		public List<SkillPrerequisiteAsset> Prerequisites{ get; private set;}

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
					string skillEffectAssetType = reader.GetAttrString ("AssetType", "");
					var asset = EffectUtility.CreateAssetOfType(skillEffectAssetType);
					if (asset != null) {
						Effects.Add (asset);
						Effects[Effects.Count-1].OnLoadAsset(reader);
					}
				}
				break;
			case "Prerequisite":
				{
					var asset = new SkillPrerequisiteAsset ();
					Prerequisites.Add (asset);
					Prerequisites [Prerequisites.Count - 1].OnLoadAsset (reader);
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
			foreach (var prerequisite in Prerequisites) {
				writer.WriteStartElement ("Prerequisite");
				prerequisite.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
			foreach(var effect in Effects){
				writer.WriteStartElement ("Effect");
				writer.SetAttr ("AssetType", effect.GetType ().Name);
				effect.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}

		#endregion

		public SkillAsset():base(){
			Effects = new List<EffectAsset> ();
			Prerequisites = new List<SkillPrerequisiteAsset> ();
		}
	}
}
