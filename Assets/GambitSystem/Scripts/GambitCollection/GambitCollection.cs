using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// monobehaviour that contains the list of gambits
/// </summary>
public class GambitCollection : MonoBehaviour {
	/// <summary>
	/// the collection of gambits
	/// </summary>
	private List<Gambit> _gambits;

	/// <summary>
	/// The active gambit identifier.
	/// </summary>
	private int _activeGambitId = -1;

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
	/// Gets or sets the active gambit identifier.
	/// </summary>
	public int ActiveGambitId{
		get{ return _activeGambitId;}
		set{ _activeGambitId = value;}
	}

	/// <summary>
	/// Configures the gambits
	/// </summary>
	public void ConfigureGambits(){
		Gambits.Add (new Gambit (0, new Skill("skill 1",0)));
		//Gambits.Add (new Gambit (0, new Skill("skill 2")));
		Perception perception = this.GetComponent<Perception>();
		if (perception == null)
			perception = this.gameObject.AddComponent<Perception> ();
		Gambits.Add (new TargetFirstGambit (0, new Skill("skill 2",0),null,perception));
	}

	// Use this for initialization
	void Start () {
		ConfigureGambits ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Gambits.Count <= 0) // if there's nothing in the list of gambits, return
			return;

		// find the highest priority runnable gambit
		int highestPriority = 0;
		int highestIndex = -1; // case where there's no runnable gambit
		for (int i = 0; i < Gambits.Count; i++) {
			if (Gambits [i].Priority >= highestPriority) {
				highestIndex = i;
				highestPriority = Gambits [i].Priority;
			}
		}

		if (highestIndex == -1) { // no runnable gambit
			if (ActiveGambitId != -1) // stop currently running gambit if there's any
				StopCoroutine (Gambits [ActiveGambitId].Coroutine());
			ActiveGambitId = -1;
			return;
		}

		// if runnable gambit has higher priority than active gambit, and not locked, switch active gambit
		if (ActiveGambitId != highestIndex) {
			if (ActiveGambitId != -1) // stop currently running gambit if there's any
				StopCoroutine (Gambits [ActiveGambitId].Coroutine());
			ActiveGambitId = highestIndex;
			StartCoroutine (Gambits [ActiveGambitId].Coroutine());
		}
	}
}
