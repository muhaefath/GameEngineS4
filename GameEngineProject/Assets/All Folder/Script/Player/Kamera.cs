using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour {

	public Transform Target;

	public float Smoothmove ;
	public Vector3 offset;


	

	void LateUpdate () {
		Vector3 desiredPosition = Target.position + offset;
		Vector3 smoothPosition = Vector3.Lerp (transform.position,desiredPosition,Smoothmove);
		transform.position = smoothPosition;


	}
}
