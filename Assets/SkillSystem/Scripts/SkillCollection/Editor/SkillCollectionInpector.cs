using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using UtilitySystems.XmlDatabase.Editor;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameSystems.SkillSystem.Editor{
	[CustomEditor(typeof(SkillCollection))]
	public class SkillCollectionInpector : UnityEditor.Editor {
		public override bool RequiresConstantRepaint ()
		{
			return true;
		}

		public override void OnInspectorGUI(){
			var collection = (SkillCollection)target;
			DisplayCollectionAssetGUI (collection);
			GUILayout.Space (4);
			DisplayCollectionGUI (collection);
		}

		public void DisplayCollectionAssetGUI(SkillCollection collection){
			var asset = SkillCollectionDatabase.Instance.Get (collection.SkillCollectionId);
			string displayText;
			// if the asset is found, use its name
			if (asset != null) {
				displayText = asset.Name;
			}
			// if the id is below 0 no collection is assigned
			else if (collection.SkillCollectionId <= 0) {
				displayText = "Not set";
			}
			// if no asset is assigned and the id is above zero
			// previous collection is missing
			else {
				displayText = "Missing";
			}

			GUILayout.Space (4);

			// show the collection's name and id and allow user to change 
			// the assigned stat collection via a dialogue pop up
			EditorGUI.BeginDisabledGroup(Application.isPlaying);
			if(GUILayout.Button(string.Format("[ID: {0}] {1}", Mathf.Max(0, collection.SkillCollectionId).ToString(), displayText),EditorStyles.toolbarPopup)){
				SkillCollectionDatabase.Instance.LoadDatabase ();
				XmlDatabaseEditorUtility.ShowContext (SkillCollectionDatabase.Instance, (value) => {
					collection.SkillCollectionId = value.Id;
				}, typeof(SkillCollectionWindow));
			}
			EditorGUI.EndDisabledGroup ();
		}

		public void DisplayCollectionGUI(SkillCollection collection){
			GUILayout.BeginVertical ();
			GUILayout.Label ("Skills");
			foreach (var skill in collection.SkillDict.Values) {
				Rect r = EditorGUILayout.BeginVertical (GUILayout.Height(20));
				float progress = skill.Cooldown > 0.0f ? (skill.CurrentCooldown / skill.Cooldown) : 1.0f;
				EditorGUI.ProgressBar (r, progress, skill.Name);
				GUILayout.FlexibleSpace ();
				EditorGUILayout.EndVertical ();
				GUILayout.Space (4);
			}
			GUILayout.EndVertical ();
		}
	}
}
