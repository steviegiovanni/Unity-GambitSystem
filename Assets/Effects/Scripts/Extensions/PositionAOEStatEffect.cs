using GameSystems.PerceptionSystem;
using GameSystems.StatSystem;
using UnityEngine;

namespace GameSystems.Effects{
	/// <summary>
	/// Positional effect
	/// </summary>
	public class PositionAOEStatEffect : PositionEffect{
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
		/// radius of AOE
		/// </summary>
		private float _radius;
		public float Radius{
			get{ return _radius;}
			set{ _radius = value;}
		}

		/// <summary>
		/// The modifier
		/// </summary>
		private float _modifier;
		public float Modifier{
			get{ return _modifier;}
			set{ _modifier = value;}
		}

		/// <summary>
		/// The stat of the owner used for the base value
		/// </summary>
		public string _statBase;
		public string StatBase{
			get{ return _statBase;}
			set{ _statBase = value;}
		}

		/// <summary>
		/// The flat value
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
		public PositionAOEStatEffect(PositionAOEStatEffectAsset asset):base(asset){
			IncludeSelf = asset.IncludeSelf;
			TargetType = asset.TargetType;
			Radius = asset.Radius;

			Modifier = asset.Modifier;
			StatBase = asset.StatBase;
			FlatValue = asset.FlatValue;
			TargetStat = asset.TargetStat;
		}

		public override void ApplyEffect ()
		{
			// get the base stat value of the user of this effect
			int baseValue = 0;
			if (StatBase != "") {
				IHasStats owner = Source.GetOwner ().GetComponent<IHasStats>();
				if (owner != null) {
					owner.TryGetStatValue (StatBase, out baseValue); // prevents the case where the statname doesn't exist
				} 
			}

			// get source as a positional source
			IHasPositionEffects positionSource = Source as IHasPositionEffects;
			if (positionSource == null)
				return;

			// get source owner
			IHasPerception perceptionOwner = Source.GetOwner().GetComponent<IHasPerception>();
			if (perceptionOwner == null)
				return;		

			// for each found percept, if the tag is inside the target type mask, process
			foreach (var percept in perceptionOwner.Perception.Percepts.Values) {
				IPerceivable perceivable = percept.Entity.GetComponent<IPerceivable>();
				if (perceivable != null && ((perceivable.Tag & TargetType) != 0) && (Vector3.Magnitude (percept.Entity.transform.position - positionSource.GetPosition ()) <= Radius)) {
					if (percept.Entity != Source.GetOwner () || IncludeSelf) {
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
