using UnityEngine;
using UnityEditor;
using UtilitySystems.XmlDatabase;
using UtilitySystems.XmlDatabase.Editor;
using System.Linq;
using GameSystems.SkillSystem.Database;
using GameSystems.SkillSystem;
using GameSystems.Editor;

namespace GameSystems.SkillSystem.Editor{
	public class SkillCollectionWindow : XmlDatabaseWindowComplex<SkillCollectionAsset> {
		private Vector2 skillSelectionScroll = Vector2.zero;
		private Vector2 skillEffectScroll = Vector2.zero;
		private Vector2 skillPrerequisiteScroll = Vector2.zero;
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

		private int _selectedEffectIndex = -1;
		public int SelectedEffectIndex{
			get{ return _selectedEffectIndex;}
			set{ 
				if (_selectedEffectIndex != value) {
					_selectedEffectIndex = value;
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

				if (SelectedSkillIndex >= 0 && SelectedSkillIndex < selectedCollection.Skills.Count) {
					DisplaySelectedSkillContent (selectedCollection.Skills [SelectedSkillIndex]);
				}

				GUILayout.EndHorizontal ();
			}

			GUILayout.EndVertical ();
		}
		#endregion

		private void DisplaySkillSelectionContent(SkillCollectionAsset asset){
			GUILayout.BeginVertical (GUILayout.Width (skillSelectionWidth));

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
				   && EditorUtility.DisplayDialog ("Delete Skill", "Are you sure you want to delete the selected skill?", "Delete", "Cancel")) {
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

		private void DisplaySelectedSkillContent(SkillAsset skill){
			GUILayout.BeginVertical ();
			GUILayout.BeginVertical (EditorStyles.helpBox);
			GUILayout.Label ("Parameters",EditorStyles.boldLabel);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Skill Name", GUILayout.Width (100));
			skill.Name = EditorGUILayout.TextField (skill.Name);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Cooldown", GUILayout.Width (100));
			skill.Cooldown = EditorGUILayout.FloatField (skill.Cooldown);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Cast Time", GUILayout.Width (100));
			skill.CastTime = EditorGUILayout.FloatField (skill.CastTime);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Range", GUILayout.Width (100));
			skill.Range = EditorGUILayout.FloatField (skill.Range);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Duration", GUILayout.Width (100));
			skill.Delay = EditorGUILayout.FloatField (skill.Delay);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Interruptable", GUILayout.Width (100));
			skill.Interruptable = EditorGUILayout.Toggle(skill.Interruptable);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Unlocked Level", GUILayout.Width (100));
			skill.RequiredLevel = EditorGUILayout.IntField(skill.RequiredLevel);
			GUILayout.EndHorizontal ();

			foreach (var extension in SkillEditorUtility.GetExtensions()) {
				if (extension.CanHandleType (skill.GetType()))
					extension.OnGUI (skill);
			}
			GUILayout.EndVertical ();

			GUILayout.BeginVertical (EditorStyles.helpBox,GUILayout.Height(200));
			GUILayout.Label ("Prerequisites",EditorStyles.boldLabel);

			skillPrerequisiteScroll = GUILayout.BeginScrollView (skillPrerequisiteScroll, false,true);

			for (int i = 0; i < skill.Prerequisites.Count; i++) {
				SkillPrerequisiteAsset prerequisite = skill.Prerequisites [i];
				GUILayout.BeginHorizontal (EditorStyles.toolbar);
				if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
					&& EditorUtility.DisplayDialog ("Remove prerequisite", "Are you sure you want to delete the prerequisite?", "Delete", "Cancel")) {
					skill.Prerequisites.RemoveAt (i);
				}
				GUILayout.Label ("StatName", GUILayout.Width (100));
				prerequisite.StatName = EditorGUILayout.TextField (prerequisite.StatName);
				GUILayout.Label ("StatValue", GUILayout.Width (100));
				prerequisite.StatValue = EditorGUILayout.IntField (prerequisite.StatValue);
				GUILayout.EndHorizontal ();
			}

			GUILayout.FlexibleSpace ();
			GUILayout.EndScrollView();

			if(GUILayout.Button("Add Prerequisite", EditorStyles.toolbarButton)){
				var newPrerequisite = new SkillPrerequisiteAsset ();
				skill.Prerequisites.Add (newPrerequisite);
			}

			GUILayout.EndVertical ();

			GUILayout.BeginVertical (EditorStyles.helpBox);
			GUILayout.Label ("Effects",EditorStyles.boldLabel);
			skillEffectScroll = GUILayout.BeginScrollView (skillEffectScroll, false,true);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("",EditorStyles.boldLabel,GUILayout.Width (30));
			GUILayout.Label ("Type",EditorStyles.boldLabel,GUILayout.Width (150));
			GUILayout.Label ("Delay",EditorStyles.boldLabel,GUILayout.Width (50));
			GUILayout.EndHorizontal ();

			for(int i = 0; i < skill.Effects.Count; i++){
			//foreach(var effect in skill.Effects){
				EffectAsset effect = skill.Effects[i];
				GUILayout.BeginHorizontal (EditorStyles.toolbar);

				if (GUILayout.Button ("-", EditorStyles.toolbarButton, GUILayout.Width (30))
				   && EditorUtility.DisplayDialog ("Remove effect", "Are you sure you want to delete the effect?", "Delete", "Cancel")) {
					skill.Effects.RemoveAt (i);
				}
					
				bool clicked = GUILayout.Toggle (i == SelectedEffectIndex, effect.GetType ().Name, ToggleButtonStyle, GUILayout.Width(150));
				if (clicked != (i == SelectedEffectIndex)) {
					if (clicked) {
						SelectedEffectIndex = i;
						GUI.FocusControl (null);
					} else {
						SelectedEffectIndex = -1;
					}
				}

				effect.Delay = EditorGUILayout.FloatField (effect.Delay,GUILayout.Width (50));
				GUILayout.EndHorizontal ();


				if (SelectedEffectIndex == i) {
					GUILayout.BeginVertical (EditorStyles.helpBox);
					foreach (var extension in EffectEditorUtility.GetExtensions()) {
						if (extension.CanHandleType (effect.GetType()))
							extension.OnGUI (effect);
					}
					GUILayout.EndVertical ();
				}
			}

			//Object source = null;
			//source = EditorGUILayout.ObjectField(source, typeof(Object), true);
			//AssetDatabase.GetAssetPath (source);


			GUILayout.FlexibleSpace ();
			GUILayout.EndScrollView();

			if(GUILayout.Button("Add Effect", EditorStyles.toolbarButton)){
				if (typeof(PositionSkillAsset).IsAssignableFrom (skill.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetPositionEffectNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreatePositionEffectAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				} else if (typeof(TargetSkillAsset).IsAssignableFrom (skill.GetType ())) {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetTargetEffectNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreateTargetEffectAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				} else {
					XmlDatabaseEditorUtility.GetGenericMenu (EffectEditorUtility.GetNames (), (index) => {
						var skillEffectAsset = EffectEditorUtility.CreateAsset (index);
						skill.Effects.Add (skillEffectAsset);
						EditorWindow.FocusWindowIfItsOpen<SkillCollectionWindow> ();
					}).ShowAsContext ();
				}
			}

			GUILayout.EndVertical ();
			GUILayout.EndVertical ();

		}

		protected override SkillCollectionAsset CreateDefaultAsset(){
			return new SkillCollectionAsset (GetDatabaseInstance ().GetNextHighestId ());
		}
	}
}
