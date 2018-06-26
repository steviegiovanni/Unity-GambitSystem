using UnityEngine;
using UnityEditor;
using System;
using UtilitySystems.XmlDatabase;
using UtilitySystems.XmlDatabase.Editor;
using System.Linq;
using GameSystems.SkillSystem.Database;
using GameSystems.GambitSystem.Database;
using GameSystems.GambitSystem;
using GameSystems.SkillSystem;
using System.Collections.Generic;

namespace GameSystems.GambitSystem.Editor{
	public class GambitCollectionWindow : XmlDatabaseWindowComplex<GambitCollectionAsset> {
		private Vector2 gambitSelectionScroll = Vector2.zero;
		//private float gambitSelectionWidth = 400;

		private int _selectedGambitIndex = -1;
		public int SelectedGambitIndex{
			get{
				return _selectedGambitIndex;
			}
			set{
				if (_selectedGambitIndex != value) {
					_selectedGambitIndex = value;
					EditorGUI.FocusTextInControl (string.Empty);
				}
			}
		}

		[MenuItem("Window/Game Systems/Skills/Gambit Collections")]
		static public void ShowWindow(){
			var wnd = GetWindow<GambitCollectionWindow> ();
			wnd.titleContent.text = "Gambit Collections";
			wnd.Show ();
		}

		#region implemented abstract members of XmlDatabaseWindow
		protected override AbstractXmlDatabase<GambitCollectionAsset> GetDatabaseInstance ()
		{
			return GambitCollectionDatabase.Instance;
		}
		protected override void DisplayAssetGUI (GambitCollectionAsset asset)
		{
			GUILayout.BeginVertical ();

			var selectedCollection = GambitCollectionDatabase.Instance.Get (SelectedAssetId);
			if (selectedCollection != null) {
				GUILayout.Label (selectedCollection.Name, EditorStyles.toolbarButton);

				GUILayout.BeginHorizontal ();
				GUILayout.Label ("ID: " + selectedCollection.Id.ToString () + ", Name ", GUILayout.Width (100));
				selectedCollection.Name = EditorGUILayout.TextField (selectedCollection.Name);
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Skill Collection ", GUILayout.Width (100));

				SkillCollectionAsset skillCollectionAsset = SkillCollectionDatabase.Instance.Get (asset.SkillCollectionId);
				if (GUILayout.Button (skillCollectionAsset==null?"Assign Skill Collection":skillCollectionAsset.Name)) {
					XmlDatabaseEditorUtility.ShowContext (SkillCollectionDatabase.Instance,(skillColAsset)=>{
						if(EditorUtility.DisplayDialog ("Change skill collection", "Are you sure you want to change the associated skill collection? Changing skill collection might result in loss of skill data in gambits.", "Change", "Cancel"))
						asset.SkillCollectionId = skillColAsset.Id;
					});
				}

				GUILayout.EndHorizontal ();

				GUILayout.BeginVertical (EditorStyles.helpBox);

				GUILayout.BeginHorizontal ();
				GUILayout.Label ("",EditorStyles.boldLabel,GUILayout.Width (30));
				GUILayout.Label ("Priority",EditorStyles.boldLabel,GUILayout.Width (100));
				GUILayout.Label ("Type",EditorStyles.boldLabel,GUILayout.Width (200));
				GUILayout.Label ("Skill",EditorStyles.boldLabel,GUILayout.Width (150));
				GUILayout.Label ("Use",EditorStyles.boldLabel,GUILayout.Width (100));
				GUILayout.EndHorizontal ();

				gambitSelectionScroll = GUILayout.BeginScrollView (gambitSelectionScroll, false,true);

				for (int i = 0; i < asset.Gambits.Count; i++) {
					GambitAsset gambitAsset = asset.Gambits [i];
					GUILayout.BeginHorizontal (EditorStyles.toolbar);
	
					if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
						&& EditorUtility.DisplayDialog ("Remove Gambit", "Are you sure you want to delete the gambit?", "Delete", "Cancel")) {
						asset.Gambits.RemoveAt (i);
					}

					gambitAsset.Priority = EditorGUILayout.IntField (gambitAsset.Priority,GUILayout.Width (100));

					bool clicked = GUILayout.Toggle (i == SelectedGambitIndex, gambitAsset.GetType ().Name, ToggleButtonStyle, GUILayout.Width(200));
					if (clicked != (i == SelectedGambitIndex)) {
						if (clicked) {
							SelectedGambitIndex = i;
							GUI.FocusControl (null);
						} else {
							SelectedGambitIndex = -1;
						}
					}

					if (skillCollectionAsset == null) {
						GUI.enabled = false;
						GUILayout.Button ("None", EditorStyles.toolbarButton,GUILayout.Width (150));
					}else {
						GUI.enabled = true;

						bool skillFound = false;
						int iterator = 0;
						while (!skillFound && (iterator < skillCollectionAsset.Skills.Count)) {
							if (skillCollectionAsset.Skills [iterator].Name == gambitAsset.SkillId) {
								skillFound = true;
								break;
							} else
								iterator++;
						}
						string skillLabel = skillFound?gambitAsset.SkillId:"Missing";

						if (GUILayout.Button (gambitAsset.SkillId == ""? "Assign Skill" : skillLabel,EditorStyles.toolbarButton,GUILayout.Width(150))) {
							List<string> skillList = new List<string> ();
							if (typeof(PositionGambitAsset).IsAssignableFrom (gambitAsset.GetType ())) {
								for (int j = 0; j < skillCollectionAsset.Skills.Count; j++) {
									if (typeof(PositionSkillAsset).IsAssignableFrom (skillCollectionAsset.Skills [j].GetType ()))
										skillList.Add (skillCollectionAsset.Skills [j].Name);
								}
							} else {
								for (int j = 0; j < skillCollectionAsset.Skills.Count; j++) {
									if (typeof(TargetSkillAsset).IsAssignableFrom (skillCollectionAsset.Skills [j].GetType ()))
										skillList.Add (skillCollectionAsset.Skills [j].Name);
								}
							}

							string [] skillNames = skillList.ToArray();
							XmlDatabaseEditorUtility.GetGenericMenu(skillNames,(index)=>{
								gambitAsset.SkillId = skillNames[index];
							}).ShowAsContext ();
						}
					}

					GUI.enabled = true;

					gambitAsset.MaxUse = EditorGUILayout.IntField (gambitAsset.MaxUse);

					GUILayout.EndHorizontal ();

					if (SelectedGambitIndex == i) {
						foreach (var editorExtension in GambitEditorUtility.GetExtensions()) {
							if (editorExtension.CanHandleType (gambitAsset.GetType()))
								editorExtension.OnGUI (gambitAsset);
						}

						DisplayGambitGUI (gambitAsset);
					}
				}

				GUILayout.FlexibleSpace ();
				GUILayout.EndScrollView();

				if(GUILayout.Button("Add Gambit", EditorStyles.toolbarButton)){
					XmlDatabaseEditorUtility.GetGenericMenu (GambitEditorUtility.GetNames (), (index) => {
						var gambitAsset = GambitEditorUtility.CreateAsset (index);
						asset.Gambits.Add (gambitAsset);
						EditorWindow.FocusWindowIfItsOpen<GambitCollectionWindow> ();
					}).ShowAsContext ();
				}

				GUILayout.EndVertical ();
			}

