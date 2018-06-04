using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetBased{
	/// <summary>
	/// SqrDistance between the specified user and the target.
	/// </summary>
	/// <param name="user">User.</param>
	/// <param name="target">Target.</param>
	float SqrDistance (GameObject user, GameObject target);
}
