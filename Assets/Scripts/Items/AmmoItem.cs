public class AmmoItem : Item
{
    public WeaponType WeaponType => _weaponType;
    private WeaponType _weaponType;
    public AmmoItem(ItemAsset item, int currentStacks = 0) : base(item, currentStacks)
    {
        AmmoItemAsset ammoItem = item as AmmoItemAsset;
        _weaponType = ammoItem.WeaponType;
    }

    public override void Use()
    {

        base.Use();
    }
}
