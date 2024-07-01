using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemsConteiner")]
public class ItemsContainer : ScriptableObject
{
    public List<ItemAsset> Items = new List<ItemAsset>();

    private void OnValidate()
    {
        var items = Items.GroupBy(item => item.Name).Where(array => array.Count() > 1);
        if (items.Count() > 0)
            throw new InvalidOperationException(nameof(items));
    }
}
