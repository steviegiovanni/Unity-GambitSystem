using System;
using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class TargetableSkillEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetableSkillAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetableSkillAsset skillAsset = asset as TargetableSkillAsset;
			GUILayout.BeginHorizontal ();
			skillAsset.IncludeSelf = GUILayout.Toggle (skillAsset.IncludeSelf, "Include self ", GUILayout.Width (150));
			skillAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField("Target Type ", (PerceptionTags)skillAsset.TargetType));
			GUILayout.EndHorizontal ();
		}

		#endregion


	}
}
