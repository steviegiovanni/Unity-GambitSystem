using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems{
	/// <summary>
	/// an entity implementing this interface should have the following
	/// - a function that returns the stat value in percent
	/// </summary>
	public interface IHasStats {
		float GetStatPercentValue (string statName);
		bool TryGetStatPercentValue (string statName, out float value); // a way to return false if stat doesn't exist
		void ModifyStat(string statName,float modifier, int flatValue, int baseValue);
		void ModifyStat (string statName, int value);
		int GetStatValue (string statName);
		bool TryGetStatValue (string statName, out int value);
	}
}
