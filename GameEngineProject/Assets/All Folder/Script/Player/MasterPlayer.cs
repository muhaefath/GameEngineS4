using System.Collections;
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

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		

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
		DarahaBar.fillAmount  =  DarahBarInt / 10;


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

	public void TebangPohon()
	{
		
		//StartCoroutine ();
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

		} else {
			ManagerGame.Instance.JumlahKayu += 10;
			KapakDipegang.SetActive (false);
			TombakDipegang.SetActive (true);
			ManagerGame.Instance.DaftarPohonDidalamScene.Remove (ManagerGame.Instance.PohonSasaran);
			Destroy (ManagerGame.Instance.PohonSasaran.gameObject);
			ManagerGame.Instance.PohonSasaran = null;

			CekUdahDeketPohon = false;
			AnimatorKarakrer.SetBool ("Tebang",false);
			TembakPeluruBool = false;

			WaktuJedaTebangPohon = 2f;
			yield return 0;
		}
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
			BarTrap.enabled = true;
			IntBarTrap -= Time.deltaTime;

			AnimatorKarakrer.SetBool ("Rakit",true);
			WaktuJedaTrap -= Time.deltaTime;
			TombakDipegang.SetActive (false);
			yield return 0;
		} else {
			if (IndexJebakan == 0) {
				Instantiate (Trap1, PosisiTrap1.position, PosisiTrap1.rotation);
				CekTombolJebakanDipencetTrap1 = false;
			} else if(IndexJebakan == 1) {
				//Instantiate (Trap2, PosisiTrap2.position, PosisiTrap2.transform.rotation);
				GameObject build = Instantiate (Trap2,PosisiTrap2.position,PosisiTrap2.rotation) as GameObject; 
				build.transform.parent = ParentTrap2.transform;


				CekTombolJebakanDipencetTrap2 = false;
			} else if(IndexJebakan == 2)
			{
				Instantiate (PartnerPrefab, PosisiPartner.position, PosisiPartner.rotation);
				CekTombolBuatPartner = false;
			}

			WaktuJedaTrap = 1.5f;
			BarTrap.enabled = false;

			AnimatorKarakrer.SetBool ("Rakit",false);


			TombakDipegang.SetActive (true);
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
