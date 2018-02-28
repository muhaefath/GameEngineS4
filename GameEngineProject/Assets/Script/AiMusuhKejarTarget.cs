using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMusuhKejarTarget : MonoBehaviour {

	public float Speed;
	public GameObject TemplePusat;
	
	// Update is called once per frame
	void Update () {
		AIJalan ();
	}

	void AIJalan()
	{
		//Debug.Log ((Vector3.Distance(TemplePusat.transform.position , this.transform.position)));
		//return;
		Vector3 SelisihPosisi =  TemplePusat.transform.position - this.transform.position ;
		if(Vector3.Distance(TemplePusat.transform.position , this.transform.position) < 5)
		{
			return;
		}

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation,Quaternion.LookRotation(SelisihPosisi),0.1f);
		this.transform.Translate (0,0,Speed * Time.deltaTime);
	}
}
