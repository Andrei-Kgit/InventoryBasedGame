using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemsContainer _items;
    [SerializeField] private ItemAsset _newItem;
    [SerializeField] private InventorySlot _slotPrefab;
    [SerializeField] private GridCell _gridCellPrefab;
    [SerializeField] private Transform _inventoryGrid;
    [SerializeField] private Transform _draggingPaent;
    [SerializeField] private InventoryPopup _inventoryPopup;
    [SerializeField] private int _cellsCount = 36;
    private List<GridCell> _cells = new List<GridCell>();
    private List<InventorySlot> _slots = new List<InventorySlot>();

    private void Start()
    {
        FillInventory(_cellsCount);

        ClearItems();
        if (_items.Items != null)
            AddItems(_items.Items);
        if (_newItem != null)
            AddItem(_newItem);
    }

    private void FillInventory(int inventorySize)
    {
        _slots.Clear();
        _cells.Clear();
        for (int i = 0; i < _cellsCount; i++)
        {
            var cell = Instantiate(_gridCellPrefab, _inventoryGrid);
            _cells.Add(cell);
        }
    }

    private void ClearItems()
    {
        foreach (InventorySlot cell in _slots)
        {
            cell.RemoveItem();
        }
    }

    public void AddItems(List<ItemAsset> items)
    {
        foreach (ItemAsset item in items)
        {
            var slot = Instantiate(_slotPrefab, _cells[GetFreeCell()].transform);

            _slots.Add(slot);
            slot.AddItem(GetItemImplementation(item));
            slot.Init(_draggingPaent, this);
            slot.OnSlotClick += _inventoryPopup.Show;
        }
    }

    public void RemoveSlot(InventorySlot slot)
    {
        _slots.Remove(slot);
        Destroy(slot.gameObject);
    }

    private Item GetItemImplementation(ItemAsset item)
    {
        Item newItem;
        switch (item.ItemType)
        {
            case ItemType.Ammo:
                newItem = new AmmoItem(item, item.MaxStacks/2);
                break;
            case ItemType.Med:
                newItem = new MedItem(item);
                break;
            case ItemType.Equipment:
                newItem = new EquipmentItem(item);
                break;
            default:
                newItem = new OtherItem(item);
                break;
        }
        return newItem;
    }

    public void AddItem(ItemAsset item)
    {
        if (item != null)
        {
            var slot = Instantiate(_slotPrefab, _cells[GetFreeCell()].transform);
            _slots.Add(slot);
            slot.AddItem(GetItemImplementation(item));
            slot.Init(_draggingPaent, this);
            slot.OnSlotClick += _inventoryPopup.Show;
        }
    }

    private int GetFreeCell()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            if (_cells[i].transform.childCount == 0)
            {
                return i;
            }
        }
        return 0;
    }
}
