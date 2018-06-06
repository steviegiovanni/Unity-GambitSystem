using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour{
	/// <summary>
	/// The percepts.
	/// </summary>
	private Dictionary<int,GameObject> _percepts;

	/// <summary>
	/// Gets the percepts.
	/// </summary>
	/// <value>The percepts.</value>
	public Dictionary<int, GameObject> Percepts{
		get{
			if (_percepts == null)
				_percepts = new Dictionary<int,GameObject> ();
			return _percepts;
		}
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable(){
		EventManager.StartListening ("PERCEPTION", Perceived);
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable(){
		EventManager.StopListening ("PERCEPTION", Perceived);
	}

	// percept is detected
	void Perceived(Hashtable param){
		if (!param.ContainsKey ("OBJECT"))
			return;

		GameObject perceivedObject = (GameObject)(param ["OBJECT"]);
		GameObject dummyObject;
		if (Percepts.TryGetValue (perceivedObject.GetInstanceID(), out dummyObject)) {
			//Debug.Log (string.Format("{0} perceived", perceivedObject.name));
			Percepts [perceivedObject.GetInstanceID()] = perceivedObject;
		}else {
			Percepts.Add (perceivedObject.GetInstanceID(), perceivedObject);
		}
	}
}
