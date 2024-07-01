public class EquipmentItem : Item
{
    public int Defence => _defence;
    private int _defence;

    public EquipmentItem(ItemAsset item) : base(item)
    {
        var newItem = item as EquipmentItemAsset;
        _defence = newItem.Defence;
    }

    public override void Use()
    {

        base.Use();
    }
}
