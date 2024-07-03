using UnityEngine;

[CreateAssetMenu(menuName = "Items/AmmoItem")]
public class AmmoItemAsset : ItemAsset
{
    public WeaponType WeaponType;

    private void OnEnable()
    {
        ItemType = ItemType.Ammo;
    }
}
