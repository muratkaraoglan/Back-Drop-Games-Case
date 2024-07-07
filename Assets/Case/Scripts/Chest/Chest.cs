using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private List<string> _itemIds;
    bool _isTextOn = false;

    public void SetItems(List<string> itemIDs)
    {
        _itemIds = itemIDs;
    }

    public List<string> GetItemIds() => _itemIds;

    public void Interact()
    {
        UIManager.Instance.OpenChestInventory(this);
    }

    public void PromptTextOn()
    {
        if (_isTextOn) return;
        _isTextOn = true;
        InteractionText.Instance.SetInteractionText(GameManager.Instance.ChestInteractPrompt);
    }

    public void PrompTextOff()
    {
        _isTextOn = false;
        InteractionText.Instance.ClearInteractionText();
    }
}
