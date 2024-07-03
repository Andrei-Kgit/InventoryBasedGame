using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float _minDamage = 10;
    [SerializeField] private float _maxDamage = 15;
    [SerializeField] private SpriteRenderer _enemyVFX;
    private Inventory _inventory;
    private Player _player;
    private float _damage = 0;

    public void Init(Player player, Inventory inventory)
    {
        _player = player;
        _inventory = inventory;
        ResetEnemy();
    }

    public void GetTurn()
    {
        Invoke("Attack", 1f);
    }

    private void Attack()
    {
        _player.GetDamage(_damage);
        _player.GetTurn();
    }

    private void ResetEnemy()
    {
        _damage = Random.Range(_minDamage, _maxDamage);
        _enemyVFX.color = GetRandomColor();
        Heal(MaxHealth);
    }

    public override void GetDamage(float value)
    {
        base.GetDamage(value);
        GetTurn();
    }

    private Color GetRandomColor()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        return color;
    }

    public override void Die()
    {
        _inventory.AddItem();
        ResetEnemy();
    }
}
