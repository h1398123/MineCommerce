using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    /// <summary>
    /// 角色的统计数据
    /// </summary>
    public CharacterStats charStats;

    /// <summary>
    /// 背包单个栏位UI预设体
    /// </summary>
    public GameObject slot;
    /// <summary>
    /// 背包所有栏位的UI集合
    /// </summary>
    public List<Image> inventoryDisplaySlots;
    /// <summary>
    /// 背包UI栏的主窗体对象
    /// </summary>
    public GameObject InventoryDisplayHolder;
    /// <summary>
    /// 背包的存放类
    /// </summary>
    public List<InventoryEntry> itemsInInventory = new List<InventoryEntry>();

    public InventoryEntry inventoryEntry;

    /// <summary>
    /// 当前可用的背包栏位数
    /// </summary>
    public int SlotCount = 36;


    /// <summary>
    /// 背包栏位索引
    /// </summary>
    int idCount = 1;

    bool addedItem = true;









    // Start is called before the first frame update
    void Start()
    {
        inventoryEntry = new InventoryEntry(0, null, null);

        itemsInInventory.Clear();


        AddSlot(SlotCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }

        RefreshInventoryDisplay();
    }

    public void StoreItem(Item item)
    {
        
        if((charStats.characterDefinition.currentEncumbrance+item.itemWeight)<=charStats.characterDefinition.maxEncumbrance)
        {
            inventoryEntry.itemEntry = item;
            inventoryEntry.stackSize = 1;
            inventoryEntry.hbSprite = item.itemIcon;
        }



        Debug.Log("111");
        TryPickUp();
    }


    void TryPickUp()
    {
        bool itsInInv = true;
        if (inventoryEntry.itemEntry==null)
        {
            Debug.Log("inventoryEntry is null!");
        }
        Debug.Log("112");
        if(itemsInInventory.Count==0)
        {
            itemsInInventory.Add(new InventoryEntry(inventoryEntry.stackSize, inventoryEntry.itemEntry, inventoryEntry.hbSprite));
        }
        else
        {
            //判断物品是否属于可堆叠物品
            if (inventoryEntry.itemEntry.isStackable)
            {
                //遍历背包类
                foreach (InventoryEntry ie in itemsInInventory)
                {
                    bool ias = (inventoryEntry.itemEntry == ie.itemEntry);
                    Debug.Log(ias);
                    //该物品是否已经存在于背包
                    if (inventoryEntry.itemEntry == ie.itemEntry)
                    {
                        bool iac = (ie.stackSize + inventoryEntry.stackSize <= charStats.characterDefinition.maxStack);
                        Debug.Log(iac);
                        if (ie.stackSize+ inventoryEntry.stackSize<= charStats.characterDefinition.maxStack)
                        {
                            ie.stackSize += 1;
                            itsInInv = true;
                            break;
                        }
                        else
                        {
                            itsInInv = false;
                        }
                        
                    }
                    //背包中没有该物品
                    else
                    {
                        itsInInv = false;
                    }
                }
            }
            else
            {
                itsInInv = false;
            }
            //检查库存中是否有空间
            if (!itsInInv)
            {
                if(itemsInInventory.Count>=SlotCount)
                {
                    Debug.Log("Inventory is Full!");
                }
                else
                {
                    addedItem = AddItemToInventory(addedItem);
                }

                
                itsInInv = true;
            }


        }
        

        Debug.Log(itemsInInventory.Count);
    }

    bool AddItemToInventory(bool finishedAdding)
    {
        itemsInInventory.Add(new InventoryEntry(inventoryEntry.stackSize, inventoryEntry.itemEntry, inventoryEntry.hbSprite));

        #region Reset itemEntry
        inventoryEntry.itemEntry = null;
        inventoryEntry.stackSize = 0;
        inventoryEntry.hbSprite = null;
        #endregion

        finishedAdding = true;
        return finishedAdding;
    }


    void RefreshInventoryDisplay()
    {

        for (int i = 0; i < itemsInInventory.Count; i++)
        {

            inventoryDisplaySlots[i].sprite = itemsInInventory[i].hbSprite;
            inventoryDisplaySlots[i].GetComponentInChildren<Text>().text = itemsInInventory[i].stackSize.ToString();
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

    void AddSlot(int slotSize)
    {
        for (int i = 0; i < slotSize; i++)
        {
            GameObject gameObject1 = Instantiate(slot);
            gameObject1.transform.SetParent(InventoryDisplayHolder.transform);
            inventoryDisplaySlots.Add(gameObject1.GetComponent<Image>());

            
        }
    }

   
}
