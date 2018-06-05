﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (NextScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator NextScene()
	{
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("MenuUtama");
	}
}
