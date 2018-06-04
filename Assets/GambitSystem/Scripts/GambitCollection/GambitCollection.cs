using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// monobehaviour that contains the list of gambits
/// </summary>
public class GambitCollection : MonoBehaviour {
	/// <summary>
	/// the collection of gambits
	/// </summary>
	private List<Gambit> _gambits;

	/// <summary>
	/// Gets the gambits.
	/// </summary>
	/// <value>The gambits.</value>
	public List<Gambit> Gambits{
		get{
			if (_gambits == null) {
				_gambits = new List<Gambit> ();
			}
			return _gambits;
		}
	}

	/// <summary>
	/// Configures the gambits.
	/// </summary>
	public void ConfigureGambits(){
		Gambits.Add (new Gambit (0, new Skill("skill 1")));
		Gambits.Add (new Gambit (0, new Skill("skill 2")));
	}

	// Use this for initialization
	void Start () {
		ConfigureGambits ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Gambits.Count; i++) {
			if (Gambits [i].Skill.CanUse ())
				Gambits [i].Skill.UseSkill ();
		}
	}
}
