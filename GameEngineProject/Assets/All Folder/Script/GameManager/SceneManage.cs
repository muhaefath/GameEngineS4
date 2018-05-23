﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour {

	public void PindahScene(string NamaScene)
	{
		PlayerPrefs.SetString ("Scene",NamaScene);

		SceneManager.LoadScene ("LoadingScreen");
	}

	public void PindahScene2(string NamaScene)
	{
		PlayerPrefs.SetString ("Scene",NamaScene);

		SceneManager.LoadScene (NamaScene);
	}

	public void NextLevel()
	{
		SceneManager.LoadScene ("Main" + (PlayerPrefs.GetInt("Level") + 1 ) );
	}
	public void PlayAgain()
	{
		SceneManager.LoadScene ("Main" + PlayerPrefs.GetInt("Level") );
	}


	public void KeluarGame()
	{
		Application.Quit ();
	}


}
