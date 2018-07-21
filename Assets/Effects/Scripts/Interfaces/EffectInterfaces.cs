using UnityEngine;

/// <summary>
/// interfaces that needs to be implemented by classes to have specific effects 
/// </summary>
namespace GameSystems.Effects{
	/// <summary>
	/// everything that has effects has to have an owner (user of the skill or item for example)
	/// </summary>
	public interface IHasEffects{
		GameObject GetOwner ();
	}

	/// <summary>
	/// should be able to get the position of anything that has positional effects
	/// </summary>
	public interface IHasPositionEffects{
		Vector3 GetPosition();
	}

	/// <summary>
	/// should be able to get the target of anything that has targetable effects
	/// </summary>
	public interface IHasTargetEffects{
		GameObject GetTarget();
	}
}
