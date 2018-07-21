using UtilitySystems.XmlDatabase;

namespace GameSystems.GambitSystem.Database{
	/// <summary>
	/// Gambit collection database
	/// </summary>
	public class GambitCollectionDatabase :  AbstractXmlDatabase<GambitCollectionAsset>{
		static private GambitCollectionDatabase _instance = null;
		static public GambitCollectionDatabase Instance{
			get{
				if(_instance == null){
					_instance = new GambitCollectionDatabase();
					_instance.LoadDatabase ();
				}
				return _instance;
			}
		}

		#region implemented abstract members of AbstractXmlDatabase
		public override GambitCollectionAsset CreateAssetOfType (string type)
		{
			if (typeof(GambitCollectionAsset).Name == type) {
				return new GambitCollectionAsset ();
			}
			return null;
		}
		public override string DatabasePath {
			get {
				return "Databases/SkillSystem/";
			}
		}
		public override string DatabaseName {
			get {
				return "GambitCollectionDatabase.xml";
			}
		}
		#endregion
	}
}
