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
			}

			GUILayout.EndVertical ();
		}
		#endregion

		protected override SkillCollectionAsset CreateDefaultAsset(){
			return new SkillCollectionAsset (GetDatabaseInstance ().GetNextHighestId ());
		}
	}
}
