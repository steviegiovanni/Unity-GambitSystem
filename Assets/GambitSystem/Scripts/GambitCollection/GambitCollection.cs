using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameSystems.SkillSystem;

/// <summary>
/// monobehaviour that contains the list of gambits
/// </summary>
public class GambitCollection : MonoBehaviour {
	/// <summary>
	/// the collection of gambits
	/// </summary>
	private List<Gambit> _gambits;

	/// <summary>
	/// Gets the gambits
	/// </summary>
	public List<Gambit> Gambits{
		get{
			if (_gambits == null) {
				_gambits = new List<Gambit> ();
			}
			return _gambits;
		}
	}

	/// <summary>
	/// The active gambit identifier.
	/// </summary>
	private int _activeGambitId = -1;

	/// <summary>
	/// Gets or sets the active gambit identifier.
	/// </summary>
	public int ActiveGambitId{
		get{ return _activeGambitId;}
		set{ _activeGambitId = value;}
	}

	/// <summary>
	/// perception component
	/// </summary>
	private Perception _perception;

	/// <summary>
	/// Gets or sets the perception component
	/// </summary>
	/// <value>The perception.</value>
	public Perception Perception{
		get{
			_perception = GetComponent<Perception> ();
			if(_perception == null)
				_perception = this.gameObject.AddComponent<Perception>();
			return _perception;
		}
		set{
			_perception = value;
		}
	}

	/// <summary>
	/// Configures the gambits
	/// </summary>
	public void ConfigureGambits(){
		Skill skill1 = new Skill(this.gameObject, "skill 1",10.0f,true,0.0f,2.0f,5.0f);
		Gambits.Add (new Gambit (this.gameObject, 0, skill1));
		Skill skill2 = new TargetableSkill (this.gameObject, "skill 2", 10.0f, true, 2.0f, 2.0f, 5.0f);
		skill2.Effects.Add (new SkillEffect(2.0f));
		skill2.Effects.Add (new SkillEffect(4.0f));
		Gambits.Add (new HighestEnmityGambit (this.gameObject, 0, skill2,(int)GambitTags.Enemy,false,Perception));
	}

	// Use this for initialization
	void Start () {
		ConfigureGambits ();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		int potentialGambit = FindPotentialGambit ();

		if (potentialGambit == -1) { // no runnable gambit
			StopAllCoroutines();
			ActiveGambitId = -1;
			return;
		}

		// if runnable gambit has higher priority than active gambit, and not locked, switch active gambit
		if (ActiveGambitId != potentialGambit) {
			StopAllCoroutines();
			ActiveGambitId = potentialGambit;
			StartCoroutine(RunGambit(ActiveGambitId));
		}
	}

	/// <summary>
	/// run the appropriate gambit and run the skill coroutine associated with the gambit
	/// </summary>
	public IEnumerator RunGambit(int gambitId){
		while (true) {
			yield return StartCoroutine (Gambits [ActiveGambitId].GambitCoroutine ());
			yield return StartCoroutine (Gambits [ActiveGambitId].Skill.SkillCoroutine ());
			yield return null;
		}
	}

	/// <summary>
	/// Finds the potential gambit.
	/// </summary>
	/// <returns>The potential gambit.</returns>
	public int FindPotentialGambit(){
		// find the highest priority runnable gambit
		int highestPriority = 0;
		int highestIndex = -1; // case where there's no runnable gambit
		for (int i = 0; i < Gambits.Count; i++) {
			if (Gambits [i].Priority >= highestPriority) {
				highestIndex = i;
				highestPriority = Gambits [i].Priority;
			}
		}

		return highestIndex;
	}

	void OnDisable(){
		StopAllCoroutines ();
	}

	void OnEnable(){
		ActiveGambitId = FindPotentialGambit ();
		if(ActiveGambitId != -1)
			StartCoroutine (RunGambit(ActiveGambitId));
	}
}
