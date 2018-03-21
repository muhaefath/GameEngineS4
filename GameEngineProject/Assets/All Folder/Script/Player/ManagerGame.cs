using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour {
	public static ManagerGame Instance;

	[Space]
	[Header ("Manage Pohon")]
	public List<TebangPohon> DaftarPohonDidalamScene;
	public TebangPohon PohonSasaran;

	[Space]
	[Header ("Manage Musuh")]
	public List<AiMusuhKejarTarget> DaftarMusuhDidalamScene;


	void Awake()
	{
		Instance = this;
		PohonSasaran = null;
	}

	void Update()
	{
		if(PohonSasaran!=null)
		{
			if((Vector3.Distance (PohonSasaran.transform.position, MasterPlayer.instance.transform.position) > 2))
			{
				MasterPlayer.instance.CekUdahDeketPohon = false;
			}
		}

	}

	public void RakitJebakan()
	{
		
	}
}
