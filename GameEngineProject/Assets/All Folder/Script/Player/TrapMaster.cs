using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrapMaster : MonoBehaviour {

	Animator AnimatorTrap;

	void Start()
	{
		//this.transform.position = new Vector3 (this.transform.position.x,0,this.transform.position.z);
		AnimatorTrap = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			AnimatorTrap.Play ("Attack");
			Destroy (col.gameObject);

			Debug.Log (col.gameObject.GetComponent<AiMusuhKejarTarget>());
			ManagerGame.Instance.DaftarMusuhDidalamScene.Remove (col.gameObject.GetComponent<AiMusuhKejarTarget>());
		
			ManagerGame.Instance.JumlahMusuhTerbunuh += 1;

			if(ManagerGame.Instance.JumlahMusuhTerbunuh == ManagerGame.Instance.MaxJumlahMusuhTerbunuh)
			{
				ManagerAllScene.Control.BackSoundWinning.enabled = true;
				PlayerPrefs.SetString ("Scene","Winning");
				PlayerPrefs.SetInt ("AllScene",PlayerPrefs.GetInt("Level"));
				SceneManager.LoadScene ("LoadingScreen");

			}
		}
	}
}