			GUILayout.EndVertical ();
		}
		#endregion

		protected void DisplayGambitGUI(GambitAsset gambitAsset){
			GUILayout.BeginVertical ();
			for (int i = 0; i < gambitAsset.Conditions.Count; i++) {
				var gambitCondition = gambitAsset.Conditions [i];
				GUILayout.BeginHorizontal ();
				GUILayout.Space (20);

				if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
					&& EditorUtility.DisplayDialog ("Remove Condition", "Are you sure you want to delete the condition?", "Delete", "Cancel")) {
					gambitAsset.Conditions.RemoveAt (i);
				}

				GUILayout.Label(gambitCondition.GetType().Name,EditorStyles.toolbarButton, GUILayout.Width (200));

				foreach (var editorExtension in GambitConditionEditorUtility.GetExtensions()) {
					if (editorExtension.CanHandleType (gambitCondition.GetType ()))
						editorExtension.OnGUI (gambitCondition);
				}

				GUILayout.EndHorizontal ();
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			if(GUILayout.Button("Add Condition", EditorStyles.toolbarButton)){
				XmlDatabaseEditorUtility.GetGenericMenu (GambitConditionEditorUtility.GetNames (), (index) => {
					var gambitConditionAsset = GambitConditionEditorUtility.CreateAsset (index);
					gambitAsset.Conditions.Add (gambitConditionAsset);
					EditorWindow.FocusWindowIfItsOpen<GambitCollectionWindow> ();
				}).ShowAsContext ();
			}
			GUILayout.EndHorizontal ();

			GUILayout.EndVertical ();
		}
	}
}