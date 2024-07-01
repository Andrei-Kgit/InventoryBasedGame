public class MedItem : Item
{
    public float HealStrength => _healStrength;
    private float _healStrength;

    public MedItem(ItemAsset item) : base(item)
    {
        var newItem = item as MedItemAsset;
        _healStrength = newItem.HealValue;
    }

    public override void Use()
    {

        base.Use();
    }
}
