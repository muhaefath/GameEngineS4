using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TebangPohon : MonoBehaviour {
	public bool CekUdahDitebang = false;
	public bool PlayerUdahDeket = false;

	MasterPlayer Manager;

	void Start()
	{
		Manager = FindObjectOfType<MasterPlayer> ();
		ManagerGame.Instance.DaftarPohonDidalamScene.Add (this);	
	}

	void Update()
	{
		if (Vector3.Distance (this.transform.position, Manager.transform.position) < 1) {
			
			PlayerUdahDeket = true;
			ManagerGame.Instance.PohonSasaran = this;
		} else {

			PlayerUdahDeket = false;
		}


		if(CekUdahDitebang == true)
		{
			Destroy (gameObject);
		}
	}
}
