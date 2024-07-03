using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action<int> OnAmmoChange;
    public event Action OnShot;
    public WeaponType Type => _type;
    public int Ammo => _ammo;
    public int MaxAmmo => _maxAmmo;

    [SerializeField] private float _damage;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _ammo—onsumption;
    [SerializeField] private WeaponType _type = WeaponType.Pistol;
    private int _ammo;

    private void Awake()
    {
        _ammo = _maxAmmo;
    }

    public void AddAmmo(int ammo)
    {
        _ammo += Mathf.Abs(ammo);
        if(_ammo > _maxAmmo)
            _ammo = _maxAmmo;
        OnAmmoChange?.Invoke(Ammo);
    }

    public void Shoot(IDamagable target)
    {
        if (_ammo >= _ammo—onsumption)
        {
            target.GetDamage(_damage);
            _ammo -= _ammo—onsumption;
            OnAmmoChange?.Invoke(Ammo);
        }
    }
}

public enum WeaponType
{
    Pistol,
    SMG,
}
