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
	/// <param name="range">Range.</param>
	public Skill(string name){
		Name = name;
	}

	/// <summary>
	/// Determines whether skill can be used
	/// </summary>
	/// <returns><c>true</c> if this instance can use; otherwise, <c>false</c>.</returns>
	public virtual bool CanUse(){
		return true;
	}

	/// <summary>
	/// Uses the skill.
	/// </summary>
	public virtual void UseSkill(){
		Debug.Log (string.Format("using {0}",Name));
	}

	/// <summary>
	/// Determines whether this instance cancel skill.
	/// </summary>
	/// <returns><c>true</c> if this instance cancel skill; otherwise, <c>false</c>.</returns>
	public virtual void CancelSkill(){
		Debug.Log (string.Format ("no longer using {0}", Name));
	}
}
