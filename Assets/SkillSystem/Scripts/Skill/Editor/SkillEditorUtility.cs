using UtilitySystems.XmlDatabase.Editor;

/// <summary>
/// Helpful methods used for editor scripts that work with SkillSystem
/// 
/// When adding a new Skill extension follow these steps:
/// 1) Add the Display name of the new Skill to the GetNames method.
/// 2) Add a CreateInstance call in CreateAsset for the new Skill, the
/// case number should be the index of the new Display name in GetNames.
/// 3) Id there is an editor extension related add it to the GetExtensions.
/// Order of extension effects in which order the extensions are displayed.
/// </summary>
namespace GameSystems.SkillSystem.Editor{
	static public class SkillEditorUtility{
		/// <summary>
		/// Gets an array containing all extension that can apply to a skill
		/// </summary>
		static public IEditorExtension[] GetExtensions(){
			return new IEditorExtension[] {
				new TargetSkillEditorExtension()
			};
		}

		/// <summary>
		/// creates an instance of the Skill Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public SkillAsset CreateAsset(int index){
			switch (index) {
			case 0:
				return new SkillAsset ();
			case 1:
				return new TargetSkillAsset ();
			case 2:
				return new PositionSkillAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// Gets an array of all the names of each Skill type
		/// </summary>
		static public string[] GetNames(){
			return new string[] {
				"Skill",
				"TargetableSkill",
				"PositionSkill"
			};
		}
	}
}
