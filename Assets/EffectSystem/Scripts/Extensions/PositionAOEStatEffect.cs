using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
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

		public PositionAOEStatEffect():base(){}
		public PositionAOEStatEffect(float delay):base(delay){}
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
			Debug.Log ("applying AOE stat effect on position");
		}
	}
}
