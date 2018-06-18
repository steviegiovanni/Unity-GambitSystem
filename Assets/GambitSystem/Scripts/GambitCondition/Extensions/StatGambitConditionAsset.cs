using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.GambitSystem{
	public class StatGambitConditionAsset : GambitConditionAsset {
		public float StatValue { get; set;}
		public string StatName{ get; set;}

		public override GambitCondition CreateInstance(){
			return new StatGambitCondition (this);
		}

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Condition":
				{
					StatValue = reader.GetAttrFloat ("StatValue", 0.0f);
					StatName = reader.GetAttrString ("StatName", "");
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
			base.OnSaveAsset (writer);
			writer.SetAttr ("StatName", StatName);
			writer.SetAttr ("StatValue", StatValue);
		}

		#endregion


	}
}