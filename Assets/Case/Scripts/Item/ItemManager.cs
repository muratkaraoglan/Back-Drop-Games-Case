using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9)]
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<ItemSO> _items;

    [SerializeField, Tooltip("Camera z offset for drop")] private float _itemSpawnZOffset = 3f;
    [SerializeField] private float _groudnCheckDistance = 5f;

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
        Ray r = _mainCamera.ScreenPointToRay(mousePosition);
        mousePosition.z = _itemSpawnZOffset;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        if (Physics.Raycast(r, out RaycastHit hitInfo, _groudnCheckDistance))
        {
            Vector3 hitPoint = hitInfo.point;
            float hitPointToRayOrigin = (r.origin - hitPoint).sqrMagnitude;
            float worldPositionToRayOrigin = (r.origin - worldPosition).sqrMagnitude;
            if (worldPositionToRayOrigin > hitPointToRayOrigin)
            {
                worldPosition = hitPoint + Vector3.up * .2f;
            }
        }

        ItemData itemData = GetItemData(itemID);

        Instantiate(itemData.ItemPrefab, worldPosition, Random.rotation).GetComponent<Item>().ItemId = itemID;
    }

}




