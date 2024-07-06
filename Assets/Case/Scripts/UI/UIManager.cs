using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [field: SerializeField] public InventorySlot SlotPrefab { get; private set; }
    [Header("Chest Inventory")]
    [SerializeField] private ChestInventoryManager _chestInventoryManager;
    [SerializeField] private GameObject _chestInventoryParent;

    [Header("Player Inventory")]
    [SerializeField] private PlayerInventoryManager _playerInventoryManager;
    [SerializeField] private GameObject _playerInventoryParent;

    private void OnEnable()
    {
        InputReader.Instance.OnInventoryButtonPressed += OnInventoryButtonPressed;
    }
    private void OnDisable()
    {
        InputReader.Instance.OnInventoryButtonPressed -= OnInventoryButtonPressed;
    }

    private void OnInventoryButtonPressed()
    {
        bool isInventoryOpen = _playerInventoryParent.activeSelf;
        _playerInventoryParent.SetActive(!isInventoryOpen);
        InputReader.Instance.SetCursorState(!isInventoryOpen);
        if (isInventoryOpen)
        {
            _chestInventoryParent.SetActive(false);
            InputReader.Instance.OnPressInteractButtonEvent -= OnPressInteractButtonEvent;
        }
    }

    public void OpenChestInventory(Chest chest)
    {
        _chestInventoryParent.SetActive(true);
        _playerInventoryParent.SetActive(true);
        _chestInventoryManager.InitInventory(chest);
        InputReader.Instance.SetCursorState(true);
        InputReader.Instance.OnPressInteractButtonEvent += OnPressInteractButtonEvent;
    }

    private void OnPressInteractButtonEvent()
    {
        _chestInventoryParent.SetActive(false);
        _playerInventoryParent.SetActive(false);
        InputReader.Instance.OnPressInteractButtonEvent -= OnPressInteractButtonEvent;
        InputReader.Instance.SetCursorState(false);
    }

    public void OnItemCollect(string itemID)
    {
        _playerInventoryManager.CollectItem(itemID);
    }
}
