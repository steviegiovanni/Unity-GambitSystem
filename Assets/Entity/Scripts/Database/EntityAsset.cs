using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UtilitySystems.XmlDatabase;

namespace GameSystems.EntitySystem.Database{
	/// <summary>
	/// Gambit collection asset for entry on the collection database
	/// </summary>
	public class EntityAsset : XmlDatabaseAsset {
		public int GambitCollectionId{ get; set;}
		public int SkillCollectionId { get; set;}

		public EntityAsset():base(){GambitCollectionId = -1;SkillCollectionId = -1;}
		public EntityAsset(int id):base(id){GambitCollectionId = -1;SkillCollectionId = -1;}

		#region implemented abstract members of XmlDatabaseAsset
		public override void OnSaveAsset (XmlWriter writer)
		{
			writer.WriteStartElement ("GambitCollection");
			writer.SetAttr ("Id", GambitCollectionId);
			writer.WriteEndElement ();

			writer.WriteStartElement ("SkillCollection");
			writer.SetAttr ("Id", SkillCollectionId);
			writer.WriteEndElement ();
		}
		public override void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "SkillCollection":
				{
					SkillCollectionId = reader.GetAttrInt ("Id", -1);
				}
				break;
			case "GambitCollection":
				{
					GambitCollectionId = reader.GetAttrInt ("Id", -1);
				}
				break;
			default:
				{
					
				}
				break;
			}
		}
		#endregion
		
	}
}
