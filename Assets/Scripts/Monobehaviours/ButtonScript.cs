using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Inventory inventorySystem;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        inventorySystem = GameObject.Find("PlayerInventory").GetComponent<Inventory>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickFunction()
    {
        inventorySystem.StoreItem(item);

    }
}
