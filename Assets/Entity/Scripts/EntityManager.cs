using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UtilitySystems;
using GameSystems.EntitySystem.Database;
using GameSystems.SkillSystem;
using GameSystems.GambitSystem;

namespace GameSystems.EntitySystem{
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
			entity.GetComponent<Entity> ().SkillCollectionId = entityAsset.SkillCollectionId;
			entity.GetComponent<Entity> ().GambitCollectionId = entityAsset.GambitCollectionId;
			entity.GetComponent<SkillCollection> ().SkillCollectionId = entityAsset.SkillCollectionId;
			entity.GetComponent<SkillCollection> ().SetupCollection ();
			entity.GetComponent<GambitCollection> ().GambitCollectionId = entityAsset.GambitCollectionId;
			entity.GetComponent<GambitCollection> ().SetupCollection ();
			return entity;
		}

		public static void Destroy(Entity entity){
			Instance.Entities.Remove (entity);
			Destroy (entity.gameObject);
		}
	}
}