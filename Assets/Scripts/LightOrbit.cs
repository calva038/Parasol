using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrbit : MonoBehaviour
{
    Vector3 testLilyLocation;
    Vector3 testLilyLocationOld;
    Vector3 offset;

    void Update()
    {
        testLilyLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(testLilyLocation, Vector3.back, 100 * Time.deltaTime);
    }
}
