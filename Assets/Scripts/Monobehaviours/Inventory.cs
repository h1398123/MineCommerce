using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
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
    public List<InventoryEntry> sourceInventory = new List<InventoryEntry>();
    /// <summary>
    /// 待处理的缓存物品
    /// </summary>
    public InventoryEntry targetItem;


    //public GameObject targetDisplayHolder;

    public Inventory targetInventory;
    /// <summary>
    /// 目标库存类
    /// </summary>
    //public List<InventoryEntry> targetInventory = null;


    /// <summary>
    /// 当前可用的背包栏位数
    /// </summary>
    public int SlotCount = 36;


    /// <summary>
    /// 背包栏位索引
    /// </summary>
    int idCount = 1;




    bool isRefresh = false;

    // Start is called before the first frame update
    void Start()
    {
        targetItem = new InventoryEntry(0, null, null);

        sourceInventory.Clear();

        

        AddSlot(SlotCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DisplayInventory();
        }

        if (isRefresh)
        {
            RefreshInventoryDisplay();
            isRefresh = false;
        }
    }

    public void SetSource(Inventory setSour)
    {
        //sourceInventory = setSour;
    }

    public void Sell(Image image)
    {
        
        if (targetInventory.sourceInventory == null)
        {
            Debug.Log("2");
            return;
        }
        foreach (InventoryEntry inve in sourceInventory)
        {
            if (image.sprite==inve.hbSprite)
            {
                DiscardItem(inve);
                targetInventory.StoreItem(inve);
                break;
            }
        }
    }

    public void Sell(Image image,int num)
    {
        for(int i=1; i<=num;i++)
        {
            if (targetInventory.sourceInventory == null)
            {

                return;
            }
            foreach (InventoryEntry inve in sourceInventory)
            {
                if (image.sprite == inve.hbSprite)
                {
                    DiscardItem(inve, num);
                    targetInventory.StoreItem(inve);
                    break;
                }



            }
        }
        


    }

    void Buy()
    {

    }

    public void StoreItem(Item item,int stackCount=1)
    {
        if ((charStats.characterDefinition.currentEncumbrance + item.itemWeight) <= charStats.characterDefinition.maxEncumbrance)
        {
            targetItem.itemEntry = item;
            targetItem.stackSize = stackCount;
            targetItem.hbSprite = item.itemIcon;
        }

        TryPickUp();
    }

    public void StoreItem(InventoryEntry item, int stackCount = 1)
    {
        if ((charStats.characterDefinition.currentEncumbrance + item.itemEntry.itemWeight) <= charStats.characterDefinition.maxEncumbrance)
        {
            targetItem.itemEntry = item.itemEntry;
            targetItem.stackSize = stackCount;
            targetItem.hbSprite = item.hbSprite;
        }

        TryPickUp();
    }
    public void DiscardItem(Item item, int stackCount = 1)
    {
        targetItem.itemEntry = item;
        targetItem.stackSize = stackCount;
        targetItem.hbSprite = item.itemIcon;

        TryThrowOut();
    }
    public void DiscardItem(InventoryEntry item, int stackCount = 1)
    {
        targetItem.itemEntry = item.itemEntry;
        targetItem.stackSize = stackCount;
        targetItem.hbSprite = item.hbSprite;

        TryThrowOut();
    }

    void TryPickUp()
    {
        bool itsInInv = true;
        if (targetItem.itemEntry == null)
        {
            Debug.Log("targetItem is null!");
        }
        if (sourceInventory.Count == 0)
        {
            sourceInventory.Add(new InventoryEntry(targetItem.stackSize, targetItem.itemEntry, targetItem.hbSprite));
        }
        else
        {
            if (targetItem.itemEntry.isStackable)
            {
                //遍历背包类
                foreach (InventoryEntry ie in sourceInventory)
                {
                    //该物品是否已经存在于背包
                    if (targetItem.itemEntry == ie.itemEntry)
                    {
                        bool iac = (ie.stackSize + targetItem.stackSize <= charStats.characterDefinition.maxStack);
                        if (ie.stackSize + targetItem.stackSize <= charStats.characterDefinition.maxStack)
                        {
                            ie.stackSize += targetItem.stackSize;
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
                if (sourceInventory.Count >= SlotCount)
                {
                    Debug.Log("Inventory is Full!");
                }
                else
                {
                    AddItemToInventory();
                }


                itsInInv = true;
            }


        }


        isRefresh = true;
        Debug.Log(sourceInventory.Count);
    }

    void TryThrowOut()
    {
        int count = 0;
        if (targetItem.itemEntry == null)
        {
            Debug.Log("targetItem is null!");
        }

        if (sourceInventory.Count == 0)
        {
            Debug.Log("the backpack is Empty");
        }
        else
        {
            foreach (InventoryEntry ie in sourceInventory)
            {
                if (targetItem.itemEntry == ie.itemEntry)
                {
                    if (ie.stackSize == 1)
                    {
                        count = sourceInventory.IndexOf(ie);
                        RemoveItemInInventory(count);
                        break;
                    }
                    else
                    {
                        ie.stackSize -= 1;
                        isRefresh = true;
                        break;
                    }

                }
                //背包中没有该物品
                else
                {
                    Debug.Log("There are not the item");
                }
            }

        }
    }

    void AddItemToInventory()
    {
        sourceInventory.Add(new InventoryEntry(targetItem.stackSize, targetItem.itemEntry, targetItem.hbSprite));

        #region Reset itemEntry
        targetItem.itemEntry = null;
        targetItem.stackSize = 0;
        targetItem.hbSprite = null;
        #endregion

    }


    void RemoveItemInInventory(int count)
    {
        
        inventoryDisplaySlots[count].sprite = null;
        inventoryDisplaySlots[count].GetComponentInChildren<Text>().text = " ";
        sourceInventory.RemoveAt(count);
        

        isRefresh = true;

        #region Reset itemEntry
        targetItem.itemEntry = null;
        targetItem.stackSize = 0;
        targetItem.hbSprite = null;
        #endregion
    }

    public void RefreshInventoryDisplay()
    {
        
        foreach(Image id in inventoryDisplaySlots)
        {
            id.sprite = null;
            id.GetComponentInChildren<Text>().text = "";
        }


        for (int i = 0; i < sourceInventory.Count; i++)
        {
            inventoryDisplaySlots[i].sprite = sourceInventory[i].hbSprite;
            inventoryDisplaySlots[i].GetComponentInChildren<Text>().text = sourceInventory[i].stackSize.ToString();
        }
    }


    public void AddSlot()
    {
        GameObject slotObject = Instantiate(slot);
        slotObject.GetComponent<InventorySlotScript>().inventorySystem = this;
        slotObject.transform.SetParent(InventoryDisplayHolder.transform);
        inventoryDisplaySlots.Add(slotObject.GetComponent<Image>());
    }

    void AddSlot(int slotSize)
    {
        for (int i = 0; i < slotSize; i++)
        {
            AddSlot();
        }
    }

    public void RemoveSlot()
    {


        Destroy(inventoryDisplaySlots[inventoryDisplaySlots.Count - 1].GetComponentInChildren<Text>());
        Destroy(inventoryDisplaySlots[inventoryDisplaySlots.Count - 1]);

        inventoryDisplaySlots.RemoveRange(inventoryDisplaySlots.Count - 1, 1);
    }
    void RemoveSlot(int slotSize)
    {
        for (int i = 0; i < slotSize; i++)
        {
            RemoveSlot();
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

    public void Clear()
    {
        sourceInventory = null;
        
    }








}
