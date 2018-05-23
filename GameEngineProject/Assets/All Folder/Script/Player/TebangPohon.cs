using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TebangPohon : MonoBehaviour {
	public bool CekUdahDitebang = false;
	public bool PlayerUdahDeket = false;

	public Image BarProgressTebangPohon;
	public float JumlahBarTebangPohon;

	public Animator Anim;

	public GameObject DaunPohon;
	public GameObject BatangPohon;

	void Awake()
	{
		
	}
	void Start()
	{
		DaunPohon = transform.GetChild (1).gameObject;
		BatangPohon = transform.GetChild (2).gameObject;

		JumlahBarTebangPohon = 1f;
		BarProgressTebangPohon = GetComponentInChildren<Image> ();
		Anim = GetComponent<Animator> ();
		BarProgressTebangPohon.enabled = false;
		ManagerGame.Instance.DaftarPohonDidalamScene.Add (this);

		//BarProgressTebangPohon.fillAmount = JumlahBarTebangPohon;
	}

	void Update()
	{
		BarProgressTebangPohon.fillAmount = JumlahBarTebangPohon;
		if (Vector3.Distance (this.transform.position, MasterPlayer.instance.transform.position) < 1 && CekUdahDitebang == false) {


			PlayerUdahDeket = true;
			ManagerGame.Instance.PohonSasaran = this;


		} else {

			PlayerUdahDeket = false;

		}
			
		if(CekUdahDitebang == true)
		{
			StartCoroutine (HilangDulu());
		}

	}

	public IEnumerator HilangDulu(){
		yield return new WaitForSeconds (70f);
		DaunPohon.SetActive (true);
		BatangPohon.SetActive (true);
		CekUdahDitebang = false;
	}
}
