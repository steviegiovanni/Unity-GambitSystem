using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class for Skill
/// </summary>
public class Skill{
	/// <summary>
	/// The owner.
	/// </summary>
	private GameObject _owner;

	/// <summary>
	/// Gets or sets the priority
	/// </summary>
	public GameObject Owner{
		get{ return _owner;}
		set{ _owner = value;}
	}

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
	/// The cast time for using the skill
	/// </summary>
	public float _castTime;

	/// <summary>
	/// Gets or sets the cast time.
	/// </summary>
	public float CastTime{
		get{ return _castTime;}
		set{ _castTime = value;}
	}

	/// <summary>
	/// The delay.
	/// </summary>
	public float _delay;

	/// <summary>
	/// Gets or sets the delay.
	/// </summary>
	/// <value>The delay.</value>
	public float Delay{
		get{ return _delay;}
		set{ _delay = value;}
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
	/// The range.
	/// </summary>
	private float _range = 0.5f;

	/// <summary>
	/// Gets or sets the range.
	/// </summary>
	/// <value>The range.</value>
	public float Range{
		get{ return _range;}
		set{ _range = value;}
	}

	/// <summary>
	/// default constructor
	/// </summary>
	public Skill(){
		Owner = null;
		Name = string.Empty;
		Cooldown = 0.0f;
		CastTime = 0.0f;
		Interruptable = true;
		Range = 0.5f;
		Delay = 0.0f;
	}

	/// <summary>
	/// parameterized constructor
	/// </summary>
	/// <param name="name">the skill name</param>
	/// <param name="cooldown">cooldown duration</param>
	public Skill(GameObject owner, string name, float cooldown, bool interruptable, float castTime, float range, float delay){
		Owner = owner;
		Name = name;
		Cooldown = cooldown;
		Interruptable = interruptable;
		CastTime = castTime;
		Range = range;
		Delay = delay;
	}

	/// <summary>
	/// Skill coroutine.
	/// </summary>
	public IEnumerator SkillCoroutine(){
		Debug.Log ("Using " + Name);
		float time = Time.time;
		yield return new WaitForSeconds(Delay - time);
	}
}
