using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerGame : MonoBehaviour {
	public static ManagerGame Instance;

	[Space]
	[Header ("Manage Pohon")]
	public List<TebangPohon> DaftarPohonDidalamScene;
	public TebangPohon PohonSasaran;

	[Space]
	[Header ("Manage Musuh")]
	public List<AiMusuhKejarTarget> DaftarMusuhDidalamScene;

	[Space]
	public float DarahBarInt;
	public Image DarahBar;

	public int JumlahKayu;
	public Text JumlahKayuText;

	public int JumlahAmunisi;
	public Text JumlahAmunisiText;

	public GameObject[] ManageTrap;

	public int WaveLevel;
	public int JumlahMusuhKeluar;
	public int[] JumlahMaksimalMusuh;

	public int JumlahMusuhTerbunuh;
	public int MaxJumlahMusuhTerbunuh;

	public bool WaktuSiang;

	public bool MusuhKeluarWave;

	void Awake()
	{
		Instance = this;
		PohonSasaran = null;
	}

	void Start()
	{
		for (int i = 0; i < ManageTrap.Length; i++) {
			ManageTrap [i].SetActive(false) ;
		}
		ManageTrap [0].SetActive(true) ;
	}

	void Update()
	{
		if (PohonSasaran != null) {
			if ((Vector3.Distance (PohonSasaran.transform.position, MasterPlayer.instance.transform.position) < 1)) {
				MasterPlayer.instance.CekUdahDeketPohon = true;
			} else {
				MasterPlayer.instance.CekUdahDeketPohon = false;
			}

		} else {
			MasterPlayer.instance.CekUdahDeketPohon = false;
		}

		DarahBar.fillAmount = DarahBarInt / 10;
		JumlahKayuText.text = "" + JumlahKayu;
		JumlahAmunisiText.text = "" + JumlahAmunisi;
	}

	public void TutupMenuTrap()
	{
		for (int i = 0; i < ManageTrap.Length; i++) {
			ManageTrap [i].SetActive(false) ;	
		}
		ManageTrap [0].SetActive(true) ;	}

	public void BukaMenuTrap()
	{
		for (int i = 0; i < ManageTrap.Length; i++) {
			ManageTrap [i].SetActive(true) ;
		}
		ManageTrap [0].SetActive(false) ;	
	}
}
