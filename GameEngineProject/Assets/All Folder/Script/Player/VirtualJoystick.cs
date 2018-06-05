using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler,IPointerUpHandler,IPointerDownHandler {

	private Image background;
	private Image virjoystrick;
	public Vector3 inputvector;


	public  Vector3 SimpanRot;

	private void Start()
	{
		background = GetComponent<Image> ();
		virjoystrick = transform.GetChild (0).GetComponent<Image> ();

	}




	public virtual void OnDrag(PointerEventData ped)
	{
		MasterPlayer.instance.TombakDipegang.SetActive (true);
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform,ped.position,ped.pressEventCamera,out pos))
		{
			pos.x = (pos.x / background.rectTransform.sizeDelta.x);
			pos.y = (pos.y / background.rectTransform.sizeDelta.y);

			inputvector = new Vector3 (pos.x ,0, pos.y);
			inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized: inputvector;

			//joymove
			//Debug.Log(inputvector);
			virjoystrick.rectTransform.anchoredPosition = new Vector3 (inputvector.x * (background.rectTransform.sizeDelta.x/2), inputvector.z * (background.rectTransform.sizeDelta.y/2));
		//	virjoystrick.rectTransform.anchoredPosition = new Vector3 (inputvector.y * (background.rectTransform.sizeDelta.y/2));
					
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		MasterPlayer.instance.AudioPlayer[0].Play();
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputvector = Vector3.zero;
		virjoystrick.rectTransform.anchoredPosition = Vector3.zero;
		MasterPlayer.instance.AudioPlayer[0].Stop();	
	}


	public void JoystcikNembak()
	{
		
		if(ManagerGame.Instance.JumlahAmunisi > 0)
		{
			MasterPlayer.instance.TembakPeluruBool = true;
		}
		if (MasterPlayer.instance.CekUdahDeketPohon) {
			MasterPlayer.instance.AudioPlayer [2].Play ();
		}
	}

	public void JoystcikDiangkat()
	{
		inputvector = Vector3.zero;
		virjoystrick.rectTransform.anchoredPosition = Vector3.zero;

		if(MasterPlayer.instance.CekUdahDeketPohon == true)
		{
			MasterPlayer.instance.LepasButtonTebangPohon ();
			MasterPlayer.instance.AudioPlayer [2].Stop ();
			return;
		}

	}

	public void TombolTrapDiangkat()
	{
		MasterPlayer.instance.LepasButtonRakitTrap ();
		MasterPlayer.instance.AudioPlayer [3].Stop ();
	}

	public void JoystickRakitTrap(int IndexTrap)
	{
		MasterPlayer.instance.AudioPlayer [3].Play ();
		if (IndexTrap == 0) {
			MasterPlayer.instance.CekTombolJebakanDipencetTrap1 = true;
		} else if(IndexTrap == 1)
		{
			MasterPlayer.instance.CekTombolJebakanDipencetTrap2 = true;
		}else if(IndexTrap ==2)
		{
			MasterPlayer.instance.CekTombolBuatPartner = true;
		}
	}

	public void BeliPeluru()
	{
		if(ManagerGame.Instance.JumlahKayu >= 5)
		{
			ManagerGame.Instance.JumlahKayu -= 5;
			ManagerGame.Instance.JumlahAmunisi += 10;
		}
	}
}
