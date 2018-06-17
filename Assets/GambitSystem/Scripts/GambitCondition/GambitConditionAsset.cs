using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;

namespace GameSystems.GambitSystem{
	public class GambitConditionAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public virtual GambitCondition CreateInstance(){
			return new GambitCondition (this);
		}

		#region IXmlOnLoadAsset implementation

		public virtual void OnLoadAsset (XmlReader reader)
		{
			switch (reader.Name) {
			case "Condition":
				{
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
		}

		#endregion


	}
}