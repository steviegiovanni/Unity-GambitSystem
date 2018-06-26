using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
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

		public PositionAOEStatEffect():base(){}
		public PositionAOEStatEffect(float delay):base(delay){}
		public PositionAOEStatEffect(PositionAOEStatEffectAsset asset):base(asset){
			IncludeSelf = asset.IncludeSelf;
			TargetType = asset.TargetType;
			Radius = asset.Radius;
		}

		public override void ApplyEffect ()
		{
			Debug.Log ("applying AOE stat effect on position");
		}
	}
}
