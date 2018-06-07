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
	private int _cooldown;

	/// <summary>
	/// Gets or sets the skill cooldown
	/// </summary>
	public int Cooldown{
		get{ return _cooldown; }
		set{ _cooldown = value; }
	}

	/// <summary>
	/// default constructor
	/// </summary>
	public Skill(){
		Name = string.Empty;
		Cooldown = 0;
	}

	/// <summary>
	/// parameterized constructor
	/// </summary>
	/// <param name="name">the skill name</param>
	/// <param name="cooldown">cooldown duration</param>
	public Skill(string name, int cooldown){
		Name = name;
		Cooldown = cooldown;
	}
}
