using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RPG default level.
/// </summary>
public class RPGDefaultLevel : RPGEntityLevel {
	#region implemented abstract members of RPGEntityLevel
	/// <summary>
	/// Gets the exp required for level.
	/// </summary>
	/// <returns>The exp required for level.</returns>
	/// <param name="level">Level.</param>
	public override int GetExpRequiredForLevel (int level)
	{
		return (int)(Mathf.Pow (Level-1, 2f) * 100);
	}
	#endregion
}
