using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour {

	public void PindahScene(string NamaScene)
	{
		SceneManager.LoadScene (NamaScene);
	}

	public void KeluarGame()
	{
		Application.Quit ();
	}


}
