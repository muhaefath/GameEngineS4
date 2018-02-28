using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMusuhKeluar : MonoBehaviour {

	public GameObject MusuhPrefab;
	public Transform[] PosisiKeluarMusuh;

	public float WaktuJedaMusuhKeluar = 3f;


	public void Update()
	{
		StartCoroutine (KeluarMusuh());
	}

	public IEnumerator KeluarMusuh()
	{
		
		if (WaktuJedaMusuhKeluar >= 0) {
			WaktuJedaMusuhKeluar -= Time.deltaTime;
			yield return 0;
		} else {
			int IndexPosisi = Random.Range (0,3);
			Instantiate (MusuhPrefab,PosisiKeluarMusuh[IndexPosisi].position,PosisiKeluarMusuh[IndexPosisi].rotation);
			WaktuJedaMusuhKeluar = 3;
		}

	}
}
