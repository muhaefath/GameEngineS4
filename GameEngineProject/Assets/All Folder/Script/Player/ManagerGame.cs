using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour {
	public static ManagerGame Instance;
	public List<TebangPohon> DaftarPohonDidalamScene;
	public TebangPohon PohonSasaran;

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
}
