using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerItemSlot : MonoBehaviour, IInventory
{
    [SerializeField] private Transform _itemRefPointOnPlayer;
    private InventorySlot _slot;
    public void ChangeInventory(string itemId, InventorySlot slot)
    {
        if (_slot != null)
        {
            slot.CurrentInventory.ChangeInventory(itemId, _slot);
            Destroy(_itemRefPointOnPlayer.GetChild(0).gameObject);
        }
        slot.transform.SetParent(transform, true);
        slot.transform.localPosition = Vector3.zero;
        slot.ChangeInventory(this);
        slot.SetAnimatorState(false);
        _slot = slot;

        ItemData item = ItemManager.Instance.GetItemData(itemId);
        GameObject itemGO = Instantiate(item.ItemPrefab, _itemRefPointOnPlayer);
        itemGO.GetComponent<Item>().DisableComponents();
    }

    public void OnDragEnd(PointerEventData pointerEvent, string itemID, InventorySlot slot)
    {
        if (pointerEvent.pointerEnter == null)//drop to ground
        {
            _slot = null;
            Destroy(slot.gameObject);
            Destroy(_itemRefPointOnPlayer.GetChild(0).gameObject);
            ItemManager.Instance.DropItem(itemID, Input.mousePosition);
            return;
        }
        pointerEvent.pointerEnter.TryGetComponent(out IInventory inventory);
        if (inventory != null && inventory != (IInventory)this)
        {
            inventory.ChangeInventory(itemID, slot);
            _slot = null;
            Destroy(_itemRefPointOnPlayer.GetChild(0).gameObject);
            return;
        }
        slot.transform.SetParent(transform, true);
        slot.transform.localPosition = Vector3.zero;

    }

    public void RemoveFromInventory(string id)
    {

    }
}
