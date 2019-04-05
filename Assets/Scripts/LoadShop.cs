using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadShop : MonoBehaviour
{
    private int shopToLoad;
    private string sceneName;
    public GameObject hubWorld;
    public static float hubRotation;
    private bool isHub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnterShop.shopDoor)
        {
            shopToLoad = GameObject.Find("shop").GetComponent<EnterShop>().shopNumber;
            if (shopToLoad == 0)
            {
                sceneName = "Hub_World";
            }
            if (shopToLoad == 1)
            {
                sceneName = "Shop_Interior";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                isHub = GameObject.Find("Player").GetComponent<PlayerMovementController>().isHub;
                if (isHub)
                {
                    hubRotation = hubWorld.transform.eulerAngles.y;
                }
                EnterShop.shopDoor = false;
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
