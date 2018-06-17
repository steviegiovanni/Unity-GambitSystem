using System.Xml;
using UtilitySystems.XmlDatabase;
using System.Collections;
using System.Collections.Generic;

namespace GameSystems.GambitSystem{
	public class StatLessThanGCAsset : StatGambitConditionAsset {
		public override GambitCondition CreateInstance(){
			return new StatLessThanGC (this);
		}
	}
}