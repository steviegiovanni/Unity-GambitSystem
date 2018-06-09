using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UtilitySystems.XmlDatabase;

namespace GameSystems.SkillSystem.Database{
	public class SkillCollectionAsset : XmlDatabaseAsset {
		#region implemented abstract members of XmlDatabaseAsset
		public override void OnSaveAsset (XmlWriter writer)
		{
			throw new NotImplementedException ();
		}
		public override void OnLoadAsset (XmlReader reader)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}
