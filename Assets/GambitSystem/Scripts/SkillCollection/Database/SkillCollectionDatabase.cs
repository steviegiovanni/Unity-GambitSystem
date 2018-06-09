using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem.Database{
	public class SkillCollectionDatabase : AbstractXmlDatabase<SkillCollectionAsset> {
		static private SkillCollectionDatabase _instance = null;
		static public SkillCollectionDatabase Instance{
			get{
				if(_instance == null){
					_instance = new SkillCollectionDatabase();
					_instance.LoadDatabase ();
				}
				return _instance;
			}
		}

		#region implemented abstract members of AbstractXmlDatabase
		public override SkillCollectionAsset CreateAssetOfType (string type)
		{
			if (typeof(SkillCollectionAsset).Name == type) {
				return new SkillCollectionAsset ();
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
				return "SkillCollectionDatabase.xml";
			}
		}
		#endregion
	}
}
