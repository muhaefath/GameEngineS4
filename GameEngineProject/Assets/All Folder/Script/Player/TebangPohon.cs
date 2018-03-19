using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TebangPohon : MonoBehaviour {
	public bool CekUdahDitebang = false;
	public bool PlayerUdahDeket = false;



	void Start()
	{
		
		ManagerGame.Instance.DaftarPohonDidalamScene.Add (this);	
	}

	void Update()
	{
		if (Vector3.Distance (this.transform.position, MasterPlayer.instance.transform.position) < 2) {
			
			PlayerUdahDeket = true;
			ManagerGame.Instance.PohonSasaran = this;
			MasterPlayer.instance.CekUdahDeketPohon = true;
		} else {

			PlayerUdahDeket = false;

		}


		if(CekUdahDitebang == true)
		{
			Destroy (gameObject);
		}
	}
}
