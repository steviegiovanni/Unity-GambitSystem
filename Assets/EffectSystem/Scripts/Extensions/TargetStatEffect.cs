using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
	/// <summary>
	/// Targetable effect that modifies target's stats
	/// </summary>
	public class TargetStatEffect : TargetEffect{
		/// <summary>
		/// modifier of the effect
		/// </summary>
		private float _modifier;
		public float Modifier{
			get{ return _modifier;}
			set{ _modifier = value;}
		}

		/// <summary>
		/// The owner's stat used as the base value
		/// </summary>
		public string _statBase;
		public string StatBase{
			get{ return _statBase;}
			set{ _statBase = value;}
		}

		/// <summary>
		/// The flat value not affected by the modifier
		/// </summary>
		private int _flatValue;
		public int FlatValue{
			get{ return _flatValue;}
			set{ _flatValue = value;}
		}

		/// <summary>
		/// The target stat
		/// </summary>
		public string _targetStat;
		public string TargetStat{
			get{ return _targetStat;}
			set{ _targetStat = value;}
		}

		/// <summary>
		/// constructor
		/// </summary>
		public TargetStatEffect(TargetStatEffectAsset asset):base(asset){
			Modifier = asset.Modifier;
			StatBase = asset.StatBase;
			FlatValue = asset.FlatValue;
			TargetStat = asset.TargetStat;
		}

		public override void ApplyEffect ()
		{
			// get the base stat value of the user of this effect
			int baseValue = 0;
			if (StatBase != "") { // prevents people forgetting to give a stat base name
				IHasStats owner = Source.GetOwner ().GetComponent<IHasStats>();
				if (owner != null) {
					owner.TryGetStatValue (StatBase, out baseValue); // prevents the case where the statname doesn't exist
				} 
			}

			// apply effect to target
			IHasTargetEffects targetSource = Source as IHasTargetEffects;
			if (targetSource != null) {
				if (targetSource.GetTarget () != null) {
					IHasStats target = targetSource.GetTarget ().GetComponent<IHasStats> ();
					if (target != null) {
						target.ModifyStat (TargetStat, Modifier, FlatValue, baseValue);
					}
				}
			}
		}
	}
}
