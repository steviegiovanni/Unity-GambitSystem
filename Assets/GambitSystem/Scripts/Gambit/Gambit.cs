using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a character gambit
/// </summary>
public class Gambit{
	/// <summary>
	/// Execution priority of gambit
	/// </summary>
	private int _priority;

	/// <summary>
	/// The skill.
	/// </summary>
	private Skill _skill;

	/// <summary>
	/// Gets or sets the priority.
	/// </summary>
	/// <value>The priority.</value>
	public int Priority{
		get{ return _priority;}
		set{ _priority = value;}
	}

	public Skill Skill{
		get{ return _skill;}
		set{ _skill = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	public Gambit(){
		Priority = 0;
		Skill = new Skill(string.Empty);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	public Gambit(int priority, Skill skill){
		Priority = priority;
		Skill = skill;
	}

	/// <summary>
	/// Coroutine this instance.
	/// </summary>
	public virtual IEnumerator Coroutine(){
		while (true) {
			Debug.Log (string.Format("{0} coroutine",Skill.Name));
			yield return null;
		}
	}
}
