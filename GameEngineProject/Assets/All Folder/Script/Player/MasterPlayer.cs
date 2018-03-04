using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPlayer : MonoBehaviour {

	public static MasterPlayer Instance;

	public float SpeedJalanPlayer;
	public float SpeedRotasiPlayer;
	[Space]
	public GameObject PeluruPrefab;
	public Transform PosisiPeluruKeluar;

	void Update()
	{
		NavigasiJalan ();
		TembakPeluru ();
	}

	void NavigasiJalan()
	{
		if(Input.GetKey(KeyCode.A))
		{
			transform.rotation = Quaternion.Euler (0,-90,0);
			transform.Translate (0,0,SpeedJalanPlayer*Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.W))
		{
			transform.rotation = Quaternion.Euler (0,0,0);
			transform.Translate (0,0,SpeedJalanPlayer*Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			transform.rotation = Quaternion.Euler (0,90,0);
			transform.Translate (0,0,SpeedJalanPlayer*Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			transform.rotation = Quaternion.Euler (0,-180,0);
			transform.Translate (0,0,SpeedJalanPlayer*Time.deltaTime);
		}
	}

	void TembakPeluru()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate (PeluruPrefab,PosisiPeluruKeluar.transform.position,PosisiPeluruKeluar.transform.rotation);
		}
	}
}
