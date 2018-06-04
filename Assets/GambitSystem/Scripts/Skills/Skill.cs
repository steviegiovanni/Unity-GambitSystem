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
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill(){
		Name = string.Empty;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	public Skill(string name){
		Name = name;
	}
}
