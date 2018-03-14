using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPlayer : MonoBehaviour {




	[Space]

	[Header ("Navigasi")]
	public float SpeedJalanPlayer;
	public float SpeedRotasiPlayer;
	[SerializeField] Animator AnimatorKarakrer;
	private Rigidbody thisrigid;

	[Space]
	public VirtualJoystick joystick;
	public Vector3 MoveVector;
	public GameObject PrefabKarakterUtama;
	public Vector3 CurrRotasi;

	[Space]

	[Header ("Senjata")]
	public GameObject PeluruPrefab;
	public Transform PosisiPeluruKeluar;

	[Space]
	public bool CekUdahDeketPohon = false;


	void Start()
	{
		AnimatorKarakrer = GetComponent<Animator> ();
	}

	void Update()
	{
		//NavigasiJalan ();
		TembakPeluru ();
		TebangPohon ();

		if (joystick.inputvector == Vector3.zero) {
			SpeedJalanPlayer = 0;
		} else {
			SpeedJalanPlayer =10;
		}

		MoveVector = PoolInput  ();

		Move ();
	}

	/*
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
	*/

	void TembakPeluru()
	{
		if(CekUdahDeketPohon == true)
		{
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate (PeluruPrefab,PosisiPeluruKeluar.transform.position,PosisiPeluruKeluar.transform.rotation);
		}
	}

	void TebangPohon()
	{
		if(ManagerGame.Instance.PohonSasaran != null )
		{
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				ManagerGame.Instance.PohonSasaran.CekUdahDitebang = true;
			}
		}
	}


	private void Move()
	{
		transform.rotation = Quaternion.Euler(MoveVector);
		transform.Translate(0,0, SpeedJalanPlayer*Time.deltaTime);

		PrefabKarakterUtama.transform.rotation = this.transform.rotation;
		PrefabKarakterUtama.transform.position = this.transform.position;

	}



	private Vector3 PoolInput()
	{
		Vector3 dir = Vector3.zero;


		if(dir.magnitude > 1)
		{
			dir.Normalize ();

		}
		if (joystick.inputvector != Vector3.zero) {

			if (joystick.inputvector.x >= 0 && joystick.inputvector.z > 0) {
				dir.y = (joystick.inputvector.x - joystick.inputvector.z) * 45 + 45;
				CurrRotasi.y = dir.y;
			} else if (joystick.inputvector.x > 0 && joystick.inputvector.z <= 0) {
				dir.y = 135 - (joystick.inputvector.x + joystick.inputvector.z) * 45;
				CurrRotasi.y = dir.y;
			} else if (joystick.inputvector.x <= 0 && joystick.inputvector.z < 0) {
				dir.y = 225 + (joystick.inputvector.z - joystick.inputvector.x) * 45;
				CurrRotasi.y = dir.y;
			} else if (joystick.inputvector.x < 0 && joystick.inputvector.z >= 0) {
				dir.y = 315 + (joystick.inputvector.x + joystick.inputvector.z) * 45;
				CurrRotasi.y = dir.y;
			}
			AnimatorKarakrer.SetBool ("Jalan", true);
			//thisrigid.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

		} else {

			dir.y = CurrRotasi.y;
			AnimatorKarakrer.SetBool("Jalan",false);

		
		}


		return dir;
	}
}
