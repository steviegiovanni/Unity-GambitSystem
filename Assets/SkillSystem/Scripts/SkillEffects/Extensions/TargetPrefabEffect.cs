using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
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
				TargetSkill targetSkill = Source as TargetSkill;
				if (targetSkill != null) {
					if(targetSkill.Target != null)
						GameObject.Instantiate (Prefab,targetSkill.Target.transform.position,Quaternion.identity);
				}
			}
		}
	}
}
