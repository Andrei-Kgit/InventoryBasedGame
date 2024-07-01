using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action OnShoot;

    public int Ammo => _ammo;

    [SerializeField] private float _damage;
    [SerializeField] private int _ammo;
    [SerializeField] private int _ammoUse;

    public void Shoot(IDamagable target)
    {
        if (_ammo >= _ammoUse)
        {
            target.GetDamage(_damage);
            _ammo -= _ammoUse;
            OnShoot?.Invoke();
        }
    }
}
