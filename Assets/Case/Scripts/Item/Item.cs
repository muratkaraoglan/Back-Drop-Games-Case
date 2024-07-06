using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string _promp;
    public string InteractionPrompt => _promp;
    public string ItemId;

    public void Interact()
    {
        UIManager.Instance.OnItemCollect(ItemId);
        Destroy(gameObject);
    }

    public void PrompTextOff()
    {
        InteractionText.Instance.SetInteractionText("");
    }

    public void PromptTextOn()
    {
        InteractionText.Instance.SetInteractionText(InteractionPrompt);
    }
}
