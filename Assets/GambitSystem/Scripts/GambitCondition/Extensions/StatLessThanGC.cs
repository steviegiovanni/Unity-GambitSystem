﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;

namespace GameSystems.GambitSystem{
	public class StatLessThanGC : StatGambitCondition {
		public StatLessThanGC (StatLessThanGCAsset asset) : base (asset){}

		public override bool GetStatus(){
			if (Owner == null)
				return false;

			IHasStats ownerStat = Owner.GetComponent<IHasStats> ();
			float curValue;
			if (!ownerStat.TryGetStatPercentValue (StatName, out curValue))
				return false;
			else
				return curValue < StatValue;
		}
	}
}
