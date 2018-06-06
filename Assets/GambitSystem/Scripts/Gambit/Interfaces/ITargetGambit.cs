using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for targetable gambit.
/// </summary>
public interface ITargetGambit{
	/// <summary>
	/// Finds the target.
	/// </summary>
	/// <returns>The target.</returns>
	GameObject FindTarget ();
}
