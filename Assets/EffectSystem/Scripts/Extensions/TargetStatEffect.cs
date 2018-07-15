using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
	/// <summary>
	/// Targetable effect
	/// </summary>
	public class TargetStatEffect : TargetEffect{
		private float _modifier;
		public float Modifier{
			get{ return _modifier;}
			set{ _modifier = value;}
		}

		public string _statBase;
		public string StatBase{
			get{ return _statBase;}
			set{ _statBase = value;}
		}

		private int _flatValue;
		public int FlatValue{
			get{ return _flatValue;}
			set{ _flatValue = value;}
		}

		public string _targetStat;
		public string TargetStat{
			get{ return _targetStat;}
			set{ _targetStat = value;}
		}
			
		public TargetStatEffect(TargetStatEffectAsset asset):base(asset){
			Modifier = asset.Modifier;
			StatBase = asset.StatBase;
			FlatValue = asset.FlatValue;
			TargetStat = asset.TargetStat;
		}

		public override void ApplyEffect ()
		{
			int baseValue;
			if (StatBase == "") {
				baseValue = 1;
			} else {
				IHasStats owner = Source.GetOwner ().GetComponent<IHasStats>();
				if (owner != null) {
					baseValue = owner.GetStatValue (StatBase);
				} else {
					baseValue = 1;
				}
			}

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
