using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

	
    void Start()
    {
        
		CraftingSystem craftingSystem = new CraftingSystem();
		Item item = new Item {itemType = Item.ItemType.GreenGem, amount = 1};
		craftingSystem.SetItem(item, 0, 0);
		Debug.Log(craftingSystem.GetItem(0, 0).itemType);
		
    }

}
