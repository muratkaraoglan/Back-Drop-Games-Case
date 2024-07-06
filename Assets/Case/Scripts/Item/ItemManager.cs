using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9)]
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<ItemSO> _items;

    [SerializeField, Tooltip("Camera z offset for drop")] private float _itemSpawnZOffset = 3;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    public List<string> CreateRandomNewItems(int itemCount)
    {
        List<string> items = new List<string>();
        for (int i = 0; i < itemCount; i++)
        {
            string itemID = _items[Random.Range(0, _items.Count)].ItemID;
            items.Add(itemID);
        }
        return items;
    }

    public ItemData GetItemData(string itemID)
    {
        return _items.Find(i => i.ItemID == itemID).ItemData;
    }


    public void DropItem(string itemID, Vector3 mousePosition)
    {
        mousePosition.z = _itemSpawnZOffset;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        ItemData itemData = GetItemData(itemID);

        Instantiate(itemData.ItemPrefab, worldPosition, Random.rotation).GetComponent<Item>().ItemId = itemID;
    }
}




