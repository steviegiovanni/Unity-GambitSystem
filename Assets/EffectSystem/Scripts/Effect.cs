using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
	/// <summary>
	/// Effect. Skill or item has a list of effects associated to it.
	/// The effects is delayed from the moment the skill or item is used.
	/// </summary>
	public class Effect {
		/// <summary>
		/// the delay of the effect
		/// </summary>
		private float _delay;
		public float Delay{
			get{ return _delay;}
			set{ _delay = value;}
		}

		/// <summary>
		/// apply the effect. override this for other extensions. the base effect does nothing
		/// </summary>
		public virtual void ApplyEffect(){}

		/// <summary>
		/// constructor with effect asset as input
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
