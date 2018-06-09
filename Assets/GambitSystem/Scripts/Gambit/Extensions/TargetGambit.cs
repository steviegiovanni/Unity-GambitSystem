using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// gambit that looks for a target
/// </summary>
public class TargetGambit : Gambit{
	/// <summary>
	/// include self when finding a target
	/// </summary>
	private bool _includeSelf;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="TargetGambit"/> include self.
	/// </summary>
	/// <value><c>true</c> if include self; otherwise, <c>false</c>.</value>
	public bool IncludeSelf{
		get{ return _includeSelf;}
		set{ _includeSelf = value;}
	}

	/// <summary>
	/// The type of the target.
	/// </summary>
	private int _targetType;

	/// <summary>
	/// Gets or sets the type of the target.
	/// </summary>
	/// <value>The type of the target.</value>
	public int TargetType{
		get{ return _targetType;}
		set{ _targetType = value;}
	}

	/// <summary>
	/// The target
	/// </summary>
	private GameObject _target;

	/// <summary>
	/// Gets or sets the target
	/// </summary>
	public GameObject Target{
		get{ return _target;}
		set{ _target = value;}
	}

	/// <summary>
	/// The perception
	/// </summary>
	private Perception _perception;

	/// <summary>
	/// Gets or sets the perception.
	/// </summary>
	public Perception Perception{
		get{ return _perception;}
		set{ _perception = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetGambit"/> class.
	/// </summary>
	public TargetGambit():base(){
		TargetType = 0;
		Target = null;
		Perception = null;
		IncludeSelf = true;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TargetFirstGambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="target">Target.</param>
	/// <param name="perception">Perception.</param>
	public TargetGambit(GameObject owner, int priority, Skill skill, int targetType, bool includeSelf, Perception perception):base(owner, priority,skill){
		TargetType = targetType;
		Target = null;
		Perception = perception;
		IncludeSelf = includeSelf;
	}

	/// <summary>
	/// override coroutine of this gambit
	/// </summary>
	public override IEnumerator GambitCoroutine(){
		while (true) {
			Target = FindTarget ();
			if (Target == null) { // no target found
				break;
			}else if (Target != null) {
				IMovable movableEntity = Owner.GetComponent<IMovable> ();
				if (movableEntity == null)
					Debug.LogWarning ("Owner does not implement IMovable");
				else {
					if (movableEntity.RemainingDistance (Target.transform.position) <= Skill.Range) {
						movableEntity.StopMove ();
					}else {
						movableEntity.MoveTo (Target.transform.position);
					}
					
					if (Skill.Cooldown <= GetOwnerCooldown () && (movableEntity.RemainingDistance(Target.transform.position) <= Skill.Range)) {
						// set up skill target
						ITargetableSkill targetableSkill = Skill as ITargetableSkill;
						if (targetableSkill == null)
							Debug.LogWarning ("calling a non targetable skill from a target gambit");
						else
							targetableSkill.Target = Target;

						ResetOwnerCooldown ();
						break;
					}
				}
			}
			yield return null;
		}

		if (Target == null) // no target, return immediately
			yield return null;
		else
			yield return new WaitForSeconds(Skill.CastTime);
	}
		
	/// <summary>
	/// Finds the target.
	/// </summary>
	/// <returns>The target.</returns>
	public virtual GameObject FindTarget ()
	{
		if (Perception == null)
			return null;

		GameObject target = null;
		foreach (var key in Perception.Percepts.Keys) {
			if ((Perception.Percepts [key].Entity != null) 
				&& ((IncludeSelf && (Perception.Percepts[key].Entity == Owner)) || (Perception.Percepts[key].Entity != Owner))
				&& ((Perception.Percepts[key].Entity.GetComponent<IPerceivable>().Tag & TargetType) != 0)) {
				target = Perception.Percepts [key].Entity;
				break;
			}
		}

		return target;
	}
}
