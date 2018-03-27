using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMusuhKejarTarget : MonoBehaviour {

	public float Speed;
	public GameObject TemplePusat;
	public Animator AnimMusuh;

	public Transform PosisiPeluru;
	public GameObject Peluru;
	// Update is called once per frame
	public float WaktuJedaNyerang;

	void Start()
	{
		ManagerGame.Instance.DaftarMusuhDidalamScene.Add (this);
		AnimMusuh = GetComponent<Animator> ();
	}
	void Update () {
		
		AIJalan ();
	}

	void AIJalan()
	{
		
		Vector3 SelisihPosisiTemple =  TemplePusat.transform.position - this.transform.position ;
		Vector3 SelisihPosisiPlayer =  MasterPlayer.instance.transform.position - this.transform.position ;


		if (Vector3.Distance (MasterPlayer.instance.transform.position, this.transform.position) < 10) {
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation,Quaternion.LookRotation(SelisihPosisiPlayer),0.1f);
			this.transform.Translate (0,0,Speed * Time.deltaTime);
			if (Vector3.Distance (MasterPlayer.instance.transform.position, this.transform.position) < 5) {
				AnimMusuh.Play ("Nyerang");
				StartCoroutine (JedaNyerangMusuh());
				if (Vector3.Distance (MasterPlayer.instance.transform.position, this.transform.position) < 1) {
					Speed = 0;	
				} else {
					Speed = 1;	
				}
			} else {
				WaktuJedaNyerang =  0.8f;
			}
		} else {
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation,Quaternion.LookRotation(SelisihPosisiTemple),0.1f);
			if (Vector3.Distance (TemplePusat.transform.position, this.transform.position) < 5) {
				AnimMusuh.Play ("Nyerang");
				StartCoroutine (JedaNyerangMusuh());
			} else {
				
				this.transform.Translate (0,0,Speed * Time.deltaTime);

				WaktuJedaNyerang =  0.8f;
			}
		}

	}

	IEnumerator JedaNyerangMusuh()
	{
		if (WaktuJedaNyerang > 0) {
			WaktuJedaNyerang -= Time.deltaTime;
			yield return 0;
		} else {
			Instantiate (Peluru,PosisiPeluru.position,PosisiPeluru.rotation);
			WaktuJedaNyerang =  0.8f;
		}

	}

	/*
	void MatiKarenaTombak()
	{
		
	}
	*/
}
