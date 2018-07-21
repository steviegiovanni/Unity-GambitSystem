using UnityEngine;
using UnityEditor;
using GameSystems.Entities.Database;
using UtilitySystems.XmlDatabase.Editor;

namespace GameSystems.Entities.Editor{
	[CustomEditor(typeof(Entity))]
	public class EntityInspector : UnityEditor.Editor {
		public override bool RequiresConstantRepaint ()
		{
			return true;
		}

		public override void OnInspectorGUI(){
			var entity = (Entity)target;
			DisplayCollectionAssetGUI (entity);
			GUILayout.Space (4);
			base.DrawDefaultInspector ();
			//DisplayCollectionGUI (entity);
		}

		public void DisplayCollectionAssetGUI(Entity collection){
			var asset = EntityDatabase.Instance.Get (collection.EntityId);
			string displayText;
			// if the asset is found, use its name
			if (asset != null) {
				displayText = asset.Name;
			}
			// if the id is below 0 no collection is assigned
			else if (collection.EntityId <= 0) {
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
			GUILayout.Label ("Entity:");
			if(GUILayout.Button(string.Format("[ID: {0}] {1}", Mathf.Max(0, collection.GambitCollectionId).ToString(), displayText),EditorStyles.toolbarPopup)){
				EntityDatabase.Instance.LoadDatabase ();
				XmlDatabaseEditorUtility.ShowContext (EntityDatabase.Instance, (value) => {
					collection.GambitCollectionId = value.Id;
				}, typeof(EntityWindow));
			}

			EditorGUI.EndDisabledGroup ();
		}
	}
}
