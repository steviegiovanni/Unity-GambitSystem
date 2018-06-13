﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UtilitySystems;

/// <summary>
/// event with hashtable parameter
/// </summary>
public class HashtableEvent : UnityEvent <Hashtable> {}

namespace GameSystems.Perception{
	/// <summary>
	/// singleton event manager to process all perception event
	/// </summary>
	public class PerceptionEVManager : Singleton<PerceptionEVManager> {
		/// <summary>
		/// the event dictionary
		/// </summary>
		private Dictionary <string, HashtableEvent> _eventDictionary;
		public Dictionary<string, HashtableEvent> EventDictionary{
			get{
				if (_eventDictionary == null)
					_eventDictionary = new Dictionary<string, HashtableEvent> ();
				return _eventDictionary;
			}
		}

		/// <summary>
		/// register a listener to the event manager
		/// </summary>
		public static void StartListening(string eventName, UnityAction<Hashtable> listener){
			HashtableEvent thisEvent = null;
			if (Instance.EventDictionary.TryGetValue (eventName, out thisEvent))
				thisEvent.AddListener (listener);
			else {
				thisEvent = new HashtableEvent ();
				thisEvent.AddListener (listener);
				Instance.EventDictionary.Add (eventName, thisEvent);
			}
		}

		/// <summary>
		/// unregister a listener from the event manager
		/// </summary>
		public static void StopListening (string eventName, UnityAction<Hashtable> listener){
			if (Instance == null)
				return;

			HashtableEvent thisEvent = null;
			if(Instance.EventDictionary.TryGetValue(eventName,out thisEvent))
				thisEvent.RemoveListener(listener);
		}

		/// <summary>
		/// call to trigger the event
		/// </summary>
		public static void TriggerEvent(string eventName, Hashtable eventParams = default(Hashtable)){
			HashtableEvent thisEvent = null;
			if (Instance.EventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.Invoke (eventParams);
			}
		}
	}
}