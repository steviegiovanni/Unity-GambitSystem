using UtilitySystems;

namespace GameSystems.SkillSystem.Database{
	public class SkillSystemDatabase : Singleton<SkillSystemDatabase> {
		private SkillCollectionDatabase _skillCollections;

		private void Awake(){
			transform.SetParent (null);
			DontDestroyOnLoad (this.gameObject);
		}

		static public SkillCollectionDatabase SkillCollections{
			get{
				if (Instance != null) {
					if (Instance._skillCollections == null) {
						Instance._skillCollections = new SkillCollectionDatabase ();
						Instance._skillCollections.LoadDatabase ();
					}
					return Instance._skillCollections;
				}
				return null;
			}
		}
	}
}
