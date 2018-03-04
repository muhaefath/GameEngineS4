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
		
	}
}
