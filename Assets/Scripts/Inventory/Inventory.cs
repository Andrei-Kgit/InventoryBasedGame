using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> OnItemUse;
    public event Action<EquipmentItem> OnEquipmentChange;
    public InventorySlot HeadSlot => _headSlot;
    public InventorySlot BodySlot => _bodySlot;
    [Header("Items containers")]
    [SerializeField] private ItemsContainer _startItems;
    [SerializeField] private ItemsContainer _randomItems;
    [Header("Inventory slots")]
    [SerializeField] private GridCell _gridCellPrefab;
    [SerializeField] private InventorySlot _inventorySlotPrefab;
    [SerializeField] private Transform _inventoryGrid;
    [SerializeField] private GridCell _headCellPrefab;
    [SerializeField] private GridCell _bodyCellPrefab;
    [SerializeField] private Transform _equipmentGrid;
    [Header("Slots behaviour")]
    [SerializeField] private Transform _draggingPaent;
    [SerializeField] private InventoryPopup _inventoryPopup;
    [Header("Inventory properties")]
    [SerializeField] private int _cellsCount = 36;
    private List<GridCell> _inventoryCells = new List<GridCell>();
    private GridCell _headCell;
    private GridCell _bodyCell;
    private InventorySlot _headSlot;
    private InventorySlot _bodySlot;
    private List<InventorySlot> _slots = new List<InventorySlot>();

    private void Start()
    {
        FillInventory(_cellsCount);

        ClearItems();
        if (_startItems.Items != null)
            AddItems(_startItems.Items);
    }

    private void FillInventory(int inventorySize)
    {
        _slots.Clear();
        _inventoryCells.Clear();
        for (int i = 0; i < _cellsCount; i++)
        {
            var cell = Instantiate(_gridCellPrefab, _inventoryGrid);
            _inventoryCells.Add(cell);
        }

        var headCell = Instantiate(_headCellPrefab, _equipmentGrid);
        var bodyCell = Instantiate(_bodyCellPrefab, _equipmentGrid);
        _headCell = headCell;
        _bodyCell = bodyCell;
    }

    private void ClearItems()
    {
        foreach (InventorySlot cell in _slots)
        {
            cell.RemoveItem();
        }
    }

    public void AddItem(ItemAsset item)
    {
        if (item != null)
        {
            var slot = Instantiate(_inventorySlotPrefab, _inventoryCells[GetFreeCell()].transform);
            _slots.Add(slot);
            Item implementedItem = GetItemImplementation(item);
            slot.AddItem(GetItemImplementation(item));
            slot.Init(_draggingPaent, this);
            slot.OnSlotClick += _inventoryPopup.Show;

        }
    }
    public void AddItem()
    {
        if (_randomItems.Items.Count > 0)
        {
            ItemAsset itemAsset = _randomItems.Items[UnityEngine.Random.Range(0, _randomItems.Items.Count)];

            if (itemAsset != null)
            {
                var slot = Instantiate(_inventorySlotPrefab, _inventoryCells[GetFreeCell()].transform);
                _slots.Add(slot);
                slot.AddItem(GetItemImplementation(itemAsset));
                slot.Init(_draggingPaent, this);
                slot.OnSlotClick += _inventoryPopup.Show;
            }
        }
    }

    public void AddItems(List<ItemAsset> items)
    {
        foreach (ItemAsset item in items)
        {
            var slot = Instantiate(_inventorySlotPrefab, _inventoryCells[GetFreeCell()].transform);

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
                newItem = new AmmoItem(item, item.MaxStacks / 2);
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

    private int GetFreeCell()
    {
        for (int i = 0; i < _inventoryCells.Count; i++)
        {
            if (_inventoryCells[i].transform.childCount == 0)
            {
                return i;
            }
        }
        return 0;
    }

    public void UseItem(Item item)
    {
        OnItemUse?.Invoke(item);
    }

    public void EquipItem(InventorySlot slot)
    {
        if (slot.ContainedItem == null)
            return;

        if (slot.ContainedItem.ItemType == ItemType.Equipment)
        {
            Transform slotParent = slot.transform.parent;
            EquipmentItem equipmentItem = (EquipmentItem)slot.ContainedItem;

            switch (equipmentItem.Type)
            {
                case EquipmentType.None:
                    Debug.Log("Not equipment");
                    break;
                case EquipmentType.Head:
                    if (_headSlot == null)
                    {
                        slot.SetOriginalParent(_headCell.transform);
                        slot.transform.SetParent(_headCell.transform);
                        _headSlot = slot;
                        _headSlot.SetToEquipmentSlot(true);
                    }
                    else
                    {
                        InventorySlot tempSlot = _headSlot;
                        slot.SetOriginalParent(_headCell.transform);
                        slot.transform.SetParent(_headCell.transform);
                        _headSlot = slot;
                        _headSlot.SetToEquipmentSlot(true);
                        tempSlot.SetOriginalParent(slotParent);
                        tempSlot.transform.SetParent(slotParent);
                        tempSlot.SetToEquipmentSlot(false);
                    }
                    break;
                case EquipmentType.Body:
                    if (_bodySlot == null)
                    {
                        slot.SetOriginalParent(_bodyCell.transform);
                        slot.transform.SetParent(_bodyCell.transform);
                        _bodySlot = slot;
                        _bodySlot.SetToEquipmentSlot(true);
                    }
                    else
                    {
                        InventorySlot tempSlot = _bodySlot;
                        slot.SetOriginalParent(_bodyCell.transform);
                        slot.transform.SetParent(_bodyCell.transform);
                        _bodySlot = slot;
                        _bodySlot.SetToEquipmentSlot(true);
                        tempSlot.SetOriginalParent(slotParent);
                        tempSlot.transform.SetParent(slotParent);
                        tempSlot.SetToEquipmentSlot(false);
                    }
                    break;
                default:
                    Debug.Log("Not equipment");
                    break;
            }

            OnEquipmentChange?.Invoke(equipmentItem);
        }
    }
}
