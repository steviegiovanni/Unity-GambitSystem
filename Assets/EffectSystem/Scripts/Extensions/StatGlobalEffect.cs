using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.PerceptionSystem;

namespace GameSystems{
	/// <summary>
	/// effect that modifies stat of units area wide 
	/// </summary>
	public class StatGlobalEffect : Effect{
		/// <summary>
		/// include self when finding a target
		/// </summary>
		private bool _includeSelf;
		public bool IncludeSelf{
			get{ return _includeSelf;}
			set{ _includeSelf = value;}
		}

		/// <summary>
		/// The type of the target.
		/// </summary>
		private int _targetType;
		public int TargetType{
			get{ return _targetType;}
			set{ _targetType = value;}
		}

		/// <summary>
		/// modifier of the effect
		/// </summary>
		private float _modifier;
		public float Modifier{
			get{ return _modifier;}
			set{ _modifier = value;}
		}

		/// <summary>
		/// the base stat of the owner that will be multiplied by the modifier
		/// </summary>
		public string _statBase;
		public string StatBase{
			get{ return _statBase;}
			set{ _statBase = value;}
		}

		/// <summary>
		/// The flat value of the stat effect
		/// </summary>
		private int _flatValue;
		public int FlatValue{
			get{ return _flatValue;}
			set{ _flatValue = value;}
		}

		/// <summary>
		/// the target stat
		/// </summary>
		public string _targetStat;
		public string TargetStat{
			get{ return _targetStat;}
			set{ _targetStat = value;}
		}

		/// <summary>
		/// constructor taking an effect asset
		/// </summary>
		public StatGlobalEffect(StatGlobalEffectAsset asset):base(asset){
			TargetType = asset.TargetType;
			IncludeSelf = asset.IncludeSelf;

			Modifier = asset.Modifier;
			StatBase = asset.StatBase;
			FlatValue = asset.FlatValue;
			TargetStat = asset.TargetStat;
		}

		public override void ApplyEffect ()
		{
			Debug.Log ("applying room wide stat effect");

			// get the base stat value of the user of this effect
			int baseValue = 0;
			if (StatBase != "") { // prevents people forgetting to give a stat base name
				IHasStats owner = Source.GetOwner ().GetComponent<IHasStats>();
				if (owner != null) {
					owner.TryGetStatValue (StatBase, out baseValue); // prevents the case where the statname doesn't exist
				} 
			}

			// if the owner doesn't have a perception component, it sees nothing basically, nothing can be affected
			IHasPerception perceptionOwner = Source.GetOwner().GetComponent<IHasPerception>();
			if (perceptionOwner == null)
				return;		

			// for each found percept, if the tag is inside the target type mask, process
			foreach (var percept in perceptionOwner.Perception.Percepts.Values) {
				IPerceivable perceivable = percept.Entity.GetComponent<IPerceivable>();
				if (perceivable != null && ((perceivable.Tag & TargetType) != 0)) {
					if (percept.Entity != Source.GetOwner () || IncludeSelf) {
						// this effect should only apply to perceivable that has stats component
						IHasStats target = percept.Entity.GetComponent<IHasStats> (); 
						if (target != null) {
							target.ModifyStat (TargetStat, Modifier, FlatValue, baseValue);
						}
					}
				}
			}
		}
	}
}
