using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TebangPohon : MonoBehaviour {
	public bool CekUdahDitebang = false;
	public bool PlayerUdahDeket = false;

	public Image BarProgressTebangPohon;
	public float JumlahBarTebangPohon;



	void Awake()
	{
		
	}
	void Start()
	{
		JumlahBarTebangPohon = 1f;
		BarProgressTebangPohon = GetComponentInChildren<Image> ();
		BarProgressTebangPohon.enabled = false;
		ManagerGame.Instance.DaftarPohonDidalamScene.Add (this);

		//BarProgressTebangPohon.fillAmount = JumlahBarTebangPohon;
	}

	void Update()
	{
		BarProgressTebangPohon.fillAmount = JumlahBarTebangPohon;
		if (Vector3.Distance (this.transform.position, MasterPlayer.instance.transform.position) < 1) {



			PlayerUdahDeket = true;
			ManagerGame.Instance.PohonSasaran = this;
			MasterPlayer.instance.CekUdahDeketPohon = true;
		} else {

			PlayerUdahDeket = false;

		}
			
		if(CekUdahDitebang == true)
		{
			Destroy (gameObject);
		}
	}
}
