using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable
{
    public event Action<float> OnHealthChange;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    [SerializeField] private float _maxHealth;
    protected float _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void GetDamage(float value)
    {
        if (value < 0)
            value *= -1;

        _currentHealth -= value;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        OnHealthChange?.Invoke(_currentHealth / _maxHealth);
    }

    public virtual void Heal(float value)
    {
        _currentHealth += Mathf.Abs(value);
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
        OnHealthChange?.Invoke(_currentHealth / _maxHealth);
    }

    public virtual void Die()
    {
    }
}
