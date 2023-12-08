using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingItemSlot : MonoBehaviour, IDropHandler {

	public void OnDrop(PointerEventData eventData){
		
		if (eventData.pointerDrag != null){
			eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
		}
	}

}
