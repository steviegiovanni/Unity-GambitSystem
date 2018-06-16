﻿using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;

namespace GameSystems.GambitSystem{
	public class GambitConditionAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public int StatValue { get; set;}
		public string StatName{ get; set;}

		public virtual GambitCondition CreateInstance(){
			return new GambitCondition (this);
		}

		#region IXmlOnLoadAsset implementation

		public void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Condition":
				{
					StatValue = reader.GetAttrInt ("StatValue", 0);
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

		public void OnSaveAsset (XmlWriter writer)
		{
			writer.SetAttr ("StatName", StatName);
			writer.SetAttr ("StatValue", StatValue);
		}

		#endregion


	}
}