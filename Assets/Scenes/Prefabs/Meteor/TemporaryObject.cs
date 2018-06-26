using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObject : MonoBehaviour {
	public float lifetime = 1.0f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, lifetime);
	}
}
