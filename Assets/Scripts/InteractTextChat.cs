using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTextChat : MonoBehaviour
{
    [SerializeField] private GameObject CharacterSpeaking;
    public static bool inConversation;
    private void Update()
    {
        if (Snowcone.canTalk)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inConversation = !inConversation;
            }
            if (inConversation)
            {
                ActivateMenu();
            }
            else
            {
                DeactivateMenu();
            }
        }
        else
        {
            DeactivateMenu();
        }
    }
    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        CharacterSpeaking.SetActive(true);
    }
    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        CharacterSpeaking.SetActive(false);
        inConversation = false;
    }
}
