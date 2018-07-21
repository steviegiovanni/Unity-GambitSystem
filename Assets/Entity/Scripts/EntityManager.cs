using System.Collections.Generic;
using UnityEngine;
using UtilitySystems;
using GameSystems.Entities.Database;

namespace GameSystems.Entities{
	/// <summary>
	/// singleton manager to process requiest to instantiate / destroy entities
	/// </summary>
	public class EntityManager : Singleton<EntityManager> {
		/// <summary>
		/// the list of entity
		/// </summary>
		private List<Entity> _entities;
		public List<Entity> Entities{
			get{
				if (_entities == null)
					_entities = new List<Entity> ();
				return _entities;
			}
		}

		public static GameObject Instantiate(int id){
			EntityAsset entityAsset = EntityDatabase.Instance.Get (id);
			GameObject entityPrefab = Resources.Load <GameObject>("Entity");
			GameObject entity = Instantiate (entityPrefab);
			entity.GetComponent<Entity> ().Setup (entityAsset);
			return entity;
		}

		public static void Destroy(Entity entity){
			Instance.Entities.Remove (entity);
			Destroy (entity.gameObject);
		}
	}
}