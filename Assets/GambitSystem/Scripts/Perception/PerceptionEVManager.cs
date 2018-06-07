using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UtilitySystems;

public class HashtableEvent : UnityEvent <Hashtable> {}

public class PerceptionEVManager : Singleton<PerceptionEVManager> {
	/// <summary>
	/// The event dictionary.
	/// </summary>
	private Dictionary <string, HashtableEvent> _eventDictionary;

	/// <summary>
	/// Gets the event dictionary.
	/// </summary>
	/// <value>The event dictionary.</value>
	public Dictionary<string, HashtableEvent> EventDictionary{
		get{
			if (_eventDictionary == null)
				_eventDictionary = new Dictionary<string, HashtableEvent> ();
			return _eventDictionary;
		}
	}

	// register a listener to the event manager
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

	// unregister a listener from the event manager
	public static void StopListening (string eventName, UnityAction<Hashtable> listener){
		if (Instance == null)
			return;

		HashtableEvent thisEvent = null;
		if(Instance.EventDictionary.TryGetValue(eventName,out thisEvent))
			thisEvent.RemoveListener(listener);
	}

	// call to trigger the event
	public static void TriggerEvent(string eventName, Hashtable eventParams = default(Hashtable)){
		HashtableEvent thisEvent = null;
		if (Instance.EventDictionary.TryGetValue (eventName, out thisEvent))
			thisEvent.Invoke (eventParams);
	}
}