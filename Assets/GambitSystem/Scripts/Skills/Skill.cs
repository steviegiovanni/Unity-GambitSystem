using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class for Skill
/// </summary>
public class Skill{
	/// <summary>
	/// the name of the skill
	/// </summary>
	private string _name;

	/// <summary>
	/// Gets or sets the name of the skill
	/// </summary>
	public string Name{
		get{ return _name;}
		set{ _name = value;}
	}

	/// <summary>
	/// skill cooldown
	/// </summary>
	private float _cooldown;

	/// <summary>
	/// Gets or sets the skill cooldown
	/// </summary>
	public float Cooldown{
		get{ return _cooldown; }
		set{ _cooldown = value; }
	}

	/// <summary>
	/// The delay for using the skill
	/// </summary>
	public float _castTime;

	/// <summary>
	/// Gets or sets the delay.
	/// </summary>
	/// <value>The delay</value>
	public float CastTime{
		get{ return _castTime;}
		set{ _castTime = value;}
	}

	/// <summary>
	/// whether the skill can be interrupted
	/// </summary>
	public bool _interruptable;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is interruptable.
	/// </summary>
	/// <value><c>true</c> if interruptable; otherwise, <c>false</c>.</value>
	public bool Interruptable{
		get{ return _interruptable;}
		set{ _interruptable = value;}
	}

	/// <summary>
	/// default constructor
	/// </summary>
	public Skill(){
		Name = string.Empty;
		Cooldown = 0.0f;
		CastTime = 0.0f;
		Interruptable = true;
	}

	/// <summary>
	/// parameterized constructor
	/// </summary>
	/// <param name="name">the skill name</param>
	/// <param name="cooldown">cooldown duration</param>
	public Skill(string name, float cooldown, bool interruptable, float castTime){
		Name = name;
		Cooldown = cooldown;
		Interruptable = interruptable;
		CastTime = castTime;
	}

	public IEnumerator SkillCoroutine(){
		Debug.Log ("Using " + Name);
		yield return null;
	}
}
