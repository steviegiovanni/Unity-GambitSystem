using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.PerceptionSystem{
	/// <summary>
	/// an entity implementing this interface should have 
	/// a function that returns the tag of the entity (none, ally, or enemy)
	/// </summary>
	public interface IPerceivable{
		int Tag { get; set; }
	}
}
