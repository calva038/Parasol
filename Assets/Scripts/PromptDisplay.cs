using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptDisplay : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePrompt;
    [SerializeField] private GameObject ShopPrompt;
    [SerializeField] private bool shouldDisplayShop;
    [SerializeField] private bool shouldDisplayDialogue;

    private void Update()
    {
        if ((Snowcone.canTalk | Snowcone3D.canTalk3D) & (!InteractTextChat.inConversation))
        {
            shouldDisplayDialogue = true;
        }
        else
        {
            shouldDisplayDialogue = false;
        }
        if (EnterShop.shopDoor)
        {
            shouldDisplayShop = true;
        }
        else
        {
            shouldDisplayShop = false;
        }
        if (shouldDisplayDialogue)
        {
            ActivateDialoguePrompt();
        }
        else
        {
            DeactivateDialoguePrompt();
        }
        if (shouldDisplayShop)
        {
            ActivateShopPrompt();
        }
        else
        {
            DeactivateShopPrompt();
        }
    }
    void ActivateDialoguePrompt()
    {
        DialoguePrompt.SetActive(true);
    }
    public void DeactivateDialoguePrompt()
    {
        DialoguePrompt.SetActive(false);
    }
    void ActivateShopPrompt()
    {
        ShopPrompt.SetActive(true);
    }
    public void DeactivateShopPrompt()
    {
        ShopPrompt.SetActive(false);
    }
}
