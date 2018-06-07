using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Perception : MonoBehaviour{
	/// <summary>
	/// The percepts.
	/// </summary>
	private Dictionary<int,Percept> _percepts;

	/// <summary>
	/// Gets the percepts.
	/// </summary>
	/// <value>The percepts.</value>
	public Dictionary<int, Percept> Percepts{
		get{
			if (_percepts == null)
				_percepts = new Dictionary<int,Percept> ();
			return _percepts;
		}
	}

	/// <summary>
	/// The range
	/// </summary>
	[SerializeField]
	private float _range;

	/// <summary>
	/// Gets or sets the range.
	/// </summary>
	/// <value>The range.</value>
	public float Range{
		get{ return _range;}
		set{ _range = value;}
	}
		
	/// <summary>
	/// The alert mask.
	/// </summary>
	[SerializeField]
	private int _alertMask = 0xFFFF;

	/// <summary>
	/// Gets or sets the alert mask.
	/// </summary>
	/// <value>The alert mask.</value>
	public int AlertMask{
		get{ return _alertMask;}
		set{ _alertMask = value;}
	}

	/// <summary>
	/// trigerred when alerted
	/// </summary>
	private UnityEvent _onAlerted;

	/// <summary>
	/// Gets the on alerted.
	/// </summary>
	/// <value>The on alerted.</value>
	public UnityEvent OnAlerted{
		get{ 
			if (_onAlerted == null)
				_onAlerted = new UnityEvent ();
			return _onAlerted;
			}
	}

	/// <summary>
	/// trigerred when alerted
	/// </summary>
	private UnityEvent _onUnalerted;

	/// <summary>
	/// Gets the on alerted.
	/// </summary>
	/// <value>The on alerted.</value>
	public UnityEvent OnUnalerted{
		get{ 
			if (_onUnalerted == null)
				_onUnalerted = new UnityEvent ();
			return _onUnalerted;
		}
	}

	void Update(){
		// cleanup missing entity
		var perceptKeys = Percepts.Keys.ToArray();
		bool alerted = false;
		for (int i = 0; i < perceptKeys.Length; i++) {
			GameObject entity = Percepts [perceptKeys [i]].Entity;
			if (entity == null)
				Percepts.Remove (perceptKeys [i]);
			else {
				if ((Vector3.SqrMagnitude (entity.transform.position - this.transform.position) <= Range * Range)
				    && ((entity.GetComponent<IPerceivable> ().Tag & AlertMask) != 0))
					alerted = true;
			}
		}
		//invoke alert and unalerted accordingly
		if (alerted && (OnAlerted != null))
			OnAlerted.Invoke ();
		else if (!alerted && (OnUnalerted != null)) {
			OnUnalerted.Invoke ();
		}
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable(){
		PerceptionEVManager.StartListening ("PERCEPTION", Perceived);
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable(){
		PerceptionEVManager.StopListening ("PERCEPTION", Perceived);
	}

	// percept is detected
	void Perceived(Hashtable param){
		if (!param.ContainsKey ("OBJECT"))
			return;

		GameObject perceivedObject = (GameObject)(param ["OBJECT"]);
		Percept percept;
		if (Percepts.TryGetValue (perceivedObject.GetInstanceID(), out percept)) {
			Percepts [perceivedObject.GetInstanceID()].Entity = perceivedObject;
		}else {
			IPerceivable perceivable = perceivedObject.GetComponent<IPerceivable> ();
			if(perceivable != null)
				Percepts.Add (perceivedObject.GetInstanceID(), new Percept(perceivedObject,0));
		}
	}
}
