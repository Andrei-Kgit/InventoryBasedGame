using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamagable
{
    public float Health => _currentHealth;

    [SerializeField] private float _maxHealth;
    [SerializeField] private Weapon _selectedWeapon;
    [SerializeField] private Button _attackButton;
    [SerializeField] private Inventory _inventory;
    private float _currentHealth;
    private int _defence = 0;
    private float _damage = 0;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Attack(IDamagable target)
    {
        _selectedWeapon.Shoot(target);
    }

    public void SelectWeapon(Weapon weapon)
    {
        _selectedWeapon = weapon;
    }

    public void GetDamage(float value)
    {
        _currentHealth += value;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player is Dead!");
    }
}
