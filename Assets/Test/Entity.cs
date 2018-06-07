using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour {

	[SerializeField]
	private GambitTags _entityTag;

	public int EntityTag{
		get{ return (int)_entityTag;}
		set{ _entityTag = (GambitTags)value;}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PerceptionEVManager.TriggerEvent ("PERCEPTION", new Hashtable (){ { "OBJECT",this.gameObject } });
	}
}
