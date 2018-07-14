using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.PerceptionSystem;

namespace GameSystems{
	/// <summary>
	/// Targetable effect
	/// </summary>
	public class TargetAOEStatEffect : TargetStatEffect{
		/// <summary>
		/// include self when finding a target
		/// </summary>
		private bool _includeTarget;
		public bool IncludeTarget{
			get{ return _includeTarget;}
			set{ _includeTarget = value;}
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
		/// radius of AOE
		/// </summary>
		private float _radius;
		public float Radius{
			get{ return _radius;}
			set{ _radius = value;}
		}

		public TargetAOEStatEffect():base(){}
		public TargetAOEStatEffect(float delay):base(delay){}
		public TargetAOEStatEffect(TargetAOEStatEffectAsset asset):base(asset){
			IncludeTarget = asset.IncludeTarget;
			TargetType = asset.TargetType;
			Radius = asset.Radius;
		}

		public override void ApplyEffect ()
		{
			Debug.Log ("applying AOE stat effect on target");

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

			// get source as a targettable source
			IHasTargetEffects targetSource = Source as IHasTargetEffects;
			if (targetSource == null)
				return;

			// get source owner
			IHasPerception perceptionOwner = Source.GetOwner().GetComponent<IHasPerception>();
			if (perceptionOwner == null)
				return;		

			foreach (var percept in perceptionOwner.Perception.Percepts.Values) {
				if (targetSource.GetTarget () != null && percept.Entity == targetSource.GetTarget () && IncludeTarget) {
					IHasStats target = targetSource.GetTarget ().GetComponent<IHasStats> ();
					if (target != null) {
						target.ModifyStat (TargetStat, Modifier, FlatValue, baseValue);
					}
				} else if(percept.Entity != targetSource.GetTarget ()){
					IPerceivable perceivable = percept.Entity.GetComponent<IPerceivable>();
					if (perceivable != null && ((perceivable.Tag & TargetType) != 0) && (Vector3.Magnitude(percept.Entity.transform.position - targetSource.GetTarget().transform.position) <= Radius)) {
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
