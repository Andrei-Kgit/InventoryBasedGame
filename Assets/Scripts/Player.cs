using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public event Action<float, float> OnDefenceChange;
    [SerializeField] private GameOver _gameOverPanel;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private EnemySpawner _enemySpawner;
    private InventorySlot _headSlot;
    private InventorySlot _bodySlot;
    private Weapon _selectedWeapon;
    private Enemy _enemy;
    private int _headDefence = 0;
    private int _bodyDefence = 0;
    private float _damage = 0;
    private bool _isPlayerTurn = true;

    private void Start()
    {
        _headSlot = _inventory.HeadSlot;
        _bodySlot = _inventory.BodySlot;
        _enemySpawner.Init(this, _inventory);
        _enemy = _enemySpawner.SpawnEnemy();
        _attackButton.onClick.AddListener(() => Attack(_enemy));
        _inventory.OnItemUse += UseItem;
        _inventory.OnEquipmentChange += SetDefence;
    }

    public void GetTurn()
    {
        _isPlayerTurn = true;
    }

    private void Attack(IDamagable target)
    {
        if (_isPlayerTurn && _selectedWeapon != null)
        {
            _selectedWeapon.Shoot(target);
            _isPlayerTurn = false;
        }
    }

    public void SelectWeapon(Weapon weapon)
    {
        _selectedWeapon = weapon;
    }

    private void UseItem(Item item)
    {
        switch (item.ItemType)
        {
            case ItemType.Ammo:

                AmmoItem ammoItem = (AmmoItem)item;
                if (ammoItem.WeaponType == _selectedWeapon.Type)
                {
                    int neededAmmoCount = _selectedWeapon.MaxAmmo - _selectedWeapon.Ammo;
                    if (ammoItem.CurrentStacks >= neededAmmoCount)
                    {
                        _selectedWeapon.AddAmmo(neededAmmoCount);
                        ammoItem.ChangeStacks(-neededAmmoCount);
                    }
                    else
                    {
                        _selectedWeapon.AddAmmo(ammoItem.CurrentStacks);
                        ammoItem.ChangeStacks(-ammoItem.CurrentStacks);
                    }
                }
                else
                {
                    Debug.Log("Wrong ammo type");
                }

                break;
            case ItemType.Med:

                MedItem medItem = (MedItem)item;
                if (CurrentHealth < MaxHealth)
                {
                    Heal(medItem.HealStrength);
                    medItem.ChangeStacks(-1);
                }

                break;
            case ItemType.Equipment:
                break;
            default:
                Debug.Log("Cant use");
                break;
        }
    }

    private void SetDefence(EquipmentItem item)
    {
        if(item.Type==EquipmentType.Head)
        {
            _headDefence = item.Defence;
        }
        if(item.Type == EquipmentType.Body)
        {
            _bodyDefence = item.Defence;
        }
        OnDefenceChange?.Invoke(_headDefence, _bodyDefence);
    }

    public override void GetDamage(float value)
    {
        //30% headshot chance
        if (UnityEngine.Random.value < 0.3f)
        {
            value -= _headDefence;
        }
        else
        {
            value -= _bodyDefence;
        }
        base.GetDamage(value);
    }

    public override void Die()
    {
        _gameOverPanel.Show();
    }

    private void OnDisable()
    {
        _attackButton.onClick.RemoveAllListeners();
        _inventory.OnItemUse -= UseItem;
        _inventory.OnEquipmentChange -= SetDefence;
    }
}
