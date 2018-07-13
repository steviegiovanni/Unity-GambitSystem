using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems{
	/// <summary>
	/// Targetable effect asset.
	/// </summary>
	public class TargetStatEffectAsset : TargetEffectAsset {
		public float Modifier { get; set;}
		public string StatBase {get;set;}
		public int FlatValue{ get; set;}
		public string TargetStat{ get; set;}

		public override Effect CreateInstance(){
			return new TargetStatEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);

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
