using UnityEngine;

public class GridCell : MonoBehaviour
{
    public InventorySlot InventorySlot => _inventorySlot;
    public EquipmentType EquipmentType => _equipmentType;
    [SerializeField] private EquipmentType _equipmentType;
    private InventorySlot _inventorySlot;

    public void TakeSlot(InventorySlot slot)
    {
        _inventorySlot = slot;
    }
}

public enum EquipmentType
{
    None,
    Head,
    Body,
}
