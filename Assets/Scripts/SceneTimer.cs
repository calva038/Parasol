using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTimer : MonoBehaviour
{
    void Start()
    {

        StartCoroutine(loadSceneAfterDelay(10));

    }

    IEnumerator loadSceneAfterDelay(float waitbySecs)
    {

        yield return new WaitForSeconds(waitbySecs);
        Application.LoadLevel("Menu");
    }
}