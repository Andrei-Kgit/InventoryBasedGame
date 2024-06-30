using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    [SerializeField] private List<InventoryCell> _cells;
    [SerializeField] private InventoryCell _cellPrefab;
    [SerializeField] private Transform _inventoryGrid;
    [SerializeField] private Transform _draggingPaent;
    [SerializeField] private InventoryPopup _inventoryPopup;
    [SerializeField] private int _cellsCount = 36;

    private void Start()
    {
        FillInventory(_cellsCount);
        AddItems(_items);
    }

    private void FillInventory(int inventorySize)
    {
        _cells.Clear();
        _cells = new List<InventoryCell>();
        for (int i = 0; i < _cellsCount; i++)
        {
            var cell = Instantiate(_cellPrefab, _inventoryGrid);
            cell.Init(_draggingPaent);
            cell.OnCellClick += _inventoryPopup.Show;
            _cells.Add(cell);
        }
    }

    public void AddItems(List<Item> items)
    {
        foreach (InventoryCell cell in _cells)
        {
            cell.RemoveItem();
        }

        for (int i = 0; i < items.Count; i++)
        {
            _cells[i].AddItem(items[i]);
        }
    }
}
