using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.SkillSystem{
	public class SkillPrerequisite {
		private string _statName;
		public string StatName{
			get{ return _statName;}
			set{ _statName = value;}
		}

		private int _statValue;
		public int StatValue{
			get{ return _statValue;}
			set{ _statValue = value;}
		}

		public SkillPrerequisite(SkillPrerequisiteAsset asset){
			StatName = asset.StatName;
			StatValue = asset.StatValue;
		}

		public void ApplyPrerequisite(IHasStats affected){
			Debug.Log ("applying prerequisite");
			affected.ModifyStat (StatName, -StatValue);
		}

		public bool CheckPrerequisite(IHasStats statOwner){
			int statValue = 0;
			if(statOwner.TryGetStatValue(StatName,out statValue)){
				if (statValue >= StatValue)
					return true;
				else
					return false;
			}else
				return false;
		}
	}
}