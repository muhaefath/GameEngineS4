using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMaster : MonoBehaviour {

	Animator AnimatorTrap;

	void Start()
	{
		//this.transform.position = new Vector3 (this.transform.position.x,0,this.transform.position.z);
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
