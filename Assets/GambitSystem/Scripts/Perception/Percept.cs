using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Percept : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		EventManager.TriggerEvent ("PERCEPTION", new Hashtable (){ { "OBJECT",this.gameObject } });
	}
}
