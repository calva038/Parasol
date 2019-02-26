using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowcone : MonoBehaviour
{
    public static bool canTalk;
    
    void OnTriggerEnter2D (Collider2D collision)
    {
        canTalk = true;
    }

    void OnTriggerStay2D (Collider2D collision)
    {
        canTalk = true;
    }

    void OnTriggerExit2D (Collider2D collision)
    {
        canTalk = false;
    }
}
