using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Entity : MonoBehaviour, IPerceivable, IUseCooldown, IMovable {
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
	private NavMeshAgent _movementComp;

	public NavMeshAgent MovementComponent{
		get{ 
			if (_movementComp == null) 
				_movementComp = GetComponent<NavMeshAgent> ();
			if (_movementComp == null)
				_movementComp = this.gameObject.AddComponent<NavMeshAgent> ();
			return _movementComp;
		}
	}

	#region IMovable implementation

	public void MoveTo (Vector3 targetPos)
	{
		MovementComponent.SetDestination (targetPos);
	}

	public void StopMove ()
	{
		MovementComponent.isStopped = true;
	}
		
	public float RemainingDistance (Vector3 targetPos)
	{
		if (!MovementComponent.hasPath) 
			return Vector3.Distance(this.transform.position,targetPos);

		return MovementComponent.remainingDistance;
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
