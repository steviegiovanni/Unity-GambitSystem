using System;
using UnityEngine;
using UnityEditor;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class TargetSkillEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetSkillAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetSkillAsset skillAsset = asset as TargetSkillAsset;
			GUILayout.Space (10);

			GUILayout.BeginHorizontal ();
			skillAsset.IncludeSelf = GUILayout.Toggle (skillAsset.IncludeSelf, "Include self ", GUILayout.Width (150));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Type", GUILayout.Width (100));
			skillAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField((PerceptionTags)skillAsset.TargetType));
			GUILayout.EndHorizontal ();
		}

		#endregion


	}
}
