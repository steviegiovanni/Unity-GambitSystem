using UtilitySystems.XmlDatabase.Editor;

/// <summary>
/// Helpful methods used for editor scripts that work with Gambit Condition
/// 
/// When adding a new Gambit Condition extension follow these steps:
/// 1) Add the Display name of the new Gambit Condition to the GetNames method.
/// 2) Add a CreateInstance call in CreateAsset for the new Gambit, the
/// case number should be the index of the new Display name in GetNames.
/// 3) If there is an editor extension related add it to the GetExtensions.
/// Order of extension effects in which order the extensions are displayed.
/// </summary>
namespace GameSystems.GambitSystem.Editor{
	static public class GambitConditionEditorUtility{
		/// <summary>
		/// Gets an array containing all extension that can apply to a gambit condition
		/// </summary>
		static public IEditorExtension[] GetExtensions(){
			return new IEditorExtension[] {
				new StatGCEditorExtension()
			};
		}

		/// <summary>
		/// creates an instance of the Gambit Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public GambitConditionAsset CreateAsset(int index){
			switch (index) {
			case 0:
				return new GambitConditionAsset ();
			case 1:
				return new StatLessThanGCAsset ();
			case 2:
				return new StatGreaterThanGCAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// Gets an array of all the names of each Gambit condition type
		/// </summary>
		static public string[] GetNames(){
			return new string[] {
				"GambitCondition",
				"StatLessThan",
				"StatGreaterThan"
			};
		}
	}
}
