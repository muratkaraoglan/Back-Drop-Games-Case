using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerItemSlot : MonoBehaviour, IInventory
{
    [SerializeField] private Transform _itemRefPointOnPlayer;

    public void ChangeInventory(string itemId, InventorySlot slot)
    {

    }

    public void OnDragEnd(PointerEventData pointerEvent, string itemID, InventorySlot slot)
    {

    }

    public void RemoveFromInventory(string id)
    {
     
    }
}
