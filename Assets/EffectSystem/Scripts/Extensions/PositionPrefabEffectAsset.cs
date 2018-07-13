using System.Xml;
using UtilitySystems.XmlDatabase;
using UnityEngine;
//using UnityEditor;

namespace GameSystems{
	/// <summary>
	/// positional effect asset.
	/// </summary>
	public class PositionPrefabEffectAsset : PositionEffectAsset {
		public Object Prefab { get; set;}
		public string PrefabName{ get; set;}

		public override Effect CreateInstance(){
			return new PositionPrefabEffect (this);
		}

		#region IXmlOnSaveAsset implementation

		public override void OnSaveAsset (XmlWriter writer)
		{
			base.OnSaveAsset (writer);

			writer.SetAttr ("PrefabName", PrefabName);
		}

		#endregion

		#region IXmlOnLoadAsset implementation

		public override void OnLoadAsset (XmlReader reader)
		{
			base.OnLoadAsset (reader);
			switch (reader.Name) {
			case "Effect":
				{
					PrefabName = reader.GetAttrString ("PrefabName","");
					AssetBundle [] bundles = Resources.FindObjectsOfTypeAll<AssetBundle> ();
					int i = 0;
					bool found = false;
					while (i < bundles.Length && !found) {
						if (bundles [i].name == "effects") {
							found = true;
						} else
							i++;
					}

					if (found)
						Prefab = bundles [i].LoadAsset (PrefabName);
					else
						Prefab = AssetBundle.LoadFromFile (Application.streamingAssetsPath + "/assetbundles/effects").LoadAsset (PrefabName);
					//Prefab = AssetDatabase.LoadAssetAtPath <Object>(reader.GetAttrString ("Path", ""));
				}
				break;
			}
		}

		#endregion
	}
}
