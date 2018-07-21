using System;
using UnityEngine;
using UnityEditor;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.GambitSystem.Editor{
	public class StatGCEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(StatGambitConditionAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			StatGambitConditionAsset gambitConditionAsset = asset as StatGambitConditionAsset;
			GUILayout.Label ("Name ", EditorStyles.toolbarButton,GUILayout.Width(50));
			gambitConditionAsset.StatName = GUILayout.TextField(gambitConditionAsset.StatName, GUILayout.Width (100));
			GUILayout.Label ("Value ", EditorStyles.toolbarButton,GUILayout.Width(50));
			gambitConditionAsset.StatValue = EditorGUILayout.FloatField(gambitConditionAsset.StatValue,GUILayout.Width(50));
		}

		#endregion


	}
}
