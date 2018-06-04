using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBasedSkill : Skill, ITargetBased {
	#region ITargetBased implementation
	/// <summary>
	/// SqrDistance between the specified user and the target.
	/// </summary>
	/// <param name="user">User.</param>
	/// <param name="target">Target.</param>
	/// <returns>The distance.</returns>
	public float SqrDistance (GameObject user, GameObject target)
	{
		return Vector3.SqrMagnitude (user.gameObject.transform - target.gameObject.transform);
	}

	#endregion


}
