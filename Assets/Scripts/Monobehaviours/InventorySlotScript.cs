using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;

    public Inventory inventorySystem;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonLeftClick()
    {
        Debug.Log("[InvetorySlotScript]you are reading item information !");
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            Debug.Log("[InvetorySlotScript:OnMouseEnter]you are reading item information !");
            inventorySystem.DisplayInformation(gameObject.GetComponent<Image>());

        }
    }

    private void ButtonMiddleClick()
    {
        Debug.Log("[InvetorySlotScript]Button Middle Click");
    }

    private void ButtonRightClick()
    {
        Debug.Log("[InvetorySlotScript]you are selling item!");

        if(GetComponent<Image>().sprite==null)
        {
            return;
        }
        
        //Debug.Log(int.Parse(GetComponentInChildren<Text>().ToString()).ToString());
        if(Input.GetKey(KeyCode.LeftShift))
        {

            int ciys = int.Parse(GetComponentInChildren<Text>().text.ToString());
            Debug.Log("左SHIFT：" + Input.GetKeyDown(KeyCode.LeftShift)+" "+ ciys);

            inventorySystem.Sell(GetComponent<Image>(), ciys); 
        }
        else
        {
            inventorySystem.Sell(GetComponent<Image>());
        }
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("[InvetorySlotScript]you are reading item information !");
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            Debug.Log("[InvetorySlotScript:OnMouseEnter]you are reading item information !");
            inventorySystem.DisplayInformation(gameObject.GetComponent<Image>());

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventorySystem.ConcealInformation();
    }
}
