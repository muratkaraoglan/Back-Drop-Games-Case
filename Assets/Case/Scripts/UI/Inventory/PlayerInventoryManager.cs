using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventoryManager : MonoBehaviour, IInventory
{

    [SerializeField] private Transform _inventorySlotParent;
    List<string> _itemIds = new List<string>();

    public void ChangeInventory(string itemId, InventorySlot slot)
    {
        _itemIds.Add(itemId);
        slot.SetAnimatorState(false);
        slot.ChangeInventory(this);
        slot.transform.SetParent(_inventorySlotParent, true);
    }

    public void OnDragEnd(PointerEventData pointerEvent, string itemID, InventorySlot slot)
    {
        if (pointerEvent.pointerEnter == null)
        {
            Destroy(slot.gameObject);
            ItemManager.Instance.DropItem(itemID, Input.mousePosition);
            return;
        }
        pointerEvent.pointerEnter.TryGetComponent(out IInventory inventory);
        if (inventory != null)
        {
            inventory.ChangeInventory(itemID, slot);
        }
    }

    public void RemoveFromInventory(string id)
    {
        _itemIds.Remove(id);
    }

    public void CollectItem(string itemID)
    {
        ItemData itemData = ItemManager.Instance.GetItemData(itemID);

        if (itemData != null)
        {
            InventorySlot slot = Instantiate(UIManager.Instance.SlotPrefab, _inventorySlotParent);
            slot.SetAnimatorState(false);
            slot.Initialize(this, itemID);
        }
    }
}
