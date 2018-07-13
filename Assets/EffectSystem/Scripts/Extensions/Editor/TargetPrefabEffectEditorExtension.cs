using System;
using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class TargetPrefabEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetPrefabEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetPrefabEffectAsset effectAsset = asset as TargetPrefabEffectAsset;
			GUILayout.BeginHorizontal();
			GUILayout.Space (20);
			effectAsset.Prefab = EditorGUILayout.ObjectField("Prefab ",effectAsset.Prefab, typeof(UnityEngine.Object),false);
			effectAsset.PrefabName = effectAsset.Prefab.name+".prefab";
			GUILayout.EndHorizontal ();
		}

		#endregion


	}
}
