using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    public static bool shopDoor;
    public int shopNumber;

    void OnTriggerEnter(Collider collision)
    {
        shopDoor = true;
    }

    void OnTriggerStay(Collider collision)
    {
        shopDoor = true;
    }

    void OnTriggerExit(Collider collision)
    {
        shopDoor = false;
    }
}