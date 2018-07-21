using UnityEngine;
using GameSystems.PerceptionSystem;

namespace GameSystems.Effects{
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

		/// <summary>
		/// constructor
		/// </summary>
		public TargetAOEStatEffect(TargetAOEStatEffectAsset asset):base(asset){
			IncludeTarget = asset.IncludeTarget;
			TargetType = asset.TargetType;
			Radius = asset.Radius;
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

			// get source as a targettable source
			IHasTargetEffects targetSource = Source as IHasTargetEffects;
			if (targetSource == null)
				return;

			// get source owner
			IHasPerception perceptionOwner = Source.GetOwner().GetComponent<IHasPerception>();
			if (perceptionOwner == null)
				return;		

			// for each found percept, if the tag is inside the target type mask, process
			foreach (var percept in perceptionOwner.Perception.Percepts.Values) {
				IPerceivable perceivable = percept.Entity.GetComponent<IPerceivable>();
				if (perceivable != null && ((perceivable.Tag & TargetType) != 0) && (Vector3.Magnitude(percept.Entity.transform.position - targetSource.GetTarget().transform.position) <= Radius)) {
					if (percept.Entity != targetSource.GetTarget () || IncludeTarget) {
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
