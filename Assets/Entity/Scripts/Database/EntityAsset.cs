using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Entities.Database{
	/// <summary>
	/// Gambit collection asset for entry on the collection database
	/// </summary>
	public class EntityAsset : XmlDatabaseAsset {
		public string Description{ get; set;}
		public int Tag{ get; set;}
		public int AlertMask{ get; set;}
		public float Vision{ get; set;}


		public int GambitCollectionId{ get; set;}
		public int SkillCollectionId { get; set;}

		public EntityAsset():base(){GambitCollectionId = -1;SkillCollectionId = -1;}
		public EntityAsset(int id):base(id){GambitCollectionId = -1;SkillCollectionId = -1;}

		#region implemented abstract members of XmlDatabaseAsset
		public override void OnSaveAsset (XmlWriter writer)
		{
			writer.WriteStartElement ("EntityInfo");
			writer.SetAttr ("Description", Description);
			writer.SetAttr ("Tag", Tag);
			writer.SetAttr ("AlertMask", AlertMask);
			writer.SetAttr ("Vision", Vision);
			writer.WriteEndElement ();

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
			case "EntityInfo":
				{
					Description = reader.GetAttrString ("Description", "");
					Tag = reader.GetAttrInt ("Tag", 0);
					AlertMask = reader.GetAttrInt ("AlertMask", 0);
					Vision = reader.GetAttrFloat ("Vision", 0.0f);
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
