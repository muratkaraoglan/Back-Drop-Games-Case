using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private ChestInventoryManager _chestInventoryManager;
    [SerializeField] private GameObject _chestInventoryParent;

    [SerializeField] private GameObject _playerInventoryParent;
    public void OpenChestInventory(Chest chest)
    {
        _chestInventoryParent.SetActive(true);
        _playerInventoryParent.SetActive(true);
        _chestInventoryManager.InitInventory(chest);
        InputReader.Instance.SetCursorState(false);
        InputReader.Instance.OnPressInteractButtonEvent += OnPressInteractButtonEvent;
    }

    private void OnPressInteractButtonEvent()
    {
        _chestInventoryParent.SetActive(false);
        _playerInventoryParent.SetActive(false);
        InputReader.Instance.OnPressInteractButtonEvent -= OnPressInteractButtonEvent;
        InputReader.Instance.SetCursorState(true);
    }
}
