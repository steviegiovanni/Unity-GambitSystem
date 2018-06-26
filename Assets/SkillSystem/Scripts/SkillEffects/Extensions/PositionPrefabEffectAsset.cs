using System.Xml;
using UtilitySystems.XmlDatabase;
using UnityEngine;
using UnityEditor;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// positional effect asset.
	/// </summary>
	public class PositionPrefabEffectAsset : PositionEffectAsset {
		public Object Prefab { get; set;}

		public override Effect CreateInstance(){
			return new PositionPrefabEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);

			if (Prefab != null) {
				writer.SetAttr ("Path", AssetDatabase.GetAssetPath (Prefab));
			}
		}

		#endregion

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Effect":
				{
					Prefab = AssetDatabase.LoadAssetAtPath <Object>(reader.GetAttrString ("Path", ""));
				}
				break;
			}
		}

		#endregion
	}
}
