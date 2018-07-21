using UtilitySystems.XmlDatabase;

namespace GameSystems.Entities.Database{
	/// <summary>
	/// Gambit collection database
	/// </summary>
	public class EntityDatabase :  AbstractXmlDatabase<EntityAsset>{
		static private EntityDatabase _instance = null;
		static public EntityDatabase Instance{
			get{
				if(_instance == null){
					_instance = new EntityDatabase();
					_instance.LoadDatabase ();
				}
				return _instance;
			}
		}

		#region implemented abstract members of AbstractXmlDatabase
		public override EntityAsset CreateAssetOfType (string type)
		{
			if (typeof(EntityAsset).Name == type) {
				return new EntityAsset ();
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
				return "EntityDatabase.xml";
			}
		}
		#endregion
	}
}
