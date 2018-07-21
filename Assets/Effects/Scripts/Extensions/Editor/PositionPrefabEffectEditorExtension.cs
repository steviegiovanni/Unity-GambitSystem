using System;
using UnityEngine;
using UnityEditor;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.Effects.Editor{
	public class PositionPrefabEffectEditorExtension : EditorExtension {
		#region implemented abstract members of EditorExtension

		public override bool CanHandleType (Type type)
		{
			return typeof(PositionPrefabEffectAsset).IsAssignableFrom (type);
		}

		public override void OnGUI (object asset)
		{
			PositionPrefabEffectAsset effectAsset = asset as PositionPrefabEffectAsset;

			GUILayout.BeginHorizontal();
			GUILayout.Label ("Prefab", GUILayout.Width (150));
			effectAsset.Prefab = EditorGUILayout.ObjectField(effectAsset.Prefab,typeof(UnityEngine.Object),false);
			effectAsset.PrefabName = (effectAsset.Prefab != null)?(effectAsset.Prefab.name+".prefab"):"";
			GUILayout.EndHorizontal ();
		}

		#endregion


	}
}
