﻿using UnityEngine;

namespace GameSystems.PerceptionSystem{
	/// <summary>
	/// Percept class. Holds the entity game object that triggers 
	/// the percept, and the enmity generated by that entity
	/// </summary>
	public class Percept{
		/// <summary>
		/// The entity game object
		/// </summary>
		private GameObject _entity;
		public GameObject Entity{
			get{ return _entity;}
			set{ _entity = value;}
		}

		/// <summary>
		/// The enmity of the entity
		/// </summary>
		private int _enmity;
		public int Enmity{
			get{ return _enmity;}
			set{ _enmity = value;}
		}

		/// <summary>
		/// constructor 
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="enmity">Enmity.</param>
		public Percept(GameObject entity, int enmity){
			Entity = entity;
			Enmity = enmity;
		}
	}
}