using UtilitySystems;
using GameSystems.SkillSystem.Database;
using GameSystems.GambitSystem.Database;

/// <summary>
/// main singleton database that will load the skill collection and the gambit collection
/// </summary>
public class SkillSystemDatabase : Singleton<SkillSystemDatabase> {
	private SkillCollectionDatabase _skillCollections;
	private GambitCollectionDatabase _gambitCollections;

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

	static public GambitCollectionDatabase GambitCollections{
		get{
			if (Instance != null) {
				if (Instance._gambitCollections == null) {
					Instance._gambitCollections = new GambitCollectionDatabase ();
					Instance._gambitCollections.LoadDatabase ();
				}
				return Instance._gambitCollections;
			}
			return null;
		}
	}
}
