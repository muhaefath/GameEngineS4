using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class AiPartner : MonoBehaviour {

	public float Speed;

	public float WaktuHidup;
	public Image BarHidup;


	public GameObject KapakDipegang;
	public GameObject TombakDipegang;

	public float WaktuJedaTebangPohon;
	public TebangPohon SasaranPohon;

	public float WaktuJedaSerangMusuh;
	public AiMusuhKejarTarget SasaranMusuh;
	public GameObject TowerCandi;
	public GameObject PeluruPartner;
	public Transform PosisiPeluru;

	Animator AnimatorKarakrer;

	public int IndexPohon;

	 NavMeshAgent PartnerAgent;
	public NavMeshHit hit;

	// Use this for initialization
	void Start () {
		PartnerAgent = GetComponent<NavMeshAgent> ();
		AnimatorKarakrer = GetComponent<Animator> ();
		//IndexPohon = 
		PartnerAgent.Warp(this.transform.position);


	}
	
	// Update is called once per frame
	void Update () {
		BarHidup.fillAmount = WaktuHidup / 30;

		AIMaster ();
		StartCoroutine (HitungWaktuHidup());
	}

	void AIMaster()
	{
		if (ManagerGame.Instance.DaftarMusuhDidalamScene.Count == 0) {
			if (ManagerGame.Instance.DaftarPohonDidalamScene.Count > 0) {

				CariPohon ();

			} else {
		
				AnimatorKarakrer.SetBool ("Tebang",false);
				KapakDipegang.SetActive (false);
				TombakDipegang.SetActive (true);
				WaktuJedaTebangPohon = 2f;
			}
		} else  {
			
			SasaranMusuh = ManagerGame.Instance.DaftarMusuhDidalamScene [0];
			for (int i = 0; i < ManagerGame.Instance.DaftarMusuhDidalamScene.Count; i++) {
				if ((TowerCandi.transform.position - ManagerGame.Instance.DaftarMusuhDidalamScene [i].transform.position).magnitude < (TowerCandi.transform.position - SasaranMusuh.transform.position).magnitude) {
					SasaranMusuh = ManagerGame.Instance.DaftarMusuhDidalamScene [i];

				}

			}

			CariMusuh ();
		} 
	}

	void CariPohon()
	{
		SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [0] ;
		for (int i = 0; i < ManagerGame.Instance.DaftarPohonDidalamScene.Count; i++) {
			if((this.transform.position -  ManagerGame.Instance.DaftarPohonDidalamScene [i].transform.position).magnitude < (this.transform.position -  SasaranPohon.transform.position).magnitude)
			{
				SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [i];
			}

		}



		Vector3 SelisihPosisiPlayer = SasaranPohon.transform.position - this.transform.position ;

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation,Quaternion.LookRotation(SelisihPosisiPlayer),0.1f);
		if (Vector3.Distance (SasaranPohon.transform.position, this.transform.position) > 1.5f) {
			AnimatorKarakrer.SetBool ("Jalan",true);
			//this.transform.Translate (0, 0, Speed * Time.deltaTime);

			PartnerAgent.SetDestination (SasaranPohon.transform.position);

		} else {
			
			AnimatorKarakrer.SetBool ("Jalan",false);
			StartCoroutine (TebangPohon());

		

		}
	}

	 IEnumerator TebangPohon()
	{

		if (WaktuJedaTebangPohon > 0 ) {
			KapakDipegang.SetActive (true);
			TombakDipegang.SetActive (false);

			WaktuJedaTebangPohon -= Time.deltaTime;

			
			AnimatorKarakrer.SetBool ("Tebang",true);

		
		} else {
			ManagerGame.Instance.JumlahKayu += 10;
			KapakDipegang.SetActive (false);
			TombakDipegang.SetActive (true);

			ManagerGame.Instance.DaftarPohonDidalamScene.Remove (SasaranPohon);
			Destroy (SasaranPohon.gameObject);

			if(ManagerGame.Instance.DaftarPohonDidalamScene.Count > 0)
			{
				SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [0] ;

				for (int i = 0; i < ManagerGame.Instance.DaftarPohonDidalamScene.Count; i++) {
					if((this.transform.position -  ManagerGame.Instance.DaftarPohonDidalamScene [i].transform.position).magnitude < (this.transform.position -  SasaranPohon.transform.position).magnitude)
					{
						SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [i];
					}
			
				}
			}

			AnimatorKarakrer.SetBool ("Tebang",false);

			WaktuJedaTebangPohon = 2f;
			yield return 0;
		}
	}

	void CariMusuh()
	{
		
		KapakDipegang.SetActive (false);
		TombakDipegang.SetActive (true);

		AnimatorKarakrer.SetBool ("Tebang",false);
		WaktuJedaTebangPohon = 2f;

		

		
		Vector3 SelisihPosisiPlayer = SasaranMusuh.transform.position - this.transform.position ;

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation,Quaternion.LookRotation(SelisihPosisiPlayer),0.1f);
		if (Vector3.Distance (SasaranMusuh.transform.position, this.transform.position) > 2) {
			PartnerAgent.speed = 1;
			AnimatorKarakrer.SetBool ("Jalan",true);
			//this.transform.Translate (0, 0, Speed * Time.deltaTime);
			PartnerAgent.SetDestination (SasaranMusuh.transform.position);

		} else {
			PartnerAgent.speed = 0;
			StartCoroutine (TembakMusuh());


		}
	}

	IEnumerator TembakMusuh()
	{
		if (WaktuJedaSerangMusuh > 0) {
			

			WaktuJedaSerangMusuh -= Time.deltaTime;


			AnimatorKarakrer.Play ("Tembak");

		} else {
			
			Instantiate (PeluruPartner,PosisiPeluru.transform.position,PosisiPeluru.transform.rotation);

			WaktuJedaSerangMusuh = 1f;
			yield return 0;
		}
	}

	IEnumerator HitungWaktuHidup()
	{
		if (WaktuHidup > 0) {
			WaktuHidup -= Time.deltaTime;
			yield return 0;
		} else {
			Destroy (gameObject);
		}
	}

}
