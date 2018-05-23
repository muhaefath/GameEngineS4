using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeluruMusuh : MonoBehaviour {


	public float SpeedPeluru;


	void Start(){
		transform.Rotate (0,180,0);
	}
	// Update is called once per frame
	void Update () {

		transform.Translate (0,0,-SpeedPeluru*Time.deltaTime);

		Destroy (gameObject,2);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			//Destroy (col.gameObject);
			MasterPlayer.instance.DarahBarInt -=5;
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Temple")
		{
			//Destroy (col.gameObject);
			ManagerGame.Instance.DarahBarInt -=5;
			Destroy (gameObject);
		}
	}
}
