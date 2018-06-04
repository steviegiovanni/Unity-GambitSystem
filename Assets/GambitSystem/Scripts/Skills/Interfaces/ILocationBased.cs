using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILocationBased{
	/// <summary>
	/// SqrDistance between the specified user and the location.
	/// </summary>
	/// <param name="player">Player.</param>
	float SqrDistance (GameObject user, Vector3 location);
}
