using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
	
	public enum ItemType {
		YellowBone,
		GreenGem,
		BlueGreenGem,
		PurpleGem,
		SilverGem,
		GoldMetal,
		SilverMetal,
		Fabric,
		Sword_1,
		Sword_2, 
		Sword_3,
		Armorbody_1,
		Armorhat_1,
		Armorbody_2,
		Armorhat_2,
		Armorbody_3,
		Armorhat_3,
		SilverPotion,
		GreenPotion,
	}
	
	public ItemType itemType;
	public int amount;
	
	public Sprite GetSprite(){
		switch (itemType){
		default:
		case ItemType.YellowBone: 	return ItemAssets.Instance.YellowBoneSprite;
		case ItemType.GreenGem: 	return ItemAssets.Instance.GreenGemSprite;
		case ItemType.BlueGreenGem: return ItemAssets.Instance.BlueGreenGemSprite;
		case ItemType.PurpleGem: 	return ItemAssets.Instance.PurpleGemSprite;
		case ItemType.SilverGem: 	return ItemAssets.Instance.SilverGemSprite;
		case ItemType.GoldMetal: 	return ItemAssets.Instance.GoldMetalSprite;
		case ItemType.SilverMetal: 	return ItemAssets.Instance.SilverMetalSprite;
		case ItemType.Fabric: 		return ItemAssets.Instance.FabricSprite;
		case ItemType.Sword_1: 		return ItemAssets.Instance.Sword_1Sprite;
		case ItemType.Sword_2: 		return ItemAssets.Instance.Sword_2Sprite;
		case ItemType.Sword_3: 		return ItemAssets.Instance.Sword_3Sprite;
		case ItemType.Armorbody_1:	return ItemAssets.Instance.Armorbody_1Sprite;
		case ItemType.Armorhat_1: 	return ItemAssets.Instance.Armorhat_1Sprite;
		case ItemType.Armorbody_2: 	return ItemAssets.Instance.Armorbody_2Sprite;
		case ItemType.Armorhat_2: 	return ItemAssets.Instance.Armorhat_2Sprite;
		case ItemType.Armorbody_3: 	return ItemAssets.Instance.Armorbody_3Sprite;
		case ItemType.Armorhat_3: 	return ItemAssets.Instance.Armorhat_3Sprite;
		case ItemType.SilverPotion: return ItemAssets.Instance.SilverPotionSprite;
		case ItemType.GreenPotion: 	return ItemAssets.Instance.GreenPotionSprite;
		}
	}
	
	public bool IsStackable(){
		switch (itemType){
		default:
		case ItemType.YellowBone: 	
		case ItemType.GreenGem: 	
		case ItemType.BlueGreenGem: 
		case ItemType.PurpleGem: 	
		case ItemType.SilverGem: 	
		case ItemType.GoldMetal: 	
		case ItemType.SilverMetal: 	
		case ItemType.Fabric: 	
		case ItemType.SilverPotion: 
		case ItemType.GreenPotion:
			return true;
		case ItemType.Sword_1: 		
		case ItemType.Sword_2: 		
		case ItemType.Sword_3: 		
		case ItemType.Armorbody_1:
		case ItemType.Armorhat_1: 	
		case ItemType.Armorbody_2: 	
		case ItemType.Armorhat_2: 	
		case ItemType.Armorbody_3: 	
		case ItemType.Armorhat_3: 	
			return false;
		}
	}
	
}
