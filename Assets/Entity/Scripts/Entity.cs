using System.Collections;
using System.Collections.Generic;
using GameSystems;
using GameSystems.EntitySystem.Database;
using GameSystems.GambitSystem;
using GameSystems.PerceptionSystem;
using GameSystems.SkillSystem;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Entity class. A test class that implements all of the required interfaces
/// to be able to make use of the SkillSystem
/// </summary>
[System.Serializable]
public class Entity : MonoBehaviour, IPerceivable, IMovable, IHasPerception, IHasStats, IHasLevel {
	//======================================= begin test parameters
	[SerializeField]
	private int _health = 100;
	public int Health{
		get{ return _health;}
		set{ _health = value;}
	}

	[SerializeField]
	private int _maxHealth = 100;
	public int MaxHealth{
		get{ return _maxHealth;}
		set{ _maxHealth = value;}
	}

	#region IHasStats implementation


	public float GetStatPercentValue (string statName)
	{
		return (float)Health / (float)MaxHealth;
	}

	public bool TryGetStatPercentValue (string statName, out float statValue){
		statValue = (float)Health / (float)MaxHealth;
		return true;
	}

	public void ModifyStat(string statName,float modifier, int flatValue, int baseValue){
		Health += (int)(modifier * baseValue + (float)flatValue); 
	}

	public void ModifyStat(string statName,int value){
		Health += value; 
	}

	public int GetStatValue(string statName){
		return Health;
	}

	public bool TryGetStatValue(string statName, out int value){
		value = Health;
		return true;
	}

	#endregion

	//======================================== end test parameters

	/// <summary>
	/// level of this entity
	/// </summary>
	private RPGEntityLevel _rpgEntityLevel;
	public RPGEntityLevel EntityLevel{
		get{
			if (_rpgEntityLevel == null) 
				_rpgEntityLevel = GetComponent<RPGEntityLevel> ();
			return _rpgEntityLevel;
		}
	}

	#region IHasLevel implementation

	public int GetLevel ()
	{
		if (EntityLevel == null)
			return 1000;
		else
			return EntityLevel.Level;
	}

	#endregion

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

	#region IPerceivable implementation
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

	public bool Stopped{
		get{ return MovementComponent.isStopped; }
		set{ MovementComponent.isStopped = value;}
	}

	/// <summary>
	/// used by a gambit in its coroutine to query whether entity is close enough 
	/// to a target position
	/// </summary>
	public float RemainingDistance (Vector3 targetPos)
	{
		if (!MovementComponent.hasPath || (MovementComponent.destination != targetPos)) 
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

		// test levelling
		if (Input.GetKeyUp (KeyCode.Space)) {
			if (EntityLevel != null)
				EntityLevel.ModifyExp (50);
		}
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

	/// <summary>
	/// entity id
	/// </summary>
	[SerializeField]
	private int _entityId = -1;
	public int EntityId {
		get { return _entityId;}
		set{ _entityId = value;}
	}

	/// <summary>
	/// gambit collection id
	/// </summary>
	[SerializeField]
	private int _gambitCollectionId = -1;
	public int GambitCollectionId {
		get { return _gambitCollectionId;}
		set{ _gambitCollectionId = value;}
	}

	/// <summary>
	/// skill collection id
	/// </summary>
	[SerializeField]
	private int _skillCollectionId = -1;
	public int SkillCollectionId {
		get { return _skillCollectionId;}
		set{ _skillCollectionId = value;}
	}

	public void Setup(EntityAsset asset){
		SkillCollectionId = asset.SkillCollectionId;
		GambitCollectionId = asset.GambitCollectionId;
		GetComponent<SkillCollection> ().SkillCollectionId = asset.SkillCollectionId;
		GetComponent<SkillCollection> ().SetupCollection ();
		GetComponent<GambitCollection> ().GambitCollectionId = asset.GambitCollectionId;
		GetComponent<GambitCollection> ().SetupCollection ();
		Perception.AlertMask = asset.AlertMask;
		Perception.Range = asset.Vision;
		Tag = asset.Tag;
	}
}
