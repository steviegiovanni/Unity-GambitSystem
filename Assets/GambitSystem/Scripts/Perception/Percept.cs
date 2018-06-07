using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percept{
	/// <summary>
	/// The entity.
	/// </summary>
	private GameObject _entity;

	/// <summary>
	/// Gets or sets the entity.
	/// </summary>
	/// <value>The entity.</value>
	public GameObject Entity{
		get{ return _entity;}
		set{ _entity = value;}
	}

	/// <summary>
	/// The enmity.
	/// </summary>
	private int _enmity;

	/// <summary>
	/// Gets or sets the enmity.
	/// </summary>
	/// <value>The enmity.</value>
	public int Enmity{
		get{ return _enmity;}
		set{ _enmity = value;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Percept"/> class.
	/// </summary>
	public Percept(){
		Entity = null;
		Enmity = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Percept"/> class.
	/// </summary>
	/// <param name="entity">Entity.</param>
	/// <param name="enmity">Enmity.</param>
	public Percept(GameObject entity, int enmity){
		Entity = entity;
		Enmity = enmity;
	}
}