using UnityEngine;
using UnityEditor;
using System;
using UtilitySystems.XmlDatabase;
using UtilitySystems.XmlDatabase.Editor;
using System.Linq;
using GameSystems.SkillSystem.Database;

namespace GameSystems.SkillSystem.Editor{
	public class SkillCollectionWindow : XmlDatabaseWindowComplex<SkillCollectionAsset> {
		private Vector2 skillSelectionScroll = Vector2.zero;
		private float skillSelectionWidth = 200;

		private int _selectedSkillIndex = -1;
		public int SelectedSkillIndex{
			get{
				return _selectedSkillIndex;
			}
			set{
				if (_selectedSkillIndex != value) {
					_selectedSkillIndex = value;
					EditorGUI.FocusTextInControl (string.Empty);
				}
			}
		}

		[MenuItem("Window/Game Systems/Skills/Skill Collections")]
		static public void ShowWindow(){
			var wnd = GetWindow<SkillCollectionWindow> ();
			wnd.titleContent.text = "Skill Collections";
			wnd.Show ();
		}

		#region implemented abstract members of XmlDatabaseWindow
		protected override AbstractXmlDatabase<SkillCollectionAsset> GetDatabaseInstance ()
		{
			return SkillCollectionDatabase.Instance;
		}
		protected override void DisplayAssetGUI (SkillCollectionAsset asset)
		{
			GUILayout.BeginVertical ();

			var selectedCollection = SkillCollectionDatabase.Instance.Get (SelectedAssetId);
			if (selectedCollection != null) {
				GUILayout.Label (selectedCollection.Name, EditorStyles.toolbarButton);

				GUILayout.BeginHorizontal ();
				GUILayout.Label ("ID: " + selectedCollection.Id.ToString () + ", Name ", GUILayout.Width (100));
				selectedCollection.Name = EditorGUILayout.TextField (selectedCollection.Name);
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				DisplaySkillSelectionContent (selectedCollection);
				GUILayout.EndHorizontal ();
			}

			GUILayout.EndVertical ();
		}
		#endregion

		private void DisplaySkillSelectionContent(SkillCollectionAsset asset){
			GUILayout.BeginVertical (GUILayout.Width (200));

			// Scroll view for the listed assets
			skillSelectionScroll = GUILayout.BeginScrollView(skillSelectionScroll,false,true);

			// list all the skills in the collection
			GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true));

			foreach (var skill in asset.Skills) {
				string displayText = skill.Name;
				int skillIndex = asset.Skills.IndexOf (skill);

				var select = GUILayout.Toggle (skillIndex == SelectedSkillIndex, displayText, EditorStyles.toolbarButton);
				if (select == true) {
					SelectedSkillIndex = skillIndex;
				}
			}

			if (asset.Skills.Count <= 0) {
				GUILayout.Label ("No skills created\nClick '+' to add a new Skill.", EditorStyles.centeredGreyMiniLabel);
			}

			GUILayout.FlexibleSpace ();
			GUILayout.EndVertical ();

			GUILayout.EndScrollView ();

			GUILayout.EndVertical ();
		}

		private void DisplaySkillSelectionFooter(){
			var selectedCollection = SkillCollectionDatabase.Instance.Get (SelectedAssetId, true);
			if (selectedCollection != null) {
				GUILayout.BeginHorizontal (GUILayout.Width (200));
				if(GUILayout.Button("+", EditorStyles.toolbarButton)){
					XmlDatabaseEditorUtility.GetGenericMenu (SkillEditorUtility.GetNames (), (index) => {
						var skillAsset = SkillEditorUtility.CreateAsset (index);
						selectedCollection.Skills.Add (skillAsset);

						SelectedSkillIndex = selectedCollection.Skills.Count - 1;
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				}
				if (GUILayout.Button ("-", EditorStyles.toolbarButton)
				   && EditorUtility.DisplayDialog ("Delete Skill", "Are you sure you want to delete the " + "selected stat?", "Delete", "Cancel")) {
					if (SelectedSkillIndex >= 0 && SelectedSkillIndex < selectedCollection.Skills.Count) {
						selectedCollection.Skills.RemoveAt (SelectedSkillIndex--);
						if (SelectedSkillIndex == -1 && selectedCollection.Skills.Count > 0) {
							SelectedSkillIndex = 0;
						}
					}
				}
				GUILayout.Label ("", EditorStyles.toolbarButton, GUILayout.Width (15));
				GUILayout.EndHorizontal ();
			}
		}

		protected override void OnDisplayFooter(){
			DisplaySkillSelectionFooter ();
			base.OnDisplayFooter ();
		}

		protected override SkillCollectionAsset CreateDefaultAsset(){
			return new SkillCollectionAsset (GetDatabaseInstance ().GetNextHighestId ());
		}
	}
}
