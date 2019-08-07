using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformation : MonoBehaviour
{
    public GameObject itemImage;
    public Text itemName;
    public Text description;
    public Text information;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void InformationDisplay(string na, string desc, string info)
    {

        itemName.text = na;
        description.text = desc;
        information.text = info;

        itemImage.SetActive(true);

    }

    public void InformationConceal()
    {

        itemImage.SetActive(false);
        itemName.text = "";
        description.text = "";
        information.text = "";
    }
}
