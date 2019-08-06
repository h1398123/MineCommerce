using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour, IPointerClickHandler
{
    public BuildData BuildDefinition;
    public List<Item> items = new List<Item>();
    public List<InventoryEntry> shop = new List<InventoryEntry>();

    public UnityEvent leftClick;

    public Inventory shopInventory;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            leftClick.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        leftClick.AddListener(new UnityAction(OpenShopPack));
        shopInventory = GameObject.Find("ShopInventory").GetComponent<Inventory>();
        foreach (Item im in items)
        {
            StoreItem(im,Random.Range(1,10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StoreItem(Item im,int icout)
    {
        if(shop.Count==0)
        {
            shop.Add(new InventoryEntry(1, im, im.itemIcon));
        }
    }
    

    private void OpenShopPack()
    {
        shopInventory.sourceInventory = shop;
        shopInventory.RefreshInventoryDisplay();

        DisplayInventory();
    }

    void DisplayInventory()
    {
        if (shopInventory.InventoryDisplayHolder.activeSelf == true)
        {
            
        }
        else
        {
            shopInventory.InventoryDisplayHolder.SetActive(true);
        }

        
    }

}


