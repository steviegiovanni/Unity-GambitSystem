using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;

namespace GameSystems.SkillSystem{
	public class GambitAsset : IXmlOnSaveAsset, IXmlOnLoadAsset {
		public int Priority { get; set;}
		public SkillAsset Skill{ get; set;}
		public int SkillId{ get; set;}

		public virtual Gambit CreateInstance(){
			return new Gambit (this);
		}

		#region IXmlOnLoadAsset implementation

		public void OnLoadAsset (XmlReader reader)
		{
			throw new System.NotImplementedException ();
		}

		#endregion

		#region IXmlOnSaveAsset implementation
		public void OnSaveAsset (XmlWriter writer)
		{
			throw new System.NotImplementedException ();
		}
		#endregion
	}
}
