using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Effects{
	/// <summary>
	/// positional effect asset.
	/// </summary>
	public class PositionAOEStatEffectAsset : PositionEffectAsset {
		public int TargetType { get; set;}
		public bool IncludeSelf { get; set;}
		public float Radius{get; set;}

		public float Modifier { get; set;}
		public string StatBase {get;set;}
		public int FlatValue{ get; set;}
		public string TargetStat{ get; set;}

		public override Effect CreateInstance(){
			return new PositionAOEStatEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);

			writer.SetAttr ("TargetType", TargetType);
			writer.SetAttr ("IncludeSelf", IncludeSelf);
			writer.SetAttr ("Radius", Radius);
			writer.SetAttr ("Modifier", Modifier);
			writer.SetAttr ("StatBase", StatBase);
			writer.SetAttr ("FlatValue", FlatValue);
			writer.SetAttr ("TargetStat", TargetStat);
		}

		#endregion

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Effect":
				{
					TargetType = reader.GetAttrInt ("TargetType", 0);
					IncludeSelf = reader.GetBoolAttribute ("IncludeSelf", true);
					Radius = reader.GetAttrFloat ("Radius", 0.0f);
					Modifier = reader.GetAttrFloat("Modifier",0.0f);
					StatBase = reader.GetAttrString ("StatBase", "");
					FlatValue = reader.GetAttrInt ("FlatValue", 0);
					TargetStat = reader.GetAttrString ("TargetStat", "");
				}
				break;
			}
		}

		#endregion
	}
}
