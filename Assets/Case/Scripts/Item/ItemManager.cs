using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9)]
public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<ItemSO> _items;
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
}




