using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrbit : MonoBehaviour
{
    Vector3 testLilyLocation = new Vector3(3.2f, -6.02f, -0.26f);
    void Update()
    {
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(testLilyLocation, Vector3.back, 100 * Time.deltaTime);
    }
}
