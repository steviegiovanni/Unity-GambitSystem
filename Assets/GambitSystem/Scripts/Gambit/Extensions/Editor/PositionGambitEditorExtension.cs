using System;
using UnityEngine;
using UnityEditor;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.GambitSystem.Editor{
	public class PositionGambitEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(PositionGambitAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			PositionGambitAsset gambitAsset = asset as PositionGambitAsset;
			GUILayout.BeginHorizontal ();
			float x = gambitAsset.TargetPosition.x;
			float y = gambitAsset.TargetPosition.y;
			float z = gambitAsset.TargetPosition.z;
			GUILayout.Label ("X ", EditorStyles.toolbarButton);
			x = EditorGUILayout.FloatField (x);
			GUILayout.Label ("Y ", EditorStyles.toolbarButton);
			y = EditorGUILayout.FloatField (y);
			GUILayout.Label ("Z ", EditorStyles.toolbarButton);
			z = EditorGUILayout.FloatField (z);
			gambitAsset.TargetPosition = new Vector3 (x, y, z);
			GUILayout.EndHorizontal ();
		}

		#endregion


	}
}
