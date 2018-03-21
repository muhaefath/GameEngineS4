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

			inputvector = new Vector3 (pos.x * 2- 1,0, pos.y*2 -1);
			inputvector = (inputvector.magnitude > 1.0f) ? inputvector.normalized: inputvector;

			//joymove
			//Debug.Log(inputvector);
			virjoystrick.rectTransform.anchoredPosition = new Vector3 (inputvector.x * (background.rectTransform.sizeDelta.x/2), inputvector.z * (background.rectTransform.sizeDelta.y/2));
		//	virjoystrick.rectTransform.anchoredPosition = new Vector3 (inputvector.y * (background.rectTransform.sizeDelta.y/2));
				
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputvector = Vector3.zero;
		virjoystrick.rectTransform.anchoredPosition = Vector3.zero;

	}


	public void JoystcikNembak()
	{
		
		MasterPlayer.instance.TembakPeluruBool = true;
	}

	public void JoystcikDiangkat()
	{
		if(MasterPlayer.instance.CekUdahDeketPohon == true)
		{
			MasterPlayer.instance.LepasButtonTebangPohon ();
		}
	}
}
