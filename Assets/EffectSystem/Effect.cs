using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
	/// <summary>
	/// Skill effect. Every skill has a list of effects associated to it.
	/// The effects is delayed from the moment the skill coroutine is run.
	/// </summary>
	public class Effect {
		/// <summary>
		/// the delay of the skill effect
		/// </summary>
		private float _delay;
		public float Delay{
			get{ return _delay;}
			set{ _delay = value;}
		}

		/// <summary>
		/// apply the effect. override this for other extensions.
		/// </summary>
		public virtual void ApplyEffect(){
			Debug.Log ("apply skill effect");
		}

		/// <summary>
		/// default constructor
		/// </summary>
		public Effect(){
			Delay = 0.0f;
		}

		/// <summary>
		/// constructor with specified delay
		/// </summary>
		public Effect(float delay){
			Delay = delay;
		}

		/// <summary>
		/// constructor with skilleffect asset as input
		/// </summary>
		public Effect(EffectAsset asset){
			Delay = asset.Delay;
		}

		/// <summary>
		/// source of the effect. could be skill, could be item
		/// </summary>
		private IHasEffects _source;
		public IHasEffects Source{
			get{ return _source;}
			set{ _source = value;}
		}
	}
}
