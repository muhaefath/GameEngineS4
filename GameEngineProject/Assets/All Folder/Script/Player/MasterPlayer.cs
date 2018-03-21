using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterPlayer : MonoBehaviour {


	public static MasterPlayer instance;

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
	[Space]

	[Space]
	[Header ("Trap")]
	public float WaktuJedaTrap;
	public bool CekTombolJebakanDipencet;
	public Image BarTrap;
	public float IntBarTrap;

	public bool CekUdahDeketPohon = false; // bila sudah dekak pohon joystick nembak berubah jadi tebang pohon

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		AnimatorKarakrer = GetComponent<Animator> ();
		BarTrap.enabled = false;
	}

	void Update()
	{
		BarTrap.fillAmount = IntBarTrap;

		if (joystick.inputvector == Vector3.zero) {
			SpeedJalanPlayer = 0;
		} else {
			SpeedJalanPlayer =10;
		}

		if(TembakPeluruBool)
		{
			if(CekUdahDeketPohon == true)
			{
				StartCoroutine (JedaTebangPohon());
				//TembakPeluruBool = false;
				return;
			}
			TembakPeluru ();
		}

		if (CekTombolJebakanDipencet) {
			StartCoroutine (PasangJebakan());
		}

		MoveVector = PoolInput  ();

		Move ();
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
			WaktuJedaTebangPohon -= Time.deltaTime;
			ManagerGame.Instance.PohonSasaran.JumlahBarTebangPohon -= Time.deltaTime;
			ManagerGame.Instance.PohonSasaran.BarProgressTebangPohon.enabled = true;
			AnimatorKarakrer.SetBool ("Tebang",true);

		} else {
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

		AnimatorKarakrer.SetBool ("Tebang",false);
		TembakPeluruBool = false;

		WaktuJedaTebangPohon = 2f;
	}


	public IEnumerator PasangJebakan()
	{
		if (WaktuJedaTrap >= 0) {
			BarTrap.enabled = true;
			IntBarTrap -= Time.deltaTime;

			AnimatorKarakrer.SetBool ("Rakit",true);
			WaktuJedaTrap -= Time.deltaTime;
			TombakDipegang.SetActive (false);
			yield return 0;
		} else {
			WaktuJedaTrap = 1.5f;
			BarTrap.enabled = false;

			AnimatorKarakrer.SetBool ("Rakit",false);
			CekTombolJebakanDipencet = false;
			TombakDipegang.SetActive (true);
		}
	}

	public void LepasButtonRakitTrap()
	{
		
		AnimatorKarakrer.SetBool ("Rakit",false);

		WaktuJedaTrap = 1.5f;

		IntBarTrap = 1.5f;

		CekTombolJebakanDipencet = false;
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
			AnimatorKarakrer.SetBool("Jalan",true);
			//thisrigid.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

		} else {

			dir.y = CurrRotasi.y;
			AnimatorKarakrer.SetBool("Jalan",false);

		}
			
		return dir;
	}


}
