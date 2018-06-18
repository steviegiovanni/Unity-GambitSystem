using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// an entity implementing this interface should have the following
	/// - a function that moves the entity to a certain position
	/// - a function that stops the entity's movement
	/// - a function to query the remaining distance of an entity to a position
	/// </summary>
	public interface IMovable {
		void MoveTo (Vector3 targetPos);
		bool Stopped{ get; set;}
		float RemainingDistance (Vector3 targetPos);
	}
}