using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour, ITargetable {
	#region ITargetable implementation
	public int TargetType {
		get{ return (int)_entityTag;}
		set{ _entityTag = (GambitTags)value;}
	}
	#endregion

	[SerializeField]
	private GambitTags _entityTag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PerceptionEVManager.TriggerEvent ("PERCEPTION", new Hashtable (){ { "OBJECT",this.gameObject } });
	}
}
