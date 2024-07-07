using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestInventoryManager : MonoBehaviour, IInventory
{
    [SerializeField] private Transform _inventorySlotParent;
    [SerializeField] private float _nextItemSpawnDuration = .2f;
    private List<string> _itemIds;
    private Chest _currentChest;

    public void InitInventory(Chest chest)
    {
        _inventorySlotParent.DestroyAllChildren();
        _currentChest = chest;
        _itemIds = _currentChest.GetItemIds();
        StartCoroutine(InstantiateAnimation());
    }

    IEnumerator InstantiateAnimation()
    {
        int i = 0;
        while (i < _itemIds.Count)
        {
            Instantiate(UIManager.Instance.SlotPrefab, _inventorySlotParent).Initialize(this, _itemIds[i]);
            yield return Utils.GetWaitForSeconds(_nextItemSpawnDuration);
            i++;
        }
    }

    public void ChangeInventory(string itemId, InventorySlot slot)
    {
        _itemIds.Add(itemId);
        slot.ChangeInventory(this);
        slot.transform.SetParent(_inventorySlotParent, true);
    }

    public void OnDragEnd(PointerEventData pointerEvent, string itemID, InventorySlot slot)
    {
        if (pointerEvent.pointerEnter == null)
        {
            _itemIds.Add(itemID);
            slot.transform.SetParent(_inventorySlotParent, true);
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

    private void OnDisable()
    {
        _currentChest.SetItems(_itemIds);
    }
}
