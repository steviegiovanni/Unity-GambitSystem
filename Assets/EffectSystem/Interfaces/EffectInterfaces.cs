using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// interfaces that needs to be implemented by skills to have a specific effects 
/// </summary>
namespace GameSystems{
	public interface IHasEffects{
		GameObject GetOwner ();
	}

	public interface IHasPositionEffects{
		Vector3 GetPosition();
	}

	public interface IHasTargetEffects{
		GameObject GetTarget();
	}
}
