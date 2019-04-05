using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowcone3D : MonoBehaviour
{
    public static bool canTalk3D;

    void OnTriggerEnter(Collider collision)
    {
        canTalk3D = true;
    }

    void OnTriggerStay(Collider collision)
    {
        canTalk3D = true;
    }

    void OnTriggerExit(Collider collision)
    {
        canTalk3D = false;
    }
}