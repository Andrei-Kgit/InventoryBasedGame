using System;
using UnityEngine;

public abstract class Item
{
    public event Action OnItemUse;
    public event Action OnItemRemove;

    public string Name => _name;
    public string ActionButtonText => _actionButtonLabel;
    public Sprite Icon => _icon;
    public int CurrentStacks => _currentStacks;
    public int MaxStacks => _maxStacks;
    public float Weight => _weight;
    public ItemType ItemType => _itemType;

    private string _name;
    private string _actionButtonLabel;
    private Sprite _icon;
    private int _maxStacks;
    private int _currentStacks;
    private float _weight;
    private ItemType _itemType;

    public Item(ItemAsset item, int currentStacks = 0)
    {
        _name = item.Name;
        _icon = item.Icon;
        _weight = item.Weight;
        _maxStacks = item.MaxStacks;
        _itemType = item.ItemType;
        _currentStacks = currentStacks;
        _actionButtonLabel = GetActionButtonLabel();

        if (_currentStacks == 0)
            _currentStacks = _maxStacks;
    }

    private string GetActionButtonLabel()
    {
        switch (_itemType)
        {
            case ItemType.Ammo:
                return " упить";
            case ItemType.Med:
                return "Ћечить";
            case ItemType.Equipment:
                return "Ёкипировать";
            default:
                return "»спользовать";
        }
    }

    public virtual void ChangeStacks(int mod)
    {
        _currentStacks += mod;

        if (_currentStacks <= 0)
            _currentStacks = 0;

        if (_currentStacks > _maxStacks)
            _currentStacks = _maxStacks;
    }

    public virtual void Use()
    {
        OnItemUse?.Invoke();
    }

    public virtual void RemoveItem()
    {
        OnItemRemove?.Invoke();
    }
}

public enum ItemType
{
    Ammo,
    Med,
    Equipment,
}
