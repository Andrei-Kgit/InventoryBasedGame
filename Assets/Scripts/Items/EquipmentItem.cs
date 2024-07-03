public class EquipmentItem : Item
{
    public int Defence => _defence;
    public EquipmentType Type => _type;
    private int _defence;
    private EquipmentType _type;

    public EquipmentItem(ItemAsset item) : base(item)
    {
        var newItem = item as EquipmentItemAsset;
        _defence = newItem.Defence;
        _type = newItem.Type;
    }

    public override void Use()
    {

        base.Use();
    }
}
