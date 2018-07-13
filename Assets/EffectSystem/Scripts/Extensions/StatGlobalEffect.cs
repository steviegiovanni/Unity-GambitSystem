using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public StatGlobalEffect():base(){}
		public StatGlobalEffect(float delay):base(delay){}
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
		}
	}
}
