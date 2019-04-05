using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlipInsideOut : MonoBehaviour
{
    public GameObject hubWorld;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        hubWorld.transform.eulerAngles = new Vector3(0, LoadShop.hubRotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
