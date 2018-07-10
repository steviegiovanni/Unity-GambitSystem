using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// RPG level change event arguments.
/// </summary>
public class RPGLevelChangeEventArgs : EventArgs {
	/// <summary>
	/// Gets the new level.
	/// </summary>
	/// <value>The new level.</value>
	public int NewLevel{ get; private set;}

	/// <summary>
	/// Gets the old level.
	/// </summary>
	/// <value>The old level.</value>
	public int OldLevel{ get; private set;}

	/// <summary>
	/// Initializes a new instance of the <see cref="RPGLevelChangeEventArgs"/> class.
	/// </summary>
	/// <param name="newLevel">New level.</param>
	/// <param name="oldLevel">Old level.</param>
	public RPGLevelChangeEventArgs(int newLevel, int oldLevel){
		NewLevel = newLevel;
		OldLevel = oldLevel;
	}
}
