using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		if(ManagerAllScene.Control.LevelTelahSelesai < PlayerPrefs.GetInt ("Level"))
		{
			ManagerAllScene.Control.LevelTelahSelesai = PlayerPrefs.GetInt ("Level");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
