﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGambit : Gambit {
	/// <summary>
	/// Initializes a new instance of the <see cref="PositionGambit"/> class.
	/// </summary>
	public PositionGambit():base(){
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="PositionGambit"/> class.
	/// </summary>
	/// <param name="priority">Priority.</param>
	/// <param name="skill">Skill.</param>
	public PositionGambit(int priority, Skill skill):base(priority,skill){
	}

	/// <summary>
	/// Coroutine this instance.
	/// </summary>
	public override IEnumerator Coroutine(){
		while (true) {
			Debug.Log (string.Format("{0} coroutine",Skill.Name));
			yield return null;
		}
	}
}
