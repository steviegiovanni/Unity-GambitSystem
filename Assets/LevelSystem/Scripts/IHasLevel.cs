﻿namespace GameSystems.LevelSystem{
	/// <summary>
	/// an entity implementing this interface should have the following
	/// - a function that returns the level of the entity
	/// </summary>
	public interface IHasLevel{
		int GetLevel ();
	}
}
