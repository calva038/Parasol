using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptDisplay : MonoBehaviour
{
    [SerializeField] private GameObject Prompt;
    [SerializeField] private bool shouldDisplay;

    private void Update()
    {
        if ((Snowcone.canTalk) & (!InteractTextChat.inConversation))
        {
            shouldDisplay = true;
        }
        else
        {
            shouldDisplay = false;
        }
        if (shouldDisplay)
        {
            ActivatePrompt();
        }
        else
        {
            DeactivatePrompt();
        }
    }
    void ActivatePrompt()
    {
        Prompt.SetActive(true);
    }
    public void DeactivatePrompt()
    {
        Prompt.SetActive(false);
    }
}
