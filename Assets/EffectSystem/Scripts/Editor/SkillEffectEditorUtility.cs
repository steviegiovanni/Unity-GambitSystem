﻿using UtilitySystems.XmlDatabase.Editor;

/// <summary>
/// Helpful methods used for editor scripts that work with SkillEffect
/// 
/// When adding a new Skill Effect extension follow these steps:
/// 1) Add the Display name of the new Skill Effect to the GetNames method.
/// 2) Add a CreateInstance call in CreateAsset for the new Skill Effect, the
/// case number should be the index of the new Display name in GetNames.
/// 3) Id there is an editor extension related add it to the GetExtensions.
/// Order of extension effects in which order the extensions are displayed.
/// </summary>
namespace GameSystems.SkillSystem.Editor{
	static public class SkillEffectEditorUtility{
		/// <summary>
		/// Gets an array containing all extension that can apply to a skill
		/// </summary>
		static public IEditorExtension[] GetExtensions(){
			return new IEditorExtension[] {
				new StatGlobalEffectEditorExtension(),
				new PositionPrefabEffectEditorExtension(),
				new TargetPrefabEffectEditorExtension()
			};
		}

		/// <summary>
		/// creates an instance of the Skill Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public EffectAsset CreateAsset(int index){
			switch (index) {
			case 0:
				return new EffectAsset ();
			case 1:
				return new StatGlobalEffectAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// creates an instance of the Skill Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public EffectAsset CreatePositionEffectAsset(int index){
			switch (index) {
			case 0:
				return new EffectAsset ();
			case 1:
				return new PositionEffectAsset ();
			case 2:
				return new StatGlobalEffectAsset ();
			case 3:
				return new PositionAOEStatEffectAsset ();
			case 4:
				return new PositionPrefabEffectAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// creates an instance of the Skill Asset. the index
		/// relates to the position of the asset's name within the array
		/// gotten from GetName() method
		/// </summary>
		static public EffectAsset CreateTargetEffectAsset(int index){
			switch (index) {
			case 0:
				return new EffectAsset ();
			case 1:
				return new TargetEffectAsset ();
			case 2:
				return new StatGlobalEffectAsset ();
			case 3:
				return new TargetStatEffectAsset ();
			case 4:
				return new TargetAOEStatEffectAsset ();
			case 5:
				return new TargetPrefabEffectAsset ();
			default:
				return null;
			}
		}

		/// <summary>
		/// Gets an array of all the names of each Skill type
		/// </summary>
		static public string[] GetNames(){
			return new string[] {
				"Effect",
				"StatGlobalEffect",
			};
		}

		/// <summary>
		/// Gets an array of all the names of each Skill type
		/// </summary>
		static public string[] GetPositionEffectNames(){
			return new string[] {
				"Effect",
				"PositionEffect",
				"StatGlobalEffect",
				"PositionAOEStatEffect",
				"PositionPrefabEffect"
			};
		}

		/// <summary>
		/// Gets an array of all the names of each Skill type
		/// </summary>
		static public string[] GetTargetEffectNames(){
			return new string[] {
				"Effect",
				"TargetEffect",
				"StatGlobalEffect",
				"TargetStatEffect",
				"TargetAOEStatEffect",
				"TargetPrefabEffect"
			};
		}
	}
}