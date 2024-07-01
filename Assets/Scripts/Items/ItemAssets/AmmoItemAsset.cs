using UnityEngine;

[CreateAssetMenu(menuName = "Items/AmmoItem")]
public class AmmoItemAsset : ItemAsset
{
    private void OnEnable()
    {
        ItemType = ItemType.Ammo;
    }
}
