using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable {
	void MoveTo (Vector3 targetPos);
	void StopMove ();
	float RemainingDistance (Vector3 targetPos);
}
