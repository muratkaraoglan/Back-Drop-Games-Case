using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
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
        InteractionText.Instance.SetInteractionText(GameManager.Instance.CollectibleItemInteractPrompt);
    }

    public void DisableComponents()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        enabled = false;
    }
}
