using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	public interface ITargetableEffect{
		/// <summary>
		/// every skill effect that implements ITargetableEffect needs to implement
		/// getter setter for the target
		/// </summary>
		GameObject Target{get;set;}
	}
}
