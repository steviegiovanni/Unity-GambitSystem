using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
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

		public StatGlobalEffect():base(){}
		public StatGlobalEffect(float delay):base(delay){}
		public StatGlobalEffect(StatGlobalEffectAsset asset):base(asset){
			TargetType = asset.TargetType;
			IncludeSelf = asset.IncludeSelf;
		}

		public override void ApplyEffect ()
		{
			Debug.Log ("applying room wide stat effect");
		}
	}
}
