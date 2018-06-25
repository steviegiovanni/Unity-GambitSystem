using System;
using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class StatGlobalEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(StatGlobalEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			StatGlobalEffectAsset effectAsset = asset as StatGlobalEffectAsset;
			GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			effectAsset.IncludeSelf = GUILayout.Toggle (effectAsset.IncludeSelf, "Include self ", GUILayout.Width (150));
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			effectAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField("Target Type ", (PerceptionTags)effectAsset.TargetType));
			GUILayout.EndHorizontal ();
			GUILayout.EndVertical ();
		}

		#endregion


	}
}
