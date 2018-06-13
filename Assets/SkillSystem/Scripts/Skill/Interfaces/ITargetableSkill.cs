using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// every skill implementing ITargetableSkill needs to provide 
	/// a way to return the current target
	/// </summary>
	public interface ITargetableSkill{
		GameObject Target {get;set;}
	}
}
