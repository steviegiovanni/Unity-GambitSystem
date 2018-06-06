using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill.
/// </summary>
public class Skill{
	/// <summary>
	/// The name.
	/// </summary>
	private string _name;

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>The name.</value>
	public string Name{
		get{ return _name;}
		set{ _name = value;}
	}

	/// <summary>
	/// The cooldown.
	/// </summary>
	private int _cooldown;

	/// <summary>
	/// Gets or sets the cooldown.
	/// </summary>
	/// <value>The cooldown.</value>
	public int Cooldown{
		get{ return _cooldown; }
		set{ _cooldown = value; }
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill(){
		Name = string.Empty;
		Cooldown = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="range">Range.</param>
	public Skill(string name, int cooldown){
		Name = name;
		Cooldown = cooldown;
	}
}
