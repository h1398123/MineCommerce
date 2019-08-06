using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEntry
{
    public Item itemEntry;//物品的实例
    public int stackSize;//堆叠大小
    public int inventorySlot;//背包栏位的数量
    public int hotBarSlot;//快捷栏位的数量
    public Sprite hbSprite;//在栏位上显示的图标

    public InventoryEntry(int stackSize,Item itemEntry,Sprite hbSprite)
    {
        this.itemEntry = itemEntry;
        this.stackSize = stackSize;
        this.hotBarSlot = 0;
        this.inventorySlot = 0;
        this.hbSprite = hbSprite;
    }

}
