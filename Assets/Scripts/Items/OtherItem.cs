using UnityEngine;

public class OtherItem : Item
{
    public OtherItem(ItemAsset item) : base(item)
    {
    }

    public override void Use()
    {
        Debug.Log("There is no use");
        base.Use();
    }
}
