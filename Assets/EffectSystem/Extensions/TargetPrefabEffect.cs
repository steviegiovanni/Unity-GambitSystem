using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public TargetPrefabEffect():base(){}
		public TargetPrefabEffect(float delay):base(delay){}
		public TargetPrefabEffect(TargetPrefabEffectAsset asset):base(asset){
			Prefab = asset.Prefab;
		}

		/// <summary>
		/// apply the effect. override this for other extensions.
		/// </summary>
		public override void ApplyEffect(){
			Debug.Log ("apply target prefab effect");
			if (Prefab != null) {
				IHasTargetEffects targetSource = Source as IHasTargetEffects;
				if (targetSource != null) {
					if(targetSource.GetTarget() != null)
						GameObject.Instantiate (Prefab,targetSource.GetTarget().transform.position,Quaternion.identity);
				}
			}
		}
	}
}
