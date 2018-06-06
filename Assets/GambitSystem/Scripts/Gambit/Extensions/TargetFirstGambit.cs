using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFirstGambit : Gambit, ITargetGambit {
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
	/// The perception.
	/// </summary>
	private Perception _perception;

	/// <summary>
	/// Gets or sets the perception.
	/// </summary>
	/// <value>The perception.</value>
	public Perception Perception{
		get{ return _perception;}
		set{ _perception = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetGambit"/> class.
	/// </summary>
	public TargetFirstGambit():base(){
		_target = null;
		_perception = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetGambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="target">Target.</param>
	public TargetFirstGambit(int priority, Skill skill, GameObject target, Perception perception):base(priority,skill){
		_target = target;
		_perception = perception;
	}

	/// <summary>
	/// Coroutine this instance.
	/// </summary>
	public override IEnumerator Coroutine(){
		while (true) {
			if (Target == null)
				Target = FindTarget ();

			if(Target != null)
				Debug.Log (string.Format("targetting {0}",Target.name));
			yield return null;
		}
	}

	#region ITargetGambit implementation
	/// <summary>
	/// Finds the target.
	/// </summary>
	/// <returns>The target.</returns>
	public GameObject FindTarget ()
	{
		if (Perception == null)
			return null;

		GameObject target = null;
		foreach (var key in Perception.Percepts.Keys) {
			target = Perception.Percepts [key];
			break;
		}

		return target;
	}

	#endregion
}
