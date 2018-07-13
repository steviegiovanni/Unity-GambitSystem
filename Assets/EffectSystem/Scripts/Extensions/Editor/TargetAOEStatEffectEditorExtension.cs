using System;
using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class TargetAOEStatEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetAOEStatEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetAOEStatEffectAsset effectAsset = asset as TargetAOEStatEffectAsset;

			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			effectAsset.IncludeTarget = GUILayout.Toggle (effectAsset.IncludeTarget, "Include Target ", GUILayout.Width (150));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			effectAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField("Target Type ", (PerceptionTags)effectAsset.TargetType));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Radius", GUILayout.Width (100));
			effectAsset.Radius= EditorGUILayout.FloatField (effectAsset.Radius);
			GUILayout.EndHorizontal ();
		}

		#endregion
	}
}
