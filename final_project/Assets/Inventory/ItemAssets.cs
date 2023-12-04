using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
	public static ItemAssets Instance { get; private set;}
	
	private void Awake(){
		Instance = this;
	}

	public Transform pfItemWorld;
	
	public Sprite YellowBoneSprite;
	public Sprite GreenGemSprite;
	public Sprite BlueGreenGemSprite;
	public Sprite PurpleGemSprite;
	public Sprite SilverGemSprite;
	public Sprite GoldMetalSprite;
	public Sprite SilverMetalSprite;
	public Sprite FabricSprite;
	public Sprite Sword_1Sprite;
	public Sprite Sword_2Sprite;
	public Sprite Sword_3Sprite;
	public Sprite Armorbody_1Sprite;
	public Sprite Armorhat_1Sprite;
	public Sprite Armorbody_2Sprite;
	public Sprite Armorhat_2Sprite;
	public Sprite Armorbody_3Sprite;
	public Sprite Armorhat_3Sprite;
	public Sprite SilverPotionSprite;
	public Sprite GreenPotionSprite;
	
}
