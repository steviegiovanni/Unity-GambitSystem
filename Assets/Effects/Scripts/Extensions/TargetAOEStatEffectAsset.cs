using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.Effects{
	/// <summary>
	/// Targetable effect asset.
	/// </summary>
	public class TargetAOEStatEffectAsset : TargetStatEffectAsset {
		public int TargetType { get; set;}
		public bool IncludeTarget { get; set;}
		public float Radius{get; set;}

		public override Effect CreateInstance(){
			return new TargetAOEStatEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);

			writer.SetAttr ("TargetType", TargetType);
			writer.SetAttr ("IncludeTarget", IncludeTarget);
			writer.SetAttr ("Radius", Radius);
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
					IncludeTarget = reader.GetBoolAttribute ("IncludeTarget", true);
					Radius = reader.GetAttrFloat ("Radius", 0.0f);
				}
				break;
			}
		}

		#endregion
	}
}
