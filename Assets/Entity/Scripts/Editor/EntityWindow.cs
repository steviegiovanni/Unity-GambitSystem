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
using GameSystems.EntitySystem;
using GameSystems.EntitySystem.Database;
using System.Collections.Generic;

namespace GameSystems.EntitySystem.Editor{
	public class EntityWindow : XmlDatabaseWindowSimple<EntityAsset> {
		private EntityDatabase _database = null;

		[MenuItem("Window/Game Systems/Skills/Entity Database")]
		static public void ShowWindow(){
			var wnd = GetWindow<EntityWindow> ();
			wnd.titleContent.text = "Entities";
			wnd.Show ();
		}

		#region implemented abstract members of XmlDatabaseWindow
		protected override AbstractXmlDatabase<EntityAsset> GetDatabaseInstance ()
		{
			if (_database == null) {
				_database = new EntityDatabase ();
				_database.LoadDatabase ();
			}

			return _database;
		}
		protected override void DisplayAssetGUI (EntityAsset asset)
		{
			GUILayout.BeginVertical("Box");

			GUILayout.BeginHorizontal();

			//asset.Icon = (Sprite)EditorGUILayout.ObjectField(asset.Icon, typeof(Sprite), false,
			//	GUILayout.Width(72), GUILayout.Height(72));

			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Name", GUILayout.Width(100));
			asset.Name = EditorGUILayout.TextField(asset.Name);

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Gambit Collection ", GUILayout.Width (100));

			GambitCollectionAsset gambitCollectionAsset = GambitCollectionDatabase.Instance.Get (asset.GambitCollectionId);
			if (GUILayout.Button (gambitCollectionAsset==null?"Assign Gambit Collection":gambitCollectionAsset.Name)) {
				XmlDatabaseEditorUtility.ShowContext (GambitCollectionDatabase.Instance,(gambitColAsset)=>{
					if(EditorUtility.DisplayDialog ("Change gambit collection", "Are you sure you want to change the associated gambit collection? Changing gambit collection might result in unsynchronized skill collection", "Change", "Cancel"))
						asset.GambitCollectionId = gambitColAsset.Id;
				});
			}

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

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("WTF", GUILayout.Width(100));
			asset.Description = EditorGUILayout.TextField(asset.Description);
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}
		#endregion
	}
}