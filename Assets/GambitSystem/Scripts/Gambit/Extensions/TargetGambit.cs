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
		/// include self when finding a target
		/// </summary>
		private bool _includeSelf;
		public bool IncludeSelf{
			get{ return _includeSelf;}
			set{ _includeSelf = value;}
		}

		/// <summary>
		/// The type of the target.
		/// </summary>
		private int _targetType;
		public int TargetType{
			get{ return _targetType;}
			set{ _targetType = value;}
		}

		/// <summary>
		/// The target
		/// </summary>
		private GameObject _target;
		public GameObject Target{
			get{ return _target;}
			set{ _target = value;}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetGambit"/> class.
		/// </summary>
		public TargetGambit():base(){
			TargetType = 2;
			Target = null;
			IncludeSelf = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetFirstGambit"/> class.
		/// </summary>
		public TargetGambit(GameObject owner, int priority, int targetType, bool includeSelf):base(owner, priority){
			TargetType = targetType;
			Target = null;
			IncludeSelf = includeSelf;
		}

		/// <summary>
		/// constructor with a targetgambitasset as an input
		/// </summary>
		public TargetGambit(TargetGambitAsset asset):base(asset){
			IncludeSelf = asset.IncludeSelf;
			TargetType = asset.TargetType;
		}

		/// <summary>
		/// override coroutine of this gambit
		/// </summary>
		public override IEnumerator GambitCoroutine(){
			if (Skill == null) {
				Debug.Log ("No skill is associated to this gambit");
				yield return null;
			}

			while (true) {
				Debug.Log ("shalala");
				Target = FindTarget ();
				if (Target == null) { // can't find any target
					break;
				}else if (Target != null) {
					IMovable movableEntity = Owner.GetComponent<IMovable> ();
					Debug.Log ("hmmmmmm....");
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

			Perception perception = Owner.GetComponent<IHasPerception> ().Perception;

			GameObject target = null;
			foreach (var key in perception.Percepts.Keys) {
				if ((perception.Percepts [key].Entity != null) 
					&& ((IncludeSelf && (perception.Percepts[key].Entity == Owner)) || (perception.Percepts[key].Entity != Owner))
					&& ((perception.Percepts[key].Entity.GetComponent<IPerceivable>().Tag & TargetType) != 0)) {
					target = perception.Percepts [key].Entity;
					break;
				}
			}

			return target;
		}
	}
}
