using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// an entity implementing this interface should have the following
	/// - a function that returns the stat value in percent
	/// </summary>
	public interface IHasStats {
		float GetStatPercentValue (string statName);
		bool TryGetStatPercentValue (string statName, out float value); // a way to return false if stat doesn't exist
	}
}
