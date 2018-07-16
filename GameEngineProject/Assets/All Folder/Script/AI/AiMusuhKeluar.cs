using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMusuhKeluar : MonoBehaviour {

	public GameObject MusuhPrefab;
	public Transform[] PosisiKeluarMusuh;

	public float WaktuJedaMusuhKeluar = 3f;

	public DayNight Matahari;



	public void Update()
	{
		if (ManagerGame.Instance.WaktuSiang == false && ManagerGame.Instance.MusuhKeluarWave == false && ManagerGame.Instance.WaveLevel < ManagerGame.Instance.JumlahMaksimalMusuh.Length) {
			if (ManagerGame.Instance.JumlahMusuhKeluar < ManagerGame.Instance.JumlahMaksimalMusuh[ManagerGame.Instance.WaveLevel]) {
				Matahari.speed = 0;
				StartCoroutine (KeluarMusuh ());
			} else {
				Matahari.speed = 5;
				ManagerGame.Instance.MusuhKeluarWave = true;
				ManagerGame.Instance.WaveLevel += 1;
				ManagerGame.Instance.JumlahMusuhKeluar = 0;
			}
		} 
	}

	public IEnumerator KeluarMusuh()
	{
		
		if (WaktuJedaMusuhKeluar >= 0) {
			WaktuJedaMusuhKeluar -= Time.deltaTime;
			yield return 0;
		} else {
			int IndexPosisi = Random.Range (0,3);
			 
			GameObject musuh = Instantiate (MusuhPrefab,PosisiKeluarMusuh[IndexPosisi].position,PosisiKeluarMusuh[IndexPosisi].rotation) as GameObject;

			ManagerGame.Instance.JumlahMusuhKeluar += 1;
			WaktuJedaMusuhKeluar = 3;
		}

	}
}
