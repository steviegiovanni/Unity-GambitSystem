using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement;

namespace GameSystems{
	/// <summary>
	/// Targetable effect
	/// </summary>
	public class TargetPrefabEffect : TargetEffect{
		private Object _prefab;
		public Object Prefab{
			get{ return _prefab;}
			set{ _prefab = value;}
		}
			
		public TargetPrefabEffect(TargetPrefabEffectAsset asset):base(asset){
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
			else
				Prefab = AssetBundle.LoadFromFile (Application.streamingAssetsPath + "/assetbundles/effects").LoadAsset (asset.PrefabName);
		}

		//void Temp(IAsyncOperation<IList<IResourceLocation>> op){
		//}

		/// <summary>
		/// apply the effect. override this for other extensions.
		/// </summary>
		public override void ApplyEffect(){
			Debug.Log ("apply target prefab effect");
			//if (Prefab != null) {
				IHasTargetEffects targetSource = Source as IHasTargetEffects;
				if (targetSource != null) {
					if (targetSource.GetTarget () != null) {
						//Addressables.LoadAssets<IResourceLocation> ("Meteor", null).Completed += Temp;
						//Addressables.Instantiate<GameObject> ("Meteor.prefab",targetSource.GetTarget ().transform.position,Quaternion.identity);
						GameObject.Instantiate (Prefab, targetSource.GetTarget ().transform.position, Quaternion.identity);
					}
				}
			//}
		}
	}
}
