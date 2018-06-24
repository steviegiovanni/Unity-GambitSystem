using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;
using GameSystems.PerceptionSystem;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// gambit that looks for a target
	/// </summary>
	public class TargetGambit : Gambit{
		/// <summary>
		/// The target
		/// </summary>
		private GameObject _target;
		public GameObject Target{
			get{ return _target;}
			set{ _target = value;}
		}

		/// <summary>
		/// constructor with a targetgambitasset as an input
		/// </summary>
		public TargetGambit(GambitAsset asset):base(asset){}

		/// <summary>
		/// override coroutine of this gambit
		/// </summary>
		public override IEnumerator GambitCoroutine(){
			if (Skill == null) {
				Debug.Log ("No skill is associated to this gambit");
				yield return null;
			}

			while (true) {
				Target = FindTarget ();
				if (Target == null) { // can't find any target
					break;
				}else if (Target != null) {
					IMovable movableEntity = Owner.GetComponent<IMovable> ();
					if (movableEntity == null)
						Debug.LogWarning ("Owner does not implement IMovable");
					else {
						if (movableEntity.RemainingDistance (Target.transform.position) <= Skill.Range) {
							movableEntity.Stopped = true;

							// set up skill target
							ITargetableSkill targetableSkill = Skill as ITargetableSkill;
							if (targetableSkill == null)
								Debug.LogWarning ("calling a non targetable skill from a target gambit");
							else
								targetableSkill.Target = Target;
							break;
						}else {
							Debug.Log ("target set right?");
							movableEntity.Stopped = false;
							movableEntity.MoveTo (Target.transform.position);
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
		public virtual GameObject FindTarget ()
		{
			if (Owner.GetComponent<IHasPerception>() == null)
				return null;
			TargetableSkill skill = Skill as TargetableSkill;
			if (skill == null)
				return null;

			Perception perception = Owner.GetComponent<IHasPerception> ().Perception;

			GameObject target = null;
			foreach (var key in perception.Percepts.Keys) {
				if ((perception.Percepts [key].Entity != null) 
					&& ((skill.IncludeSelf && (perception.Percepts[key].Entity == Owner)) || (perception.Percepts[key].Entity != Owner))
					&& ((perception.Percepts[key].Entity.GetComponent<IPerceivable>().Tag & skill.TargetType) != 0)) {
					target = perception.Percepts [key].Entity;
					break;
				}
			}

			return target;
		}
	}
}
