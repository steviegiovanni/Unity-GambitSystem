using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;

namespace GameSystems.GambitSystem{
	public class StatLessThanGC : StatGambitCondition {
		public StatLessThanGC (StatLessThanGCAsset asset) : base (asset){}

		public override bool GetStatus(){
			Debug.Log ("tralalala");
			if (Owner == null)
				return false;
			Debug.Log ("budum");

			IHasStats ownerStat = Owner.GetComponent<IHasStats> ();
			return ownerStat.GetStatPercentValue (StatName) < StatValue;
		}
	}
}
