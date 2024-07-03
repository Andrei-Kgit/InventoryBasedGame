using UnityEngine;

[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItemAsset : ItemAsset
{
    public int Defence;
    public EquipmentType Type;

    private void OnEnable()
    {
        ItemType = ItemType.Equipment;
    }
}
