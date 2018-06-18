using System.Xml;
using UtilitySystems.XmlDatabase;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// target gambit asset that will create a target gambit that 
	/// targets entity with the highest enmity 
	/// </summary>
	public class HighestEnmityGambitAsset : GambitAsset {
		public override Gambit CreateInstance(){
			return new HighestEnmityGambit (this);
		}
	}
}