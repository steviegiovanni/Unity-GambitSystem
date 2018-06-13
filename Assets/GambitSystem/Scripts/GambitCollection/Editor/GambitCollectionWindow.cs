using UnityEngine;
using UnityEditor;
using System;
using UtilitySystems.XmlDatabase;
using UtilitySystems.XmlDatabase.Editor;
using System.Linq;
using GameSystems.SkillSystem.Database;
using GameSystems.GambitSystem.Database;

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
				GUILayout.EndHorizontal ();

				gambitSelectionScroll = GUILayout.BeginScrollView (gambitSelectionScroll, false,true);

				for (int i = 0; i < asset.Gambits.Count; i++) {
					GambitAsset gambitAsset = asset.Gambits [i];
					GUILayout.BeginHorizontal (EditorStyles.toolbar);
	
					if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
						&& EditorUtility.DisplayDialog ("Remove Gambit", "Are you sure you want to delete the effect?", "Delete", "Cancel")) {
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
								i++;
						}
						string skillLabel = skillFound?gambitAsset.SkillId:"Missing";

						if (GUILayout.Button (gambitAsset.SkillId == ""? "Assign Skill" : skillLabel,EditorStyles.toolbarButton,GUILayout.Width(150))) {
							string [] skillNames = new string[skillCollectionAsset.Skills.Count];
							for (int j = 0; j < skillNames.Length; j++) {
								skillNames [j] = skillCollectionAsset.Skills [j].Name;
							}
							XmlDatabaseEditorUtility.GetGenericMenu(skillNames,(index)=>{
								gambitAsset.SkillId = skillNames[index];
							}).ShowAsContext ();
						}
					}

					GUI.enabled = true;

					GUILayout.EndHorizontal ();

					if (SelectedGambitIndex == i) {

						foreach (var editorExtension in GambitEditorUtility.GetExtensions()) {
							if (editorExtension.CanHandleType (gambitAsset.GetType()))
								editorExtension.OnGUI (gambitAsset);
						}
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
	}
}