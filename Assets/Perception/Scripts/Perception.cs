using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace GameSystems.PerceptionSystem{
	/// <summary>
	/// the perception component. holds a list of percepts.
	/// listens to perception events
	/// </summary>
	public class Perception : MonoBehaviour{
		/// <summary>
		/// The list of managed percepts. key is the object instance id
		/// </summary>
		private Dictionary<int,Percept> _percepts;
		public Dictionary<int, Percept> Percepts{
			get{
				if (_percepts == null)
					_percepts = new Dictionary<int,Percept> ();
				return _percepts;
			}
		}

		/// <summary>
		/// whether this perception component is alerted or not
		/// </summary>
		[SerializeField]
		private bool _alerted = false;
		public bool Alerted{
			get{ return _alerted;}
			set{ _alerted = value;}
		}

		/// <summary>
		/// the range in which objects will cause perception component to be alerted
		/// </summary>
		[SerializeField]
		private float _range;
		public float Range{
			get{ return _range;}
			set{ _range = value;}
		}
			
		/// <summary>
		/// The alert mask which determines whether a percept causes the component to be alerted
		/// </summary>
		[SerializeField]
		private int _alertMask = 0xFFFF;
		public int AlertMask{
			get{ return _alertMask;}
			set{ _alertMask = value;}
		}

		/// <summary>
		/// trigerred when alerted
		/// </summary>
		private UnityEvent _onAlerted;
		public UnityEvent OnAlerted{
			get{ 
				if (_onAlerted == null)
					_onAlerted = new UnityEvent ();
				return _onAlerted;
				}
		}

		/// <summary>
		/// trigerred when alerted
		/// </summary>
		private UnityEvent _onUnalerted;
		public UnityEvent OnUnalerted{
			get{ 
				if (_onUnalerted == null)
					_onUnalerted = new UnityEvent ();
				return _onUnalerted;
			}
		}

		void Update(){
			// cleanup missing entity due to being destroyed
			var perceptKeys = Percepts.Keys.ToArray();
			bool alerted = false;
			for (int i = 0; i < perceptKeys.Length; i++) {
				GameObject entity = Percepts [perceptKeys [i]].Entity;
				if (entity == null)
					Percepts.Remove (perceptKeys [i]);
				else {
					// keeps track whether this perception component should be alerted or not
					if ((Vector3.Distance (entity.transform.position,this.transform.position) <= Range)
					    && ((entity.GetComponent<IPerceivable> ().Tag & AlertMask) != 0))
						alerted = true;
				}
			}

			//invoke alert and unalerted accordingly
			if (alerted && (OnAlerted != null) && !Alerted) {
				Alerted = alerted;
				OnAlerted.Invoke ();
			} else if (!alerted && (OnUnalerted != null) && Alerted) {
				Alerted = alerted;
				OnUnalerted.Invoke ();
			}
		}

		/// <summary>
		/// on enable, start listening to perception events
		/// </summary>
		void OnEnable(){
			PerceptionEVManager.StartListening ("PERCEPTION", Perceived);
		}

		/// <summary>
		/// On disable, stop listening to perception events
		/// </summary>
		void OnDisable(){
			Alerted = false;
			PerceptionEVManager.StopListening ("PERCEPTION", Perceived);
		}

		/// <summary>
		/// the function called when a perception event is received
		/// </summary>
		void Perceived(Hashtable param){
			if (!param.ContainsKey ("OBJECT"))
				return;

			GameObject perceivedObject = (GameObject)(param ["OBJECT"]);
			Percept percept;
			if (Percepts.TryGetValue (perceivedObject.GetInstanceID(), out percept)) {
				Percepts [perceivedObject.GetInstanceID()].Entity = perceivedObject;
			}else {
				IPerceivable perceivable = perceivedObject.GetComponent<IPerceivable> ();
				if(perceivable != null)
					Percepts.Add (perceivedObject.GetInstanceID(), new Percept(perceivedObject,0));
			}
		}
	}
}
