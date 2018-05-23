using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockScene : MonoBehaviour {

	public int LevelIndex;
	Image ThisImage;
	Button ThisButton;

	// Use this for initialization
	void Start () {
		ThisImage = GetComponent<Image> ();
		ThisButton = GetComponent<Button> ();

		if(ManagerAllScene.Control.LevelTelahSelesai < LevelIndex)
		{
			ThisImage.color = new Color32 (255,255,255,125);
			ThisButton.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
