using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour, IDropHandler
{
    private InventorySlot _inventorySlot;
    private bool _hasSlot = false;

    public void TakeSlot(InventorySlot slot)
    {
        _inventorySlot = slot;
        _hasSlot = true;
    }
    public InventorySlot ReleseSlot()
    {
        _hasSlot = false;
        return _inventorySlot;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }
}
