using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{



    /// <summary>
    /// 背包单个栏位UI预设体
    /// </summary>
    public GameObject slot;
    /// <summary>
    /// 商店所有栏位的UI集合
    /// </summary>
    public List<Image> shopDisplaySlots;
    /// <summary>
    /// 商店UI栏的主窗体对象
    /// </summary>
    public GameObject ShopDisplayHolder;
    /// <summary>
    /// 商店的存放类
    /// </summary>
    public List<InventoryEntry> itemsInShop = null;
    /// <summary>
    /// 当前可用的商店栏位数
    /// </summary>
    public int shopSlotCount = 36;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetShopInventory(List<InventoryEntry> inventories)
    {
        itemsInShop = inventories;
    }
    void RefreshInventoryDisplay()
    {
        if(itemsInShop!=null)
        {
            for (int i = 0; i < itemsInShop.Count; i++)
            {

                shopDisplaySlots[i].sprite = itemsInShop[i].hbSprite;
                shopDisplaySlots[i].GetComponentInChildren<Text>().text = itemsInShop[i].stackSize.ToString();
            }
        }
        

    }

    void DisplayInventory()
    {
        if (ShopDisplayHolder.activeSelf == true)
        {
            ShopDisplayHolder.SetActive(false);
        }
        else
        {
            ShopDisplayHolder.SetActive(true);
        }
    }

    void AddSlot(int slotSize)
    {
        GameObject gameObject2 = Instantiate(slot);
        gameObject2.transform.SetParent(ShopDisplayHolder.transform);
        shopDisplaySlots.Add(gameObject2.GetComponent<Image>());
    }
}
