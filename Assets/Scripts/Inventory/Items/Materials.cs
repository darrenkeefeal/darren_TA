using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Items {

	public Materials(string itemName, string itemDesc, ItemType itemType, Quality quality, string spriteNeutral , string spriteHighlight, int maxSize)
		: base(itemName,itemDesc,itemType,quality,spriteNeutral,spriteHighlight,maxSize,1)
	{
		
	}

	public override void Use (Slots slot, Items_Script item)
	{
		
	}

	public Materials()
	{
	
	}
}
