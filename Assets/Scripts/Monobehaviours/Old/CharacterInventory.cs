using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    /// <summary>
    /// 库存类的实例
    /// </summary>
    public static CharacterInventory instance;

    /// <summary>
    /// 角色的统计数据
    /// </summary>
    public CharacterStats charStats;
    /// <summary>
    /// 用来寻找并存放玩家角色
    /// </summary>
    GameObject foundStats;

    /// <summary>
    /// 人物UI对象
    /// </summary>
    public GameObject PropertyDisplayHolder;
    /// <summary>
    /// 装备栏位
    /// </summary>
    public Image[] propertyDisplaySlots = new Image[15];
    /// <summary>
    /// 背包UI对象
    /// </summary>
    public GameObject InventoryDisplayHolder;
    /// <summary>
    /// 背包栏位
    /// </summary>
    public Image[] inventoryDisplaySlots = new Image[36];

    /// <summary>
    /// 物品数目上限
    /// </summary>
    int inventoryItemCap = 20;
    /// <summary>
    /// 字典序列
    /// </summary>
    int idCount = 1;
    bool addedItem = true;

    /// <summary>
    /// 背包字典对象
    /// </summary>
    public Dictionary<int, InventoryEntry> itemsInInventory = new Dictionary<int, InventoryEntry>();
    /// <summary>
    /// 临时存放的物品,换而言之，此即将添加到人物背包的物品
    /// </summary>
    public InventoryEntry itemEntry;


    void Start()
    {
        instance = this;
        itemEntry = new InventoryEntry(0, null, null);
        itemsInInventory.Clear();
        propertyDisplaySlots = PropertyDisplayHolder.GetComponentsInChildren<Image>();
        inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            DisplayProperty();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }
    }


    public void StoreItem(Item itemToStore)
    {
        addedItem = false;
       
        if ((charStats.characterDefinition.currentEncumbrance+itemToStore.itemWeight)<=charStats.characterDefinition.maxEncumbrance)
        {
            
            itemEntry.itemEntry = itemToStore;
            itemEntry.stackSize = 1;
            itemEntry.hbSprite = itemToStore.itemIcon;
        }
    }

    void TryPickUp()
    {
        bool itsInInv = true;
        //检查缓存的对象是否为空，即需要存储的对象是否就绪
        if(itemEntry.itemEntry)
        {
            //通过检查序列号，来确定背包里是否已经存在物品。-如果为零，则为不存在，即添加缓存对象到背包
            if(itemsInInventory.Count==0)
            {
                addedItem = AddItemToInventory(addedItem);
            }
             //如果不为零，则意味着背包里已经存在物品
            else
            {
                //首先检查欲添加的物品是否可以堆叠
                if(itemEntry.itemEntry.isStackable)
                {
                    foreach(KeyValuePair<int,InventoryEntry> ie in itemsInInventory)
                    {
                        //该物品是否已经存在于背包
                        if(itemEntry.itemEntry==ie.Value.itemEntry)
                        {
                            ie.Value.stackSize += 1;
                            itsInInv = true;
                            break;
                        }
                        //这个物品不存在于背包中
                        else
                        {
                            itsInInv = false;
                        }
                    }
                }
                //物品不可堆叠的情况下
                else
                {
                    itsInInv = false;
                    //
                    if(itemsInInventory.Count==inventoryItemCap)
                    {
                        Debug.Log("Inventory is Full");
                    }
                }
                //检查库存中是否有空间
                if (!itsInInv)
                {
                    addedItem = AddItemToInventory(addedItem);
                    itsInInv = true;
                }
            }

        }
    }


    bool AddItemToInventory(bool finishedAdding)
    {
        itemsInInventory.Add(idCount, new InventoryEntry(itemEntry.stackSize, Instantiate(itemEntry.itemEntry), itemEntry.hbSprite));

        FillInventoryDisplay();

        #region Reset itemEntry
        itemEntry.itemEntry = null;
        itemEntry.stackSize = 0;
        itemEntry.hbSprite = null;
        #endregion

        finishedAdding = true;

        return finishedAdding;
    }


    void FillInventoryDisplay()
    {
        int slotCounter = 9;

        foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
        {
            slotCounter += 1;
            inventoryDisplaySlots[slotCounter].sprite = ie.Value.hbSprite;
            ie.Value.inventorySlot = slotCounter - 9;
        }

        while (slotCounter < 29)
        {
            slotCounter++;
            inventoryDisplaySlots[slotCounter].sprite = null;
        }
    }

    void DisplayProperty()
    {
        if (PropertyDisplayHolder.activeSelf == true)
        {
            PropertyDisplayHolder.SetActive(false);
        }
        else
        {
            PropertyDisplayHolder.SetActive(true);
        }
    }
    void DisplayInventory()
    {
        if (InventoryDisplayHolder.activeSelf == true)
        {
            InventoryDisplayHolder.SetActive(false);
        }
        else
        {
            InventoryDisplayHolder.SetActive(true);
        }
    }

}
