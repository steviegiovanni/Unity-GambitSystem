namespace GameSystems.PerceptionSystem{
	/// <summary>
	/// an entity implementing this interface should be able to return
	/// a perception component
	/// </summary>
	public interface IHasPerception{
		Perception Perception{ get;}
	}
}
