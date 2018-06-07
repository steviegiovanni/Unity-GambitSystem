using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour, IPerceivable {
	#region ITargetable implementation
	public int Tag {
		get{ return (int)_tag;}
		set{ _tag = (GambitTags)value;}
	}
	#endregion

	[SerializeField]
	private GambitTags _tag;

	[SerializeField]
	private GambitCollection _gambitCollection;

	public GambitCollection GambitCollection{
		get{ 
			if(_gambitCollection == null)
				_gambitCollection = GetComponent<GambitCollection> ();
			return _gambitCollection;
		}
	}

	[SerializeField]
	private Perception _perception;

	public Perception Perception{
		get{
			if (_perception == null)
				_perception = GetComponent<Perception> ();
			return _perception;
		}
	}

	// Use this for initialization
	void Start () {
		if (Perception != null) {
			Perception.OnAlerted.AddListener (OnAlerted);
			Perception.OnUnalerted.AddListener (OnUnalerted);
		}
	}
	
	// Update is called once per frame
	void Update () {
		PerceptionEVManager.TriggerEvent ("PERCEPTION", new Hashtable (){ { "OBJECT",this.gameObject } });
	}

	void OnAlerted(){
		if(GambitCollection != null)
			GambitCollection.enabled = true;
	}

	void OnUnalerted(){
		if(GambitCollection != null)
			GambitCollection.enabled = false;
	}
}
