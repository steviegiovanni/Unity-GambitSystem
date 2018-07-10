using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
	/// <summary>
	/// Positional effect
	/// </summary>
	public class PositionEffect : Effect{
		public PositionEffect():base(){}
		public PositionEffect(float delay):base(delay){}
		public PositionEffect(PositionEffectAsset asset):base(asset){}
	}
}
