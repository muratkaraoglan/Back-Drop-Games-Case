using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string ItemID { get; private set; }
    public ItemData ItemData;

    ItemSO()
    {
        ItemID = Guid.NewGuid().ToString();
    }
}

[System.Serializable]
public class ItemData
{
    public string ItemName;
    public Sprite ItemIcon;
    public GameObject ItemPrefab;
}
