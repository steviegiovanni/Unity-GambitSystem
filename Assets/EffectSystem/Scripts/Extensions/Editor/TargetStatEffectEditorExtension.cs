using System;
using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class TargetStatEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetStatEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetStatEffectAsset effectAsset = asset as TargetStatEffectAsset;

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Stat Base", GUILayout.Width (100));
			effectAsset.StatBase= EditorGUILayout.TextField (effectAsset.StatBase);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Stat", GUILayout.Width (100));
			effectAsset.TargetStat= EditorGUILayout.TextField (effectAsset.TargetStat);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Flat Value", GUILayout.Width (100));
			effectAsset.FlatValue= EditorGUILayout.IntField (effectAsset.FlatValue);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Modifier", GUILayout.Width (100));
			effectAsset.Modifier= EditorGUILayout.FloatField (effectAsset.Modifier);
			GUILayout.EndHorizontal ();
		}

		#endregion
	}
}
