using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    /// <summary>
    /// 库存类的实例
    /// </summary>
    public static InventorySystem instance;

    /// <summary>
    /// 角色的统计数据
    /// </summary>
    public CharacterStats charStats;
    /// <summary>
    /// 用来寻找并存放玩家角色
    /// </summary>
    GameObject foundStats;

    public GameObject slot;

    /// <summary>
    /// 背包UI对象
    /// </summary>
    public GameObject InventoryDisplayHolder;
    /// <summary>
    /// 背包栏位
    /// </summary>
    public Image[] inventoryDisplaySlots;

    public int baseSlotCount = 36;

    /// <summary>
    /// 可用的背包栏位数目
    /// </summary>
    int inventoryItemCap = 36;
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
    public InventoryEntry inventoryEntry;

    void Start()
    {
        instance = this;

        inventoryEntry = new InventoryEntry(0, null, null);

        itemsInInventory.Clear();

        inventoryDisplaySlots = new Image[baseSlotCount];
        AddSlot(baseSlotCount);
        //inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }

        if (addedItem == false)
        {
            TryPickUp();
        }
    }


    public void StoreItem(Item itemToStore)
    {
        addedItem = false;
        if ((charStats.characterDefinition.currentEncumbrance + itemToStore.itemWeight) <= charStats.characterDefinition.maxEncumbrance)
        {
            
            inventoryEntry.itemEntry = itemToStore;
            inventoryEntry.stackSize = 1;
            inventoryEntry.hbSprite = itemToStore.itemIcon;
            Debug.Log("[CharacterInventory]StoreItem is Over!");
        }
        FillInventoryDisplayText();
    }

    void TryPickUp()
    {
        bool itsInInv = true;
        //检查缓存的对象是否为空，即需要存储的对象是否就绪
        if (inventoryEntry.itemEntry)
        {
            //通过检查序列号，来确定背包里是否已经存在物品(任何物品都会占用)。-如果为零，则为不存在，即添加缓存对象到背包
            if (itemsInInventory.Count == 0)
            {
                addedItem = AddItemToInventory(addedItem);
            }
            //如果不为零，则意味着背包里已经存在物品
            else
            {
                Debug.Log("[InventorySystem]TryPickUp:"+inventoryEntry.itemEntry.isStackable);
                //首先检查欲添加的物品是否可以堆叠
                if (inventoryEntry.itemEntry.isStackable)
                {

                    foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
                    {
                        //该物品是否已经存在于背包
                        if (inventoryEntry.itemEntry == ie.Value.itemEntry)
                        {
                            Debug.Log("[InventorySystem]TryPickUp:Stackable is OK");
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
                    if (itemsInInventory.Count == inventoryItemCap)
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
        itemsInInventory.Add(idCount, new InventoryEntry(inventoryEntry.stackSize, inventoryEntry.itemEntry, inventoryEntry.hbSprite));

        FillInventoryDisplay();
        idCount = IncreaseID(idCount);

        #region Reset itemEntry
        inventoryEntry.itemEntry = null;
        inventoryEntry.stackSize = 0;
        inventoryEntry.hbSprite = null;
        #endregion

        finishedAdding = true;
        Debug.Log("[InventorySystem]AddItemToInventory:OK");
        return finishedAdding;
    }


    void FillInventoryDisplay()
    {
        int slotCounter = 0;

        foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
        {
            slotCounter += 1;
            inventoryDisplaySlots[slotCounter].sprite = ie.Value.hbSprite;
            inventoryDisplaySlots[slotCounter].GetComponentInChildren<Text>().text = ie.Value.stackSize.ToString();
            
            ie.Value.inventorySlot = slotCounter ;
        }

        while (slotCounter < 35)
        {
            slotCounter++;
            inventoryDisplaySlots[slotCounter].sprite = null;
        }
    }


    void FillInventoryDisplayText()
    {
        foreach (KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
        {
            inventoryDisplaySlots[ie.Key-1].GetComponentInChildren<Text>().text=ie.Value.stackSize.ToString();
            
        }
    }
    int IncreaseID(int currentID)
    {
        int newID = 1;

        for (int itemCount = 1; itemCount <= itemsInInventory.Count; itemCount++)
        {
            if (itemsInInventory.ContainsKey(newID))
            {
                newID += 1;
            }
            else return newID;
        }

        return newID;
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

    void AddSlot(int slotSize)
    {
        for(int i=0;i<slotSize;i++)
        {
            GameObject gameObject = Instantiate(slot);
            gameObject.transform.SetParent(InventoryDisplayHolder.transform);
            inventoryDisplaySlots[i] = gameObject.GetComponent<Image>();
        }
    }
}
