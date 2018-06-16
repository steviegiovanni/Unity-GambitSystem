using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.GambitSystem{
	/// <summary>
	/// base class for gambit condition
	/// </summary>
	public class GambitCondition : MonoBehaviour {
		/// <summary>
		/// The gambit collection.
		/// </summary>
		private GambitCollection _gambitCollection;
		public GambitCollection GambitCollection{
			get{return _gambitCollection;}
			set{_gambitCollection = value;}
		}

		/// <summary>
		/// get the status of the condition
		/// </summary>
		public virtual bool GetStatus(){
			return true;
		}

		public GambitCondition(GambitConditionAsset asset){
		}
	}
}
