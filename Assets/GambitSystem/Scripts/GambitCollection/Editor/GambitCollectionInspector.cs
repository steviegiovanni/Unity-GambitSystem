using UnityEngine;
using UnityEditor;
using GameSystems.SkillSystem.Database;
using GameSystems.SkillSystem.Editor;
using GameSystems.GambitSystem.Database;
using UtilitySystems.XmlDatabase.Editor;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GameSystems.GambitSystem.Editor{
	[CustomEditor(typeof(GambitCollection))]
	public class GambitCollectionInspector : UnityEditor.Editor {
		public override bool RequiresConstantRepaint ()
		{
			return true;
		}

		public override void OnInspectorGUI(){
			var collection = (GambitCollection)target;
			DisplayCollectionAssetGUI (collection);
			GUILayout.Space (4);
			DisplayCollectionGUI (collection);
		}

		public void DisplayCollectionAssetGUI(GambitCollection collection){
			var asset = GambitCollectionDatabase.Instance.Get (collection.GambitCollectionId);
			string displayText;
			// if the asset is found, use its name
			if (asset != null) {
				displayText = asset.Name;
			}
			// if the id is below 0 no collection is assigned
			else if (collection.GambitCollectionId <= 0) {
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
			GUILayout.Label ("Gambit Collection:");
			if(GUILayout.Button(string.Format("[ID: {0}] {1}", Mathf.Max(0, collection.GambitCollectionId).ToString(), displayText),EditorStyles.toolbarPopup)){
				GambitCollectionDatabase.Instance.LoadDatabase ();
				XmlDatabaseEditorUtility.ShowContext (GambitCollectionDatabase.Instance, (value) => {
					collection.GambitCollectionId = value.Id;
				}, typeof(GambitCollectionWindow));
			}

			EditorGUI.EndDisabledGroup ();
		}

		public void DisplayCollectionGUI(GambitCollection collection){
			GUILayout.BeginVertical ();
			//GUILayout.Label ("Skills");
			Color curColor = GUI.color;

			for(int i=0; i < collection.Gambits.Count; i++){
				GUILayout.BeginHorizontal ();
				if (collection.ActiveGambitId == i)
					GUI.color = Color.cyan;
				else if (collection.Gambits [i].IsReady)
					GUI.color = Color.white;
				else
					GUI.color = Color.gray;
				/*Rect r = EditorGUILayout.BeginVertical (GUILayout.Height(20));
				float progress = skill.Cooldown > 0.0f ? (skill.CurrentCooldown / skill.Cooldown) : 1.0f;
				EditorGUI.ProgressBar (r, progress, skill.Name);
				GUILayout.FlexibleSpace ();
				EditorGUILayout.EndVertical ();*/
				GUILayout.Label (collection.Gambits[i].Priority.ToString(),EditorStyles.toolbarButton,GUILayout.Width(50));
				GUILayout.Label ("Use: "+collection.Gambits[i].UsageNumber.ToString()+"/"+((collection.Gambits[i].MaxUse==-1)?".INF":collection.Gambits[i].MaxUse.ToString()),EditorStyles.toolbarButton,GUILayout.Width(100));
				GUILayout.Label (collection.Gambits[i].GetType().Name,EditorStyles.toolbarButton,GUILayout.Width(200));
				GUILayout.Label (collection.Gambits[i].SkillId,EditorStyles.toolbarButton,GUILayout.Width(200));
				//GUILayout.Space (4);
				GUILayout.EndHorizontal ();
			}

			GUI.color = curColor;
			GUILayout.EndVertical ();
		}
	}
}
