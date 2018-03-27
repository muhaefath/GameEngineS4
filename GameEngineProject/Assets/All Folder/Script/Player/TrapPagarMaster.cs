using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrapPagarMaster : MonoBehaviour {
	public float HealthTrap;
	public Image HealthBar;

	void Start()
	{
		//transform.rotation = Quaternion.Euler (-90,180,0);
		HealthBar.fillAmount = HealthTrap / 5;
	}


	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "PeluruMusuh")
		{
			HealthTrap -= 1;
			HealthBar.fillAmount = HealthTrap / 5;
			Destroy (col.gameObject);
			if(HealthTrap == 0)
			{
				Destroy (gameObject);
			}
		}
	}
}
