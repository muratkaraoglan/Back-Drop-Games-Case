using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventory
{
    public void OnDragEnd(PointerEventData pointerEvent, string itemID, InventorySlot slot);
    public void RemoveFromInventory(string id);
    public void ChangeInventory(string itemId, InventorySlot slot);
}
