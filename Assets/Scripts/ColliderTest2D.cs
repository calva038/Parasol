using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest2D : MonoBehaviour
{
    void OnMouseEnter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(this.gameObject.name);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(this.gameObject.name);
        }
    }
}