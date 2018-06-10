using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem.Database{
	public class SkillCollectionAsset : XmlDatabaseAsset {
		public List<SkillAsset> Skills{ get; private set;}

		public SkillCollectionAsset(): base(){Skills = new List<SkillAsset> ();}
		public SkillCollectionAsset(int id): base(id){Skills = new List<SkillAsset> ();}

		#region implemented abstract members of XmlDatabaseAsset
		public override void OnSaveAsset (XmlWriter writer)
		{
			foreach (var skill in Skills) {
				writer.WriteStartElement ("Skill");
				writer.SetAttr ("AssetType", skill.GetType ().Name);
				skill.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}
		public override void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Skill":
				{
					// get the skill type
					string skillAssetType = reader.GetAttrString ("AssetType", "");

					// create an instance of the skill asset
					var asset = SkillUtility.CreateAssetOfType(skillAssetType);
					if (asset != null) {
						Skills.Add (asset);
						// initialize the value
						Skills[Skills.Count-1].OnLoadAsset(reader);
					}
				}
				break;
			default:
				{
					if (Skills.Count > 0) {
						// element is not handled, pass to skill asset loader (most recent one)
						Skills [Skills.Count - 1].OnLoadAsset (reader);
					}
				}
				break;
			}
		}
		#endregion
	}
}
