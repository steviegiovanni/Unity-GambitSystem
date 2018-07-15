using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement;

namespace GameSystems{
	/// <summary>
	/// Positional effect
	/// </summary>
	public class PositionPrefabEffect : PositionEffect{
		private Object _prefab;
		public Object Prefab{
			get{ return _prefab;}
			set{ _prefab = value;}
		}
			
		public PositionPrefabEffect(PositionPrefabEffectAsset asset):base(asset){
			//Prefab = asset.Prefab;
			//Prefab = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/assetbundles/effects").LoadAsset(asset.PrefabName);

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
				Prefab = bundles [i].LoadAsset (asset.PrefabName);
			else {
				Debug.Log ("not found");
				Prefab = AssetBundle.LoadFromFile (Application.streamingAssetsPath + "/assetbundles/effects").LoadAsset (asset.PrefabName);
			}
		}

		//void Temp(IAsyncOperation<IList<IResourceLocation>> op){
		//	Debug.Log ("SHALALALALALALA");
		//}

		/// <summary>
		/// apply the effect. override this for other extensions.
		/// </summary>
		public override void ApplyEffect(){
			Debug.Log ("apply position prefab effect");
			//if (Prefab != null) {
				IHasPositionEffects posSource = Source as IHasPositionEffects;
				if (posSource != null) {
					//Debug.Log ("WHAT THE FUCK");
					//Addressables.LoadAssets<IResourceLocation> ("Meteor", null).Completed += Temp;
					//Addressables.Instantiate<GameObject> ("Meteor.prefab",posSource.GetPosition(),Quaternion.identity);
					GameObject.Instantiate (Prefab,posSource.GetPosition(),Quaternion.identity);
				}
			//}
		}
	}
}
