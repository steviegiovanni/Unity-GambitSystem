using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a character gambit
/// </summary>
public class Gambit{
	/// <summary>
	/// name of gambit
	/// </summary>
	private string _name;

	/// <summary>
	/// Execution priority of gambit
	/// </summary>
	private int _priority;

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>The name.</value>
	public string Name{
		get { return _name;}
		set{ _name = value;}
	}

	/// <summary>
	/// Gets or sets the priority.
	/// </summary>
	/// <value>The priority.</value>
	public int Priority{
		get{ return _priority;}
		set{ _priority = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	public Gambit(){
		Name = string.Empty;
		Priority = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="priority">Priority.</param>
	public Gambit(string name, int priority){
		Name = name;
		Priority = priority;
	}

	/// <summary>
	/// can be called every update
	/// </summary>
	public virtual void Update(){
		Debug.Log (string.Format("{0} update",Name));
	}
}
