using System;
using UnityEngine;
using UnityEditor;
using GameSystems.PerceptionSystem;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.Effects.Editor{
	public class TargetAOEStatEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(TargetAOEStatEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			TargetAOEStatEffectAsset effectAsset = asset as TargetAOEStatEffectAsset;

			GUILayout.Space (10);

			GUILayout.BeginHorizontal ();
			effectAsset.IncludeTarget = GUILayout.Toggle (effectAsset.IncludeTarget, "Include Target ", GUILayout.Width (150));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Target Type", GUILayout.Width (150));
			effectAsset.TargetType = (int)(PerceptionTags)(EditorGUILayout.EnumFlagsField("Target Type ", (PerceptionTags)effectAsset.TargetType));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Radius", GUILayout.Width (150));
			effectAsset.Radius= EditorGUILayout.FloatField (effectAsset.Radius);
			GUILayout.EndHorizontal ();
		}

		#endregion
	}
}
