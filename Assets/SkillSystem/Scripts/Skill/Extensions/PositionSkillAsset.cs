namespace GameSystems.SkillSystem{
	/// <summary>
	/// Positional skill asset, derived from based skill asset
	/// </summary>
	public class PositionSkillAsset : SkillAsset {
		public override Skill CreateInstance(){
			return new PositionSkill (this);
		}
	}
}
