using UnityEngine;

[CreateAssetMenu(menuName = "Items/MedItem")]
public class MedItemAsset : ItemAsset
{
    public float HealValue;

    private void OnEnable()
    {
        ItemType = ItemType.Med;
    }
}
