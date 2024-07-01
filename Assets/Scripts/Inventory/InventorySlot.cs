using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action<Item> OnSlotClick;
    public Item ContainedItem => _containedItem;
    public bool HasItem => _hasItem;

    [SerializeField] private TMP_Text _itemsCountText;
    [SerializeField] private Image _icon;
    private Button _slotButton;
    private Image _slotImage;

    private bool _hasItem = false;

    private Inventory _inventory;

    private Item _containedItem;

    private Transform _draggingParent;
    private Transform _originalParent;
    private Transform _content;

    public void Init(Transform draggingParent, Inventory inventory)
    {
        _inventory = inventory;
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
        _content = transform.parent.parent;
        _icon.gameObject.SetActive(true);
        _itemsCountText.gameObject.SetActive(true);
        _slotButton = GetComponent<Button>();
        _slotImage = GetComponent<Image>();
        _slotButton.onClick.AddListener(SlotClick);
    }

    private void SlotClick()
    {
        OnSlotClick?.Invoke(_containedItem);
    }

    public void SetOriginalParent(Transform parent)
    {
        _originalParent = parent;
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            RemoveItem();
            return;
        }

        _containedItem = item;
        _hasItem = true;

        _containedItem.OnItemRemove += RemoveItem;

        CheckItemStacks();

        UpdateUI();
    }

    private void CheckItemStacks()
    {
        if (_containedItem.CurrentStacks < 1)
            RemoveItem();
    }

    private void UpdateUI()
    {
        if (HasItem)
        {
            _itemsCountText.gameObject.SetActive(_containedItem.CurrentStacks > 1);
            _icon.sprite = _containedItem.Icon;
            _itemsCountText.text = _containedItem.CurrentStacks.ToString();
        }
    }

    public void RemoveItem()
    {
        _inventory.RemoveSlot(this);
    }

    private void OnDestroy()
    {
        _containedItem.OnItemRemove -= RemoveItem;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (HasItem)
        {
            transform.SetParent(_draggingParent, true);
            transform.SetAsLastSibling();
            _slotImage.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (HasItem)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (HasItem)
        {
            int closestCellIndex = 0;
            for (int i = 0; i < _content.childCount; i++)
            {
                float currentCellsDist = Vector3.Distance(transform.position, _content.GetChild(i).position);
                float maxCellsDist = Vector3.Distance(transform.position, _content.GetChild(closestCellIndex).position);
                if (currentCellsDist < maxCellsDist)
                {
                    closestCellIndex = i;
                }
            }
            float mouseDistToCell = Vector3.Distance(Input.mousePosition, _content.GetChild(closestCellIndex).position);

            if (mouseDistToCell < 20)
            {
                Transform closestCell = _content.GetChild(closestCellIndex);

                if (closestCell.childCount > 0)
                {
                    var closestCellChild = closestCell.GetChild(0);
                    InventorySlot childSlot;
                    if (closestCellChild.TryGetComponent(out childSlot))
                    {
                        if (childSlot.ContainedItem.Name == ContainedItem.Name)
                        {
                            if (childSlot.ContainedItem.CurrentStacks < childSlot.ContainedItem.MaxStacks)
                            {
                                int neededStacks = childSlot.ContainedItem.MaxStacks - childSlot.ContainedItem.CurrentStacks;
                                int canAddStacks = ContainedItem.CurrentStacks;
                                if (canAddStacks <= neededStacks)
                                {
                                    childSlot.ContainedItem.ChangeStacks(canAddStacks);
                                    ContainedItem.ChangeStacks(-canAddStacks);
                                }
                                else
                                {
                                    int stacksToAdd = canAddStacks - neededStacks;
                                    childSlot.ContainedItem.ChangeStacks(stacksToAdd);
                                    ContainedItem.ChangeStacks(-stacksToAdd);
                                }
                                CheckItemStacks();
                            }
                        }
                        else
                        {
                            closestCellChild.SetParent(_originalParent);
                            childSlot.SetOriginalParent(_originalParent);
                        }
                    }
                }

                transform.SetParent(closestCell);
                _originalParent = closestCell;
            }
            else
            {
                transform.SetParent(_originalParent);
            }




            _slotImage.raycastTarget = true;
        }
    }

}
