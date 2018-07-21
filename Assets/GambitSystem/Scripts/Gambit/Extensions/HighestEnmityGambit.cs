using UnityEngine;
using GameSystems.SkillSystem;
using GameSystems.PerceptionSystem;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// extension to the target gambit class that will target the highest enmity instance on the table of percepts
	/// </summary>
	public class HighestEnmityGambit : TargetGambit {
		public HighestEnmityGambit(HighestEnmityGambitAsset asset):base(asset){}

		/// <summary>
		/// Finds the target.
		/// </summary>
		public override GameObject FindTarget ()
		{		
			if (Owner == null)
				return null;
			
			if (Owner.GetComponent<IHasPerception>() == null)
				return null;
			Perception perception = Owner.GetComponent<IHasPerception> ().Perception;

			TargetSkill skill = Skill as TargetSkill;
			if (skill == null)
				return null;

			GameObject target = null;
			int highestEnmity = 0;
			foreach (var key in perception.Percepts.Keys) {
				if ((perception.Percepts [key].Entity != null) 
					&& (perception.Percepts[key].Enmity >= highestEnmity) 
					&& ((skill.IncludeSelf && (perception.Percepts[key].Entity == Owner)) || (perception.Percepts[key].Entity != Owner))
					&& ((perception.Percepts[key].Entity.GetComponent<IPerceivable>().Tag & skill.TargetType) != 0)){
					highestEnmity = perception.Percepts [key].Enmity;
					target = perception.Percepts [key].Entity;
					break;
				}
			}

			return target;
		}
	}
}
