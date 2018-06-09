using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem.Database;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// the base class used to define a collection of skills
	/// </summary>
	public class SkillCollection : MonoBehaviour {
		[SerializeField]
		private int _skillCollectionId = -1;
		public int SkillCollectionId {
			get { return _skillCollectionId;}
			set{ _skillCollectionId = value;}
		}

		private bool _isCollectionSetup = false;
		public bool IsCollectionSetup{
			get { return _isCollectionSetup;}
			set{ _isCollectionSetup = value;}
		}

		private Dictionary<string, Skill> _skillDict;
		public Dictionary<string, Skill> SkillDict{
			get{ 
				if (_skillDict == null)
					_skillDict = new Dictionary<string, Skill> ();
				return _skillDict;
			}
		}
	}
}
