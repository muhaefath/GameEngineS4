﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterPlayer : MonoBehaviour {


	public static MasterPlayer instance;

	[Space]

	[Header ("Status")]
	public float DarahBarInt;
	public Image DarahaBar;


	[Space]

	[Header ("Navigasi")]
	public float SpeedJalanPlayer;
	public float SpeedRotasiPlayer;
	public Animator AnimatorKarakrer;

	public float WaktuJedaTembakPeluru;
	public float WaktuJedaTebangPohon;

	public bool TembakPeluruBool;
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
	public GameObject TombakDipegang;
	public GameObject KapakDipegang;
	public Image[] JoystickKananLogo;

	[Header ("Partner")]
	public bool CekTombolBuatPartner;
	public GameObject PartnerPrefab;
	public Transform PosisiPartner;

	[Space]

	[Space]
	[Header ("Trap")]
	public float WaktuJedaTrap;
	public bool CekTombolJebakanDipencetTrap1;
	public bool CekTombolJebakanDipencetTrap2;
	public Image BarTrap;
	public float IntBarTrap;

	public GameObject Trap1;
	public Transform PosisiTrap1;
	public GameObject Trap2;
	public Transform PosisiTrap2;
	public Transform ParentTrap2;

	public bool CekUdahDeketPohon = false; // bila sudah dekak pohon joystick nembak berubah jadi tebang pohon

	public AudioSource[] AudioPlayer;



	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		//this.transform.localPosition = new Vector3 (0,0.246f,-0.142f);

		AnimatorKarakrer = GetComponent<Animator> ();


		BarTrap.enabled = false;
		for (int i = 0; i < JoystickKananLogo.Length; i++) {
			JoystickKananLogo [i].enabled = false;
		}
		JoystickKananLogo [0].enabled = true;

	}

	void Update()
	{
		BarTrap.fillAmount = IntBarTrap / 3;
		DarahaBar.fillAmount  =  DarahBarInt / 100;


		if (joystick.inputvector == Vector3.zero) {
			SpeedJalanPlayer = 0;
		} else {
			SpeedJalanPlayer =5;
		}

		if(TembakPeluruBool)
		{
			
			if(CekUdahDeketPohon == true)
			{
				
				StartCoroutine (JedaTebangPohon());
				//TembakPeluruBool = false;

				return;
			}
			if (ManagerGame.Instance.JumlahAmunisi > 0) {
				TembakPeluru ();
			} else {
				TembakPeluruBool = false;
				return;
			}

		}

		if (CekTombolJebakanDipencetTrap1) {
			StartCoroutine (PasangJebakan(0));
		}
		else if(CekTombolJebakanDipencetTrap2)
		{
			StartCoroutine (PasangJebakan(1));
		}else if(CekTombolBuatPartner)
		{
			StartCoroutine (PasangJebakan(2));
		}


		MoveVector = PoolInput  ();


		Move ();

		if (CekUdahDeketPohon == true) {
			for (int i = 0; i < JoystickKananLogo.Length; i++) {
				JoystickKananLogo [i].enabled = false;
			}
			JoystickKananLogo [1].enabled = true;
		} else {
			for (int i = 0; i < JoystickKananLogo.Length; i++) {
				JoystickKananLogo [i].enabled = false;
			}
			JoystickKananLogo [0].enabled = true;
		}
	}



	public void TembakPeluru()
	{
		
		AnimatorKarakrer.Play ("Tembak");

		StartCoroutine (JedaTembakPeluru());

		//TombakDipegang.SetActive (true);
	}


	public IEnumerator JedaTebangPohon()
	{
		
		if (WaktuJedaTebangPohon > 0) {
			
			KapakDipegang.SetActive (true);
			TombakDipegang.SetActive (false);

			WaktuJedaTebangPohon -= Time.deltaTime;
			ManagerGame.Instance.PohonSasaran.JumlahBarTebangPohon -= Time.deltaTime;
			ManagerGame.Instance.PohonSasaran.BarProgressTebangPohon.enabled = true;
			AnimatorKarakrer.SetBool ("Tebang",true);

			yield return 0;
		} else {
			
			ManagerGame.Instance.JumlahKayu += 3;
			KapakDipegang.SetActive (false);
			TombakDipegang.SetActive (true);
			//ManagerGame.Instance.DaftarPohonDidalamScene.Remove (ManagerGame.Instance.PohonSasaran);
			ManagerGame.Instance.PohonSasaran.Anim.Play ("tumbang");

			Invoke("PohonIlang",1.5f);

			//Destroy (ManagerGame.Instance.PohonSasaran.gameObject,1.5f);


			AnimatorKarakrer.SetBool ("Tebang",false);

			TembakPeluruBool = false;

			WaktuJedaTebangPohon = 2f;
			AudioPlayer [2].Stop ();


		}
	}

	 void PohonIlang()
	{
		ManagerGame.Instance.PohonSasaran.DaunPohon.SetActive (false);
		ManagerGame.Instance.PohonSasaran.BatangPohon.SetActive (false);
		ManagerGame.Instance.PohonSasaran.CekUdahDitebang = true;
		ManagerGame.Instance.PohonSasaranCurr = ManagerGame.Instance.PohonSasaran;

		ManagerGame.Instance.PohonSasaran = null;
		//StartCoroutine( HilangDulu ());

	}



	public IEnumerator JedaTebangPohon2()
	{	
		
		yield return new WaitForSeconds(2);

		ManagerGame.Instance.JumlahKayu += 10;
		KapakDipegang.SetActive (false);
		TombakDipegang.SetActive (true);
		ManagerGame.Instance.DaftarPohonDidalamScene.Remove (ManagerGame.Instance.PohonSasaran);
		ManagerGame.Instance.PohonSasaran.Anim.Play ("tumbang");
		Invoke("PohonIlang",1.5f);

		//Destroy (ManagerGame.Instance.PohonSasaran.gameObject,1.5f);
		ManagerGame.Instance.PohonSasaran = null;


		AnimatorKarakrer.SetBool ("Tebang",false);
		AudioPlayer [2].Stop ();

		TembakPeluruBool = false;

		WaktuJedaTebangPohon = 2f;
		LepasButtonTebangPohon ();
	}

	public void LepasButtonTebangPohon()
	{
		
		if(ManagerGame.Instance.PohonSasaran != null)
		{
			ManagerGame.Instance.PohonSasaran.JumlahBarTebangPohon = 1;
			ManagerGame.Instance.PohonSasaran.BarProgressTebangPohon.enabled = false;
		}
		KapakDipegang.SetActive (false);
		TombakDipegang.SetActive (true);

		AnimatorKarakrer.SetBool ("Tebang",false);

		TembakPeluruBool = false;

		WaktuJedaTebangPohon = 2f;
	}


	public IEnumerator PasangJebakan(int IndexJebakan)
	{
		if (WaktuJedaTrap >= 0) {
			if(IndexJebakan == 1 && ManagerGame.Instance.JumlahKayu >=10 || IndexJebakan == 0 && ManagerGame.Instance.JumlahKayu >=15 || IndexJebakan == 2 && ManagerGame.Instance.JumlahKayu >=30)
			{
				BarTrap.enabled = true;
				IntBarTrap -= Time.deltaTime;

				AnimatorKarakrer.SetBool ("Rakit",true);
				WaktuJedaTrap -= Time.deltaTime;
				TombakDipegang.SetActive (false);
				yield return 0;
			}
			yield return 0;
		} else {
			
			if (IndexJebakan == 0) {
				ManagerGame.Instance.JumlahKayu -= 15;
				GameObject build = Instantiate (Trap1, PosisiTrap1.position, PosisiTrap1.rotation) as GameObject; 
				build.transform.parent = ParentTrap2.transform;
				build.transform.rotation = Quaternion.Euler (build.transform.rotation.x,0,build.transform.rotation.z);

				CekTombolJebakanDipencetTrap1 = false;
			} else if(IndexJebakan == 1) {
				//Instantiate (Trap2, PosisiTrap2.position, PosisiTrap2.transform.rotation);
				ManagerGame.Instance.JumlahKayu -= 10;
				GameObject build = Instantiate (Trap2,PosisiTrap2.position,PosisiTrap2.rotation) as GameObject; 
				build.transform.parent = ParentTrap2.transform;


				CekTombolJebakanDipencetTrap2 = false;
			} else if(IndexJebakan == 2)
			{
				ManagerGame.Instance.JumlahKayu -= 30;
				GameObject build =  Instantiate (PartnerPrefab, PosisiPartner.position, PosisiPartner.rotation) as GameObject;
				//build.GetComponent<UnityEngine.AI.NavMeshAgent> ().Warp(PosisiPartner.position);

				CekTombolBuatPartner = false;
			}

			WaktuJedaTrap = 1.5f;
			BarTrap.enabled = false;

			AnimatorKarakrer.SetBool ("Rakit",false);


			TombakDipegang.SetActive (true);
			MasterPlayer.instance.AudioPlayer [3].Stop ();
		}
	}

	public void LepasButtonRakitTrap()
	{
		
		AnimatorKarakrer.SetBool ("Rakit",false);

		WaktuJedaTrap = 3f;

		IntBarTrap = 3f;

		CekTombolJebakanDipencetTrap1 = false;
		CekTombolJebakanDipencetTrap2 = false;
		CekTombolBuatPartner = false;
		BarTrap.enabled = false;
		TombakDipegang.SetActive (true);
	}


	public IEnumerator JedaTembakPeluru()
	{
		

		if (WaktuJedaTembakPeluru >= 0) {
			
			WaktuJedaTembakPeluru -= Time.deltaTime;
			yield return 0;
		} else {
			WaktuJedaTembakPeluru = 0.5f;
			TombakDipegang.SetActive (false);
			Instantiate (PeluruPrefab,PosisiPeluruKeluar.transform.position,PosisiPeluruKeluar.transform.rotation);
			AudioPlayer [1].Play ();
			ManagerGame.Instance.JumlahAmunisi -= 1;
			TembakPeluruBool = false;
			StartCoroutine (JedaTombakMuncul());
		}
	}

	public IEnumerator JedaTombakMuncul()
	{
		yield return new WaitForSeconds (0.5f);
		TombakDipegang.SetActive (true);
	}



	private void Move()
	{
		transform.rotation = Quaternion.Euler(MoveVector);
		transform.Translate(0,0, SpeedJalanPlayer*Time.deltaTime);

		PrefabKarakterUtama.transform.localRotation = this.transform.localRotation;
		PrefabKarakterUtama.transform.localPosition = this.transform.localPosition;

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
			PosisiTrap2.rotation = Quaternion.Euler (-90,CurrRotasi.y,0);
			AnimatorKarakrer.SetBool("Jalan",true);
			//thisrigid.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

		} else {

			dir.y = CurrRotasi.y;
			AnimatorKarakrer.SetBool("Jalan",false);

		}
			
		return dir;
	}



}
