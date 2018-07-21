using System.Collections;
using UnityEngine;
using GameSystems.SkillSystem;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// gambit that focuses on a position
	/// </summary>
	public class PositionGambit : Gambit{
		/// <summary>
		/// the target position of the gambit
		/// </summary>
		private Vector3 _targetPosition;
		public Vector3 TargetPosition{
			get{ return _targetPosition;}
			set{ _targetPosition = value;}
		}
			
		/// <summary>
		/// constructor with a targetgambitasset as an input
		/// </summary>
		public PositionGambit(PositionGambitAsset asset):base(asset){
			TargetPosition = asset.TargetPosition;
		}

		/// <summary>
		/// override coroutine of this gambit
		/// </summary>
		public override IEnumerator GambitCoroutine(){
			if (Skill == null) {
				Debug.Log ("No skill is associated to this gambit");
				yield return null;
			}

			PositionSkill posSkill = Skill as PositionSkill;
			if (posSkill != null)
				posSkill.Position = TargetPosition;

			while (true) {
				IMovable movableEntity = Owner.GetComponent<IMovable> ();
				if (movableEntity == null)
					Debug.LogWarning ("Owner does not implement IMovable");
				else {
					if (movableEntity.RemainingDistance (TargetPosition) <= Skill.Range) {
						movableEntity.Stopped = true;
						break;
					}else {
						movableEntity.Stopped = false;
						movableEntity.MoveTo (TargetPosition);
					}
				}
				yield return null;
			}
			yield return new WaitForSeconds(Skill.CastTime);
		}
	}
}
