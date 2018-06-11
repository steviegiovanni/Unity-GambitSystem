using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	public class SkillEffect {
		private float _delay;
		public float Delay{
			get{ return _delay;}
			set{ _delay = value;}
		}

		public virtual void ApplyEffect(){
			Debug.Log ("apply skill effect");
		}

		public SkillEffect(){
			Delay = 0.0f;
		}

		public SkillEffect(float delay){
			Delay = delay;
		}

		public SkillEffect(SkillEffectAsset asset){
			Delay = asset.Delay;
		}
	}
}
