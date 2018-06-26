using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
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
		}
	}
}
