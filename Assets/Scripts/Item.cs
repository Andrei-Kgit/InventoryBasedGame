using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public class Item : ScriptableObject, IItem
{
    public string Name => _name;
    public Sprite Icon => _icon;
    public int CurrentStacks => _currentStacks;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _maxStacks;
    private int _currentStacks = 1;

    public void ChangeStacks(int mod)
    {
        _currentStacks += mod;

        if(_currentStacks <= 0)
            _currentStacks = 0;

        if(_currentStacks > _maxStacks)
            _currentStacks = _maxStacks;
    }

    //todo переделать в абстрактный и добавить функцию virtual Use чтобы через попап можно было использовать
}
