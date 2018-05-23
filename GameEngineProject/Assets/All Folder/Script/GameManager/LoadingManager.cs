using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour {

	public GameObject[] KumpululanLoadingBar;
	public GameObject[] KumpulanTips;
	public int IndexLoadingBar;
	public float WaktuLoading;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < KumpulanTips.Length; i++) {
			KumpulanTips [i].SetActive (false);
		}
		KumpulanTips [Random.Range (0, 8)].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (PindahScene());
	}

	void LateUpdate(){
		
	}

	IEnumerator PindahScene()
	{
		if (WaktuLoading > 0) {
			WaktuLoading -= Time.deltaTime;
			yield return 0;
		} else {
			if (IndexLoadingBar == 5) {
				IndexLoadingBar = -1;

				/*
				for (int i = 0; i < KumpululanLoadingBar.Length; i++) {
					KumpululanLoadingBar [i].SetActive (false);
				}
				*/
				SceneManager.LoadScene (PlayerPrefs.GetString ("Scene"));

			} else {
				KumpululanLoadingBar [IndexLoadingBar].SetActive (true);
			}
			WaktuLoading = 0.6f;




			IndexLoadingBar += 1;
		}

	}


}
