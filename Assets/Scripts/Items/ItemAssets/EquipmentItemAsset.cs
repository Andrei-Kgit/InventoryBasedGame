using UnityEngine;

[CreateAssetMenu(menuName = "Items/EquipmentItem")]
public class EquipmentItemAsset : ItemAsset
{
    public int Defence;

    private void OnEnable()
    {
        ItemType = ItemType.Equipment;
    }
}
