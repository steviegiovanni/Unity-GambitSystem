using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// Positional effect
	/// </summary>
	public class PositionPrefabEffect : PositionEffect{
		private Object _prefab;
		public Object Prefab{
			get{ return _prefab;}
			set{ _prefab = value;}
		}

		public PositionPrefabEffect():base(){}
		public PositionPrefabEffect(float delay):base(delay){}
		public PositionPrefabEffect(PositionPrefabEffectAsset asset):base(asset){
			Prefab = asset.Prefab;
		}

		/// <summary>
		/// apply the effect. override this for other extensions.
		/// </summary>
		public override void ApplyEffect(){
			Debug.Log ("apply position prefab effect");
			if (Prefab != null) {
				PositionSkill posSkill = Source as PositionSkill;
				if (posSkill != null) {
					GameObject.Instantiate (Prefab,posSkill.Position,Quaternion.identity);
				}
			}
		}
	}
}
