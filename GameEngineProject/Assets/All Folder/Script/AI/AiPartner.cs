using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPartner : MonoBehaviour {

	public float Speed;

	public float WaktuJedaTebangPohon;
	public GameObject KapakDipegang;
	public GameObject TombakDipegang;

	public TebangPohon SasaranPohon;

	Animator AnimatorKarakrer;

	public int IndexPohon;

	// Use this for initialization
	void Start () {
		AnimatorKarakrer = GetComponent<Animator> ();
		//IndexPohon = 
		SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [0] ;
	


		for (int i = 0; i < ManagerGame.Instance.DaftarPohonDidalamScene.Count; i++) {
			if((this.transform.position -  ManagerGame.Instance.DaftarPohonDidalamScene [i].transform.position).magnitude < (this.transform.position -  SasaranPohon.transform.position).magnitude)
			{
				SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [i];
			}
			//Debug.Log ( (this.transform.position -  ManagerGame.Instance.DaftarPohonDidalamScene [i].transform.position).magnitude);
		}


	}
	
	// Update is called once per frame
	void Update () {
		
		if(ManagerGame.Instance.DaftarPohonDidalamScene.Count > 0)
		{
			// kalo malem dia cari pohon
			CariPohon();
		}
	}

	void CariPohon()
	{
		
		Vector3 SelisihPosisiPlayer = SasaranPohon.transform.position - this.transform.position ;

		this.transform.rotation = Quaternion.Slerp (this.transform.rotation,Quaternion.LookRotation(SelisihPosisiPlayer),0.1f);
		if (Vector3.Distance (SasaranPohon.transform.position, this.transform.position) > 1) {
			this.transform.Translate (0, 0, Speed * Time.deltaTime);

		} else {
			StartCoroutine (TebangPohon());
		}
	}

	 IEnumerator TebangPohon()
	{

		if (WaktuJedaTebangPohon > 0) {
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
			SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [0] ;

			for (int i = 0; i < ManagerGame.Instance.DaftarPohonDidalamScene.Count; i++) {
				if((this.transform.position -  ManagerGame.Instance.DaftarPohonDidalamScene [i].transform.position).magnitude < (this.transform.position -  SasaranPohon.transform.position).magnitude)
				{
					SasaranPohon = ManagerGame.Instance.DaftarPohonDidalamScene [i];
				}
				//Debug.Log ( (this.transform.position -  ManagerGame.Instance.DaftarPohonDidalamScene [i].transform.position).magnitude);
			}

			AnimatorKarakrer.SetBool ("Tebang",false);

			WaktuJedaTebangPohon = 2f;
			yield return 0;
		}
	}
}
