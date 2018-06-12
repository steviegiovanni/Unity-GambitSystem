using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem.Database{
	public class GambitCollectionAsset : XmlDatabaseAsset {
		public int SkillCollectionId { get; set;}
		public List<GambitAsset> Gambits{ get; private set;}

		public GambitCollectionAsset():base(){Gambits = new List<GambitAsset> (); SkillCollectionId = -1;}
		public GambitCollectionAsset(int id):base(id){Gambits = new List<GambitAsset> (); SkillCollectionId = -1;}

		#region implemented abstract members of XmlDatabaseAsset
		public override void OnSaveAsset (XmlWriter writer)
		{
			writer.WriteStartElement ("SkillCollection");
			writer.SetAttr ("Id", SkillCollectionId);
			writer.WriteEndElement ();
			foreach (var gambit in Gambits) {
				writer.WriteStartElement ("Gambit");
				writer.SetAttr ("AssetType", gambit.GetType ().Name);
				gambit.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}
		public override void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "SkillCollection":
				{
					
					SkillCollectionId = reader.GetAttrInt ("Id", -1);
				}
				break;
			case "Gambit":
				{
					// get the skill type
					string gambitAssetType = reader.GetAttrString ("AssetType", "");

					// create an instance of the skill asset
					var asset = GambitUtility.CreateAssetOfType(gambitAssetType);
					if (asset != null) {
						Gambits.Add (asset);
						// initialize the value
						Gambits[Gambits.Count-1].OnLoadAsset(reader);
					}
				}
				break;
			default:
				{
					if (Gambits.Count > 0) {
						// element is not handled, pass to skill asset loader (most recent one)
						Gambits [Gambits.Count - 1].OnLoadAsset (reader);
					}
				}
				break;
			}
		}
		#endregion
		
	}
}
