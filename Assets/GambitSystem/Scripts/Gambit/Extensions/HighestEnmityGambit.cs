using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// extension to the target gambit class that will target the highest enmity instance on the table of percepts
/// </summary>
public class HighestEnmityGambit : TargetGambit {
	/// <summary>
	/// Initializes a new instance of the <see cref="HighestEnmityGambit"/> class.
	/// </summary>
	/// <param name="owner">Owner.</param>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="target">Target.</param>
	/// <param name="perception">Perception.</param>
	public HighestEnmityGambit(GameObject owner, int priority, Skill skill, GameObject target, Perception perception):base(owner, priority,skill,target,perception){}

	/// <summary>
	/// Finds the target.
	/// </summary>
	/// <returns>The target.</returns>
	public override GameObject FindTarget ()
	{
		if (Owner == null)
			return null;
		if (Perception == null)
			return null;

		GameObject target = null;
		int highestEnmity = 0;
		foreach (var key in Perception.Percepts.Keys) {
			if ((Perception.Percepts [key].Entity != null) && (Perception.Percepts[key].Enmity >= highestEnmity) && (Perception.Percepts[key].Entity != Owner)){
				highestEnmity = Perception.Percepts [key].Enmity;
				target = Perception.Percepts [key].Entity;
				break;
			}
		}

		return target;
	}
}
