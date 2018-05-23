using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {
	public float speed;

	public float HitungWaktu;

	void Start()
	{
		
	}
	void Update()
	{
		
		if(this.transform.rotation == Quaternion.Euler(190,0,0))
		{
			
			//ManagerBuilding.Instance.MalamDatang = true;
			//ManagerBuilding.Instance.MenuBuilding [4].SetActive (true);
			//Time.timeScale = 0;
			//return;
		}
		this.transform.Rotate (speed*Time.deltaTime , 0, 0);


		if (this.transform.rotation.eulerAngles.x > 0 && this.transform.rotation.eulerAngles.x < 180) {
			speed = 3f;
			ManagerGame.Instance.WaktuSiang = true;
			ManagerGame.Instance.MusuhKeluarWave = false;
			HitungWaktu += Time.deltaTime;
		} else 
		{
			speed = 6;

			HitungWaktu += Time.deltaTime;
			ManagerGame.Instance.WaktuSiang = false;

		}
	}

	public void LanjutPagi()
	{
		
		//Time.timeScale = 1;
		//ManagerBuilding.Instance.MalamDatang = false;
		//ManagerBuilding.Instance.MenuBuilding [4].SetActive (false);
		this.transform.rotation = Quaternion.Euler (0, 0, 0);
		this.transform.Rotate (speed*Time.deltaTime , 0, 0);
	}
}
