using GameSystems.Effects;
using UnityEngine;

namespace GameSystems.SkillSystem{
	/// <summary>
	/// a skill that requires a position associated to it
	/// </summary>
	public class PositionSkill : Skill, IHasPositionEffects {
		/// <summary>
		/// The position for the skill
		/// </summary>
		private Vector3 _position;
		public Vector3 Position{
			get{ return _position;}
			set{ _position = value;}
		}

		#region IHasPositionEffects implementation

		public Vector3 GetPosition ()
		{
			return Position;
		}

		#endregion

		/// <summary>
		/// constructor with skill asset as input
		/// </summary>
		public PositionSkill(SkillAsset skillAsset): base(skillAsset){}
	}
}
