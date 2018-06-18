using UtilitySystems.XmlDatabase.Editor;

/// <summary>
/// Helpful methods used for editor scripts that work with SkillSystem
/// 
/// When adding a new Gambit extension follow these steps:
/// 1) Add the Display name of the new Gambit to the GetNames method.
/// 2) Add a CreateInstance call in CreateAsset for the new Gambit, the
/// case number should be the index of the new Display name in GetNames.
/// 3) If there is an editor extension related add it to the GetExtensions.
/// Order of extension effects in which order the extensions are displayed.
/// </summary>
namespace GameSystems.GambitSystem.Editor{
	static public class GambitEditorUtility{
		/// <summary>
		/// Gets an array containing all extension that can apply to a gambit
		/// </summary>
		static public IEditorExtension[] GetExtensions(){
			return new IEditorExtension[] {
				new PositionGambitEditorExtension()
			};
		}

		/// <summary>
		/// creates an instance of the Gambit Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public GambitAsset CreateAsset(int index){
			switch (index) {
			case 0:
				return new HighestEnmityGambitAsset ();
			case 1:
				return new PositionGambitAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// Gets an array of all the names of each Gambit type
		/// </summary>
		static public string[] GetNames(){
			return new string[] {
				"HighestEnmityGambit",
				"PositionGambit"
			};
		}
	}
}
