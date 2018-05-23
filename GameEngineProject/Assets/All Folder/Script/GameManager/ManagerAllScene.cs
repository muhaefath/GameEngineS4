using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAllScene : MonoBehaviour {

	public static ManagerAllScene Control;
	public int LevelTelahSelesai;

	void Awake()
	{
		if (Control == null) {
			DontDestroyOnLoad (gameObject);
			Control = this;
		} else {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
