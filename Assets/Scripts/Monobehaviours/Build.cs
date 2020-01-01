using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Build : MonoBehaviour, IPointerClickHandler
{
    public Builds BuildDefinition;
    public List<Item> items = new List<Item>();


    public Inventory shopInventory;

    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    /// <summary>
    /// 背包的存放类
    /// </summary>
    public List<InventoryEntry> sourceInventory = new List<InventoryEntry>();

    /// <summary>
    /// 待处理的缓存物品
    /// </summary>
    public InventoryEntry targetItem;

    /// <summary>
    /// 当前可用的背包栏位数
    /// </summary>
    public int maxSlotCount = 36;

    /// <summary>
    /// 
    /// </summary>
    public int maxStackSize = 100;

    float lastTime;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        maxStackSize = BuildDefinition.maxStackSize;
        maxSlotCount = BuildDefinition.maxStackSlot;
        targetItem = new InventoryEntry(0, null, null);

        sourceInventory.Clear();
        shopInventory = GameObject.Find("ShopInventoryDisplayManager").GetComponent<Inventory>();
        Debug.Log("[Build]items has item " + items.Count);

        lastTime = Time.time;

        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        //middleClick.AddListener(new UnityAction(ButtonMiddleClick));
        //rightClick.AddListener(new UnityAction(ButtonRightClick));
    }

    // Update is called once per frame
    void Update()
    {
        curTime = Time.time;
        if (curTime - lastTime >= 10)
        {
            Debug.Log("Timer is working!");
            lastTime = curTime;
            TimeOutput();
        }
    }

    public void TimeOutput()
    {
        foreach (Item im in items)
        {
            StoreItem(im, Random.Range(1, 3));
        }
    }




    private void OpenShopPage()
    {
        if (shopInventory.InventoryDisplayHolder.activeSelf == false)
        {
            shopInventory.DisplayInventory();
        }
        shopInventory.sourceInventory = sourceInventory;
        shopInventory.RefreshInventoryDisplay();

    }


    private void CloseShopPage()
    {
        if (shopInventory.InventoryDisplayHolder.activeSelf == true)
        {
            shopInventory.DisplayInventory();
        }
        shopInventory.sourceInventory = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Open the ShopPage");
        if (other.gameObject.tag == "Player")
        {
            OpenShopPage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CloseShopPage();
        }
    }


    public void StoreItem(Item item, int stackCount = 1)
    {
        if (item != null)
        {
            targetItem.itemEntry = item;
            targetItem.stackSize = stackCount;
            targetItem.hbSprite = item.itemIcon;
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

    protected void TryPickUp()
    {
        for (int i = 0; i < targetItem.stackSize; i++)
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

                            if (ie.stackSize + targetItem.stackSize <= BuildDefinition.maxStackSize)
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
                    if (sourceInventory.Count >= maxSlotCount)
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
        }
    }

    protected void TryThrowOut()
    {
        for (int i = 0; i < targetItem.stackSize; i++)
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
        sourceInventory.RemoveAt(count);

        #region Reset itemEntry
        targetItem.itemEntry = null;
        targetItem.stackSize = 0;
        targetItem.hbSprite = null;
        #endregion
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }

    private void ButtonLeftClick()
    {
        Debug.Log("[Build:ButtonLeftClick]the builds is buttonleftclick!");
    }
}


