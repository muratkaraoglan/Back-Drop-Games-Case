using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private OnDragBeginEndEvent _onDragBeginEndEvent;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    private Image _raycastTarget;
    private Canvas _canvas;
    private ItemData _itemData;
    private string _itemID;
    private IInventory _currentInventory;
    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _raycastTarget = GetComponent<Image>();
    }

    public void Initialize(IInventory inventory, string id)
    {
        _itemData = ItemManager.Instance.GetItemData(id);
        _itemID = id;
        _itemImage.sprite = _itemData.ItemIcon;
        _itemNameText.SetText(_itemData.ItemName);
        _currentInventory = inventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_canvas.transform, true);
        _onDragBeginEndEvent.Raise(false);
        _currentInventory.RemoveFromInventory(_itemID);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _onDragBeginEndEvent.Raise(true);
        _currentInventory.OnDragEnd(eventData, _itemID, this);
    }

    public void OnDragBeginEnd(bool state)
    {
        _raycastTarget.raycastTarget = state;
    }

    public void ChangeInventory(IInventory inventory) => _currentInventory = inventory;

    public void SetAnimatorState(bool state) => _animator.enabled = state;
}
