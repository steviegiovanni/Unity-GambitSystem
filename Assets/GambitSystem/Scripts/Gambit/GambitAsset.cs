using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections.Generic;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// base class for gambit asset. an entry to a gambit collection asset.
	/// </summary>
	public class GambitAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public int Priority { get; set;}
		public string SkillId{ get; set;}
		public int MaxUse{ get; set;}
		public List<GambitConditionAsset> Conditions{ get; set;}

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
					MaxUse = reader.GetAttrInt ("MaxUse", -1);
				}
				break;
			case "Condition":
				{
					var type = reader.GetAttrString ("Type", "");
					GambitConditionAsset gambitCondition = GambitConditionUtility.CreateAssetOfType (type);
					if (gambitCondition != null) {
						Conditions.Add (gambitCondition);
						Conditions [Conditions.Count - 1].OnLoadAsset (reader);
					}
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
			writer.SetAttr ("MaxUse", MaxUse);

			foreach (var condition in Conditions) {
				writer.WriteStartElement ("Condition");
				writer.SetAttr ("Type", condition.GetType().Name);
				condition.OnSaveAsset (writer);
				writer.WriteEndElement ();
			}
		}
		#endregion

		public GambitAsset():base(){
			SkillId = ""; 
			Conditions = new List<GambitConditionAsset> ();
		}
	}
}
