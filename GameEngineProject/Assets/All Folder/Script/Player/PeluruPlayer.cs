using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeluruPlayer : MonoBehaviour {

	public float SpeedPeluru;


	void Start(){
		transform.Rotate (0,180,0);
	}
	// Update is called once per frame
	void Update () {
		
		transform.Translate (0,0,-SpeedPeluru*Time.deltaTime);

		Destroy (gameObject,1);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			ManagerGame.Instance.DaftarMusuhDidalamScene.Remove (col.gameObject.GetComponent<AiMusuhKejarTarget>());
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
	}
}
