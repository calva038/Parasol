using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private GameObject PlayerObject;
    [SerializeField] private GameObject AudioSource;
    [SerializeField] private GameObject TextObject;
    [SerializeField] private GameObject TextObject2;
    private bool hasStarted = false;

    private void Start()
    {
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasStarted == false)
        {
            AudioSource.SetActive(true);
            hasStarted = true;
            Invoke("ExecuteAfterTime", 9);//this will happen after 2 seconds
        }
    }

    void ExecuteAfterTime()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerObject.GetComponent<PlayerMovementController>().isPaused = true;
        PlayerObject.GetComponent<PlayerAnimation>().isPaused = true;
        TextObject.SetActive(true);
        Invoke("ExecuteAfterTime2", 1.5f);//this will happen after 2 seconds
    }

    void ExecuteAfterTime2()
    {
        TextObject2.SetActive(true);
    }
}
