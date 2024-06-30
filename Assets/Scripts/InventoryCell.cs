using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action<Item> OnCellClick;
    public Item ContainedItem => _containedItem;
    public bool HasItem = false;

    [SerializeField] private TMP_Text _itemsCountText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _cellButton;

    private Item _containedItem;

    private Transform _draggingParent;
    private Transform _originalParent;

    public void Init(Transform draggingParent)
    {
        _icon.gameObject.SetActive(true);
        _itemsCountText.gameObject.SetActive(true);
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
        _cellButton.onClick.AddListener(CellClick);
    }

    private void CellClick()
    {
        OnCellClick?.Invoke(_containedItem);
    }

    public void AddItem(IItem item)
    {
        _containedItem = item as Item;
        _itemsCountText.text = _containedItem.CurrentStacks.ToString();
        _icon.sprite = _containedItem.Icon;
        HasItem = true;

        _itemsCountText.gameObject.SetActive(_containedItem.CurrentStacks > 1);

        if(_containedItem.CurrentStacks < 1)
            RemoveItem();
    }

    public void RemoveItem()
    {
        _icon.sprite = null;
        _itemsCountText.text = null;
        HasItem = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //todo создать временный айтем, отображать его в начале перемещения. Добавить ему все то, что есть в этой ячейке, если она не пустая
        //сохранять индекс ячейки, из которой взяли айтем
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        //todo перемещать созданную новую tempCell за мышью с полупрозрачностью или тип того
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int closestItemIndex = 0;

        for (int i = 0; i < _originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                Vector3.Distance(transform.position, _originalParent.GetChild(closestItemIndex).position))
            {
                closestItemIndex = i;
            }
        }
        //todo брать ближайшую ячейку , сохранять ее во временную переменную, добавлять на ячейку то, что было в tempCell, а на место от куда взяли
        //кидать то, что во временной переменной
    }
}
