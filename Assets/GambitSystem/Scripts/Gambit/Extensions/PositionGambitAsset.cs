using System.Xml;
using UtilitySystems.XmlDatabase;
using UnityEngine;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// gambit asset for gambits that requires a positional target
	/// </summary>
	public class PositionGambitAsset : GambitAsset {
		public Vector3 TargetPosition { get; set;}

		public override Gambit CreateInstance(){
			return new PositionGambit (this);
		}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Gambit":
				{
					float x = reader.GetAttrFloat ("PosX", 0.0f);
					float y = reader.GetAttrFloat ("PosY", 0.0f);
					float z = reader.GetAttrFloat ("PosZ", 0.0f);
					TargetPosition = new Vector3 (x, y, z);
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
		public override void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("PosX", TargetPosition.x);
			writer.SetAttr ("PosY", TargetPosition.y);
			writer.SetAttr ("PosZ", TargetPosition.z);
			base.OnSaveAsset (writer);
		}
		#endregion
	}
}
