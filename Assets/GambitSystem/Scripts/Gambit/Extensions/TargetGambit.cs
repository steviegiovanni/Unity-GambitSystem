using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGambit : Gambit {
	/// <summary>
	/// The target.
	/// </summary>
	private GameObject _target;

	/// <summary>
	/// Gets or sets the target.
	/// </summary>
	/// <value>The target.</value>
	public GameObject Target{
		get{ return _target;}
		set{ _target = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetGambit"/> class.
	/// </summary>
	public TargetGambit():base(){
		_target = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetGambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="target">Target.</param>
	public TargetGambit(int priority, Skill skill, GameObject target):base(priority,skill){
		_target = target;
	}

	/// <summary>
	/// Coroutine this instance.
	/// </summary>
	public override IEnumerator Coroutine(){
		while (true) {
			Debug.Log (string.Format("{0} coroutine",Skill.Name));
			yield return null;
		}
	}
}
