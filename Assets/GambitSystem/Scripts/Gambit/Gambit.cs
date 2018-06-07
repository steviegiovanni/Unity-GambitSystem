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
	/// The skill associated with this gambit
	/// </summary>
	private Skill _skill;

	/// <summary>
	/// The owner.
	/// </summary>
	private GameObject _owner;

	/// <summary>
	/// Gets or sets the priority
	/// </summary>
	public int Priority{
		get{ return _priority;}
		set{ _priority = value;}
	}

	/// <summary>
	/// Gets or sets the skill
	/// </summary>
	public Skill Skill{
		get{ return _skill;}
		set{ _skill = value;}
	}

	/// <summary>
	/// Gets or sets the owner.
	/// </summary>
	/// <value>The owner.</value>
	public GameObject Owner{
		get{ return _owner;}
		set{ _owner = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	public Gambit(){
		Priority = 0;
		Skill = new Skill();
		Owner = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	public Gambit(GameObject owner, int priority, Skill skill){
		Owner = owner;
		Priority = priority;
		Skill = skill;
	}

	/// <summary>
	/// Coroutine associated to this gambit when it's active
	/// </summary>
	public virtual IEnumerator Coroutine(){
		while (true) {
			Debug.Log (string.Format("{0} coroutine",Skill.Name));
			yield return null;
		}
	}
}
