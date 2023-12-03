using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SC_SlotTemplate : MonoBehaviour, IPointerClickHandler{
	
	public Image container;
	public Image item;
	public TextMeshProUGUI count;
	
	[HideInInspector]
	public bool hasClicked = false;
	[HideInInspector]
	public SC_ItemCrafting craftingController;
	
	public void OnPointerClick(PointerEventData eventData){
		hasClicked = true;
		craftingController.ClickEventRecheck();
	}
}
