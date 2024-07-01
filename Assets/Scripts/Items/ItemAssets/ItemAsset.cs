using UnityEngine;

[CreateAssetMenu(menuName ="Items/OtherItem")]
public class ItemAsset : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public int MaxStacks;
    public float Weight;
    [HideInInspector] public ItemType ItemType;
}
