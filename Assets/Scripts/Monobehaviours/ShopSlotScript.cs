using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlotScript : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;

    public PlayerInventory inventorySystem;
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        middleClick.AddListener(new UnityAction(ButtonMiddleClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));

        inventorySystem = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonLeftClick()
    {
        Debug.Log("Button Left Click");
    }

    private void ButtonMiddleClick()
    {
        Debug.Log("Button Middle Click");
    }

    private void ButtonRightClick()
    {
        Debug.Log("你正在尝试卖掉该物品！");

        if(GetComponent<Image>().sprite==null)
        {
            return;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            int asa=int.Parse(GetComponentInChildren<Text>().ToString());
            inventorySystem.StoreItem(item);
        }
        else
        {
            inventorySystem.StoreItem(item);
        }
        
    }
}
