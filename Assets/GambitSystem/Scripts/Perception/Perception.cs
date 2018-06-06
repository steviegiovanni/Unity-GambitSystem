using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percept{
	/// <summary>
	/// The entity.
	/// </summary>
	private GameObject _entity;

	/// <summary>
	/// The enmity.
	/// </summary>
	private int _enmity;

	/// <summary>
	/// Gets or sets the entity.
	/// </summary>
	/// <value>The entity.</value>
	public GameObject Entity{
		get{ return _entity;}
		set{ _entity = value;}
	}

	/// <summary>
	/// Gets or sets the enmity.
	/// </summary>
	/// <value>The enmity.</value>
	public int Enmity{
		get{ return _enmity;}
		set{ _enmity = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Percept"/> class.
	/// </summary>
	public Percept(){
		Entity = null;
		Enmity = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Percept"/> class.
	/// </summary>
	/// <param name="entity">Entity.</param>
	/// <param name="enmity">Enmity.</param>
	public Percept(GameObject entity, int enmity){
		Entity = entity;
		Enmity = enmity;
	}
}

public class Perception : MonoBehaviour{
	/// <summary>
	/// The percepts.
	/// </summary>
	private Dictionary<int,Percept> _percepts;

	/// <summary>
	/// Gets the percepts.
	/// </summary>
	/// <value>The percepts.</value>
	public Dictionary<int, Percept> Percepts{
		get{
			if (_percepts == null)
				_percepts = new Dictionary<int,Percept> ();
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
		Percept percept;
		if (Percepts.TryGetValue (perceivedObject.GetInstanceID(), out percept)) {
			Percepts [perceivedObject.GetInstanceID()].Entity = perceivedObject;
		}else {
			Percepts.Add (perceivedObject.GetInstanceID(), new Percept(perceivedObject,0));
		}
	}
}
