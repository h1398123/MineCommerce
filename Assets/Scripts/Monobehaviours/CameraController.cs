using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject[] players;
    public  Vector3 CameraToPlayer;

    // Start is called before the first frame update
    void Start()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        CameraToPlayer = new Vector3(0,20,-15);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = players[0].transform.position + CameraToPlayer;
    }
}
