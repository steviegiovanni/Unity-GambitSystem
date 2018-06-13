using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameSystems.SkillSystem;
using GameSystems.Perception;

/// <summary>
/// Entity class. A test class that implements all of the required interfaces
/// to be able to make use of the SkillSystem
/// </summary>
[System.Serializable]
public class Entity : MonoBehaviour, IPerceivable, IMovable, IHasPerception {
	/// <summary>
	/// the perception component. stores all seen entity along with their enmity
	/// </summary>
	[SerializeField]
	private Perception _perception;

	#region IHasPerception implementation

	public Perception Perception{
		get{
			if (_perception == null)
				_perception = GetComponent<Perception> ();
			if (_perception == null)
				_perception = this.gameObject.AddComponent<Perception> ();
			return _perception;
		}
	}

	#endregion

	/// <summary>
	/// the tag of the entity (e.g. none, ally, or enemy)
	/// </summary>
	[SerializeField]
	private PerceptionTags _tag;

	#region ITargetable implementation
	public int Tag {
		get{ return (int)_tag;}
		set{ _tag = (PerceptionTags)value;}
	}
	#endregion

	/// <summary>
	/// movement component of an entity, using navmesh for this example
	/// </summary>
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
	/// <summary>
	/// used by a gambit to move entities to a target position
	/// </summary>
	public void MoveTo (Vector3 targetPos)
	{
		MovementComponent.SetDestination (targetPos);
	}

	/// <summary>
	/// used by a gambit to tell the entity to stop moving
	/// </summary>
	public void StopMove ()
	{
		MovementComponent.isStopped = true;
	}

	/// <summary>
	/// used by a gambit in its coroutine to query whether entity is close enough 
	/// to a target position
	/// </summary>
	public float RemainingDistance (Vector3 targetPos)
	{
		if (!MovementComponent.hasPath) 
			return Vector3.Distance(this.transform.position,targetPos);

		return MovementComponent.remainingDistance;
	}
	#endregion

	/// <summary>
	/// a reference to a gambit collection associated to a this entity
	/// </summary>
	[SerializeField]
	private GambitCollection _gambitCollection;

	public GambitCollection GambitCollection{
		get{ 
			if(_gambitCollection == null)
				_gambitCollection = GetComponent<GambitCollection> ();
			return _gambitCollection;
		}
	}
		

	// Use this for initialization
	void Start () {
		// associate functions to be called when perception becomes either alerted or unalerted
		if (Perception != null) {
			Perception.OnAlerted.AddListener (OnAlerted);
			Perception.OnUnalerted.AddListener (OnUnalerted);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// make every entity broadcasts an event to the other entities every frame
		PerceptionEVManager.TriggerEvent ("PERCEPTION", new Hashtable (){ { "OBJECT",this.gameObject } });
	}

	/// <summary>
	/// when perception is alerted, enable gambit
	/// </summary>
	void OnAlerted(){
		if(GambitCollection != null)
			GambitCollection.enabled = true;
	}

	/// <summary>
	/// when perception is unalerted, disable gambit
	/// </summary>
	void OnUnalerted(){
		if(GambitCollection != null)
			GambitCollection.enabled = false;
	}
}
