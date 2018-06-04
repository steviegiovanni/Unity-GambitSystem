using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationBasedSkill : Skill, ILocationBased {
	/// <summary>
	/// The range.
	/// </summary>
	private float _range;

	/// <summary>
	/// Gets or sets the range.
	/// </summary>
	/// <value>The range.</value>
	public float Range{
		get{ return _range;}
		set{ _range = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="LocationBasedSkill"/> class.
	/// </summary>
	public LocationBasedSkill():base(){
		Range = 1.0f;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="LocationBasedSkill"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="range">Range.</param>
	public LocationBasedSkill(string name, float range):base(name){
		Range = range;
	}

	#region ILocationBased implementation
	/// <summary>
	/// SqrDistance between the specified user and the location.
	/// </summary>
	/// <param name="player">Player.</param>
	/// <returns>The distance.</returns>
	/// <param name="user">User.</param>
	/// <param name="location">Location.</param>
	public float SqrDistance (GameObject user, Vector3 location)
	{
		return Vector3.SqrMagnitude(user.gameObject.transform.position - location);
	}

	#endregion

}
