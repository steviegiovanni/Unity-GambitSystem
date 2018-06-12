using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;

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
	public HighestEnmityGambit(GameObject owner, int priority, int targetType, bool includeSelf):base(owner, priority,targetType,includeSelf){}

	public HighestEnmityGambit(HighestEnmityGambitAsset asset):base(asset){}

	/// <summary>
	/// Finds the target.
	/// </summary>
	/// <returns>The target.</returns>
	public override GameObject FindTarget ()
	{		
		if (Owner == null)
			return null;
		if (Owner.GetComponent<IHasPerception>() == null)
			return null;

		Perception perception = Owner.GetComponent<IHasPerception> ().Perception;

		GameObject target = null;
		int highestEnmity = 0;
		foreach (var key in perception.Percepts.Keys) {
			if ((perception.Percepts [key].Entity != null) 
				&& (perception.Percepts[key].Enmity >= highestEnmity) 
				&& ((IncludeSelf && (perception.Percepts[key].Entity == Owner)) || (perception.Percepts[key].Entity != Owner))
				&& ((perception.Percepts[key].Entity.GetComponent<IPerceivable>().Tag & TargetType) != 0)){
				highestEnmity = perception.Percepts [key].Enmity;
				target = perception.Percepts [key].Entity;
				break;
			}
		}

		return target;
	}
}
