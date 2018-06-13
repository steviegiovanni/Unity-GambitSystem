using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Perception{
	/// <summary>
	/// an entity implementing this interface should be able to return
	/// a perception component
	/// </summary>
	public interface IHasPerception{
		Perception Perception{ get;}
	}
}
