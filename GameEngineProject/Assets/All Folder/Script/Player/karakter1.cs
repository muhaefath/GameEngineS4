using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class karakter1 : MonoBehaviour {
	public int amuni;

	public int kayu ;
	public float darah;
	public float darahplayernya;
	public Image darahgambar;
	public Image darahplayernyagambar;

	public GameObject pelurunya;
	public Transform posisipelurunya;

	public GameObject PrefabPemain; // prefab pemain supaya posisinya sama kayak yang ada di scene

	public Text JumlahUang ; 



	public VirtualJoystick joystick;


	public Animator Anim;
	//
	private Rigidbody thisrigid;
	public Vector3 MoveVector;public Vector3 MoveVector2;
	public float terminalspeed = 100.0f;
	public float drag = 0.5f;
	public float movespeed;

	public float a = 0.5f;
	public float b = 3;
	public bool NyerangUdah;

	public Vector3 CurrRotasi;

	public GameObject pohon;


	public GameObject[] senjata;
	public GameObject[] Kumpulansenjata;
	public GameObject ParentAR;
	public bool ceknyerang;
	public bool repairnya;
	public int ceka1;
	public int ceka2;
	public int cek3;
	public bool daerahbangun;

	public AudioSource[] efek;
	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {

		darah = 20;
		darahplayernya = 10;
		amuni = 10;
		PrefabPemain.transform.rotation = this.transform.rotation;
		PrefabPemain.transform.position = this.transform.position;
		kayu = 0;
		thisrigid = GetComponent<Rigidbody> ();
		thisrigid.maxAngularVelocity = terminalspeed;
		thisrigid.drag = drag;
		joystick = FindObjectOfType<VirtualJoystick> ();

		Anim = GetComponent<Animator> ();
		for (int i = 0; i <  senjata.Length; i++) {
			senjata [i].SetActive (false);
		}
		for (int i = 0; i <Kumpulansenjata.Length; i++) {
			Kumpulansenjata [i].SetActive (false);
		}
		Kumpulansenjata [0].SetActive (true);
		ceka1 = 0;
		for (int i = 0; i < efek.Length; i++) {
			efek [i].enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//JumlahUang.text = "uang: " + kayu;


		if(darah <=0 || darahplayernya <= 0)
		{
			SceneManager.LoadScene ("Gameover");
		}

		if (joystick.inputvector == Vector3.zero) {
			movespeed = 0;
		} else {
			movespeed =1200;
		}
		MoveVector =
		PoolInput ();
		
		MoveVector2 = PoolInput2  ();

		Move ();

		if (ceka2 == 2 && ceknyerang == false && amuni >0) {
			NyerangUdah = true;
			Anim.SetBool ("nembak", true);
			Anim.SetBool ("nyerang",false);
			senjata [0].SetActive (true);
			senjata [1].SetActive (false);

		}
		else  {
			//ceka2 = 1;
			Anim.SetBool ("nembak", false);
			//NyerangUdah = false;

			//senjata [0].SetActive (false);
		}

		if(cek3 == 1 && repairnya == true)
		{
			Anim.SetBool ("buat",true);
			repairnya = false;
			StartCoroutine(Repairpesawat ());

		}
		else if(cek3 == 2 && repairnya == true)
		{
			Anim.SetBool ("buat",false);
		}
		if(NyerangUdah == true)
		{
			
			StartCoroutine (Keluarpeluru ());

		}
		//senjata [0].SetActive (true);
		if(darah > 20)
		{
			darah = 20;
		}
		else if(darah <0)
		{
			darah = 0;
		}
	}

	public void Serangcross(int cekb)
	{
		if(cekb == 1)
		{
			ceka2 = 2;
		}

	}
	public void Repair(int cekc)
	{
		if(cekc == 1)
		{
			cek3 = 1;
		}
		else if(cekc==2)
		{
			cek3 = 2;
		}
	}

	public void TebangPOhon(int ceka)
	{
		if(ceka == 1)
		{
			ceknyerang = true;
			ceka1 = 1;
		}
		else if(ceka == 2)
		{
			ceknyerang = false;
			ceka1 = 2;
		}

	}

	IEnumerator Keluarpeluru()
	{
		
		if (a > 0 ) {
			efek [4].enabled = false;
			a -= Time.deltaTime;
			yield return 0;
		} else {
			efek [4].enabled = true;
			GameObject NewTombak = Instantiate (pelurunya, posisipelurunya.transform.position, posisipelurunya.transform.rotation) as GameObject ;
			//Instantiate (pelurunya, posisipelurunya.transform.position, posisipelurunya.transform.rotation);
			NewTombak.transform.parent = ParentAR.transform.transform;
			NewTombak.transform.localScale = pelurunya.transform.localScale;
			amuni -= 1;
			a = 0.5f;
			NyerangUdah = false;
			ceka2 = 1;
		}

	}

	IEnumerator Repairpesawat()
	{
		
		if (b > 0 ) {
			b -= Time.deltaTime;
			yield return 0;
		} else {
			darah += 10;
			cek3 = 2;
			b = 3;
			Anim.SetBool ("buat",false);
			Debug.Log ("s");
			kayu -= 5;
		}

	}

	private void Move()
	{
		transform.rotation = Quaternion.Euler(MoveVector2);
		transform.Translate(0,0, movespeed*Time.deltaTime);

		PrefabPemain.transform.rotation = this.transform.rotation;
		PrefabPemain.transform.position = this.transform.position;
	
	}

	private Vector3 PoolInput()
	{
		Vector3 dir = Vector3.zero;

		dir.x = Input.GetAxis ("Horizontal")  ;
		dir.z = Input.GetAxis ("Vertical");
	
		if(dir.magnitude > 1)
		{
			dir.Normalize ();

		}

		if(joystick.inputvector != Vector3.zero)
		{
			dir = joystick.inputvector;
		}
		return dir;
	}

	private Vector3 PoolInput2()
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
			Anim.SetBool ("jalan", true);
			thisrigid.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
			//Anim.SetBool ("nyerang",false);
			senjata [0].SetActive (true);
		} else {
			
			dir.y = CurrRotasi.y;
			Anim.SetBool("jalan",false);

			//Anim.SetBool ("Nyerang",false);
		}


		return dir;
	}

	void OnCollisionEnter(Collision col)
	{
		
		if(col.gameObject.tag == "Peluru")
		{
			Destroy (col.gameObject);
			darahplayernya -= 1;
		}
		if(col.gameObject.tag == "matii")
		{
			
			thisrigid.constraints = RigidbodyConstraints.FreezePositionY;
		}
		if(col.gameObject.tag == "Pesawat")
		{

			thisrigid.constraints = RigidbodyConstraints.FreezePositionY;
		}
	
	}



	void OnCollisionExit(Collision col)
	{

		 if(col.gameObject.tag == "matii")
		{
			//RigidbodyConstraints.FreezePositionY 
			thisrigid.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition ;
		}
		if(col.gameObject.tag == "Pesawat")
		{
			//RigidbodyConstraints.FreezePositionY 
			thisrigid.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition ;
		}
	}

	void OnTriggerStay(Collider col)
	{
		
		if(col.gameObject.tag == "pesawat2")
		{
			repairnya = true;
			for (int i = 0; i <Kumpulansenjata.Length; i++) {
				Kumpulansenjata [i].SetActive (false);
			}
			Kumpulansenjata [2].SetActive (true);
		}
		if(col.gameObject.tag == "daerah")
		{
			daerahbangun = true;

		}
		if(col.gameObject.tag == "Peluru")
		{
			Destroy (col.gameObject);
			darahplayernya -= 1;
		}
	}

	void OnTriggerExit(Collider col)
	{
		
		if(col.gameObject.tag == "pesawat2")
		{
			repairnya = false;
			Anim.SetBool ("buat",false);
			b = 3;
			for (int i = 0; i <Kumpulansenjata.Length; i++) {
				Kumpulansenjata [i].SetActive (false);
			}
			Kumpulansenjata [0].SetActive (true);
		}
		if(col.gameObject.tag == "daerah")
		{
			daerahbangun = false;

		}
	}
		
}
