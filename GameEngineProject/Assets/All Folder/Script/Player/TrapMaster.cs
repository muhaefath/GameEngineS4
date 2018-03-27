using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMaster : MonoBehaviour {

	Animator AnimatorTrap;

	void Start()
	{
		AnimatorTrap = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			AnimatorTrap.Play ("Attack");
			Destroy (col.gameObject);
		}
	}
}
