using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.SkillSystem;

/// <summary>
/// Base class for a character gambit
/// </summary>
public class Gambit{
	/// <summary>
	/// The owner.
	/// </summary>
	private GameObject _owner;

	/// <summary>
	/// Gets or sets the owner.
	/// </summary>
	/// <value>The owner.</value>
	public GameObject Owner{
		get{ return _owner;}
		set{ _owner = value;}
	}

	/// <summary>
	/// Execution priority of gambit
	/// </summary>
	private int _priority;

	/// <summary>
	/// Gets or sets the priority
	/// </summary>
	public int Priority{
		get{ return _priority;}
		set{ _priority = value;}
	}

	private string _skillId;

	public string SkillId{
		get{ return _skillId;}
		set{ _skillId = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	public Gambit(){
		Priority = 0;
		SkillId = "";
		Owner = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	public Gambit(GameObject owner, int priority){
		Owner = owner;
		Priority = priority;
		SkillId = "";
	}

	public Gambit(GambitAsset asset){
		Owner = null;
		Priority = asset.Priority;
		//Skill = asset.Skill.CreateInstance ();
		SkillId = asset.SkillId;
	}

	/// <summary>
	/// Coroutine associated to this gambit when it's active
	/// </summary>
	public virtual IEnumerator GambitCoroutine(){
		Skill skill = Owner.GetComponent<SkillCollection> ().GetSkill<Skill> (SkillId);
		while (true) {
			break;
				
			yield return null;
		}

		yield return new WaitForSeconds(skill.CastTime);
	}
}
