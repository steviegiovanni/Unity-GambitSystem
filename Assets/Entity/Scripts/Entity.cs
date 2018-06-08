using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour, IPerceivable, IUseCooldown {
	[SerializeField]
	private GambitTags _tag;

	#region ITargetable implementation
	public int Tag {
		get{ return (int)_tag;}
		set{ _tag = (GambitTags)value;}
	}
	#endregion

	[SerializeField]
	private float _cooldown = 10.0f;

	#region IUseCooldown implementation

	public float Cooldown {
		get {return _cooldown;}
		set {_cooldown = value;}
	}

	public void ResetCooldown ()
	{
		Cooldown = 0.0f;
	}

	#endregion

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
		Cooldown += Time.deltaTime;
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
