using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    private Player _player;
    private Inventory _inventory;
    private Enemy _enemy;

    public void Init(Player player, Inventory inventory)
    {
        _player = player;
        _inventory = inventory;
    }

    public Enemy SpawnEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        enemy.Init(_player, _inventory);

        return enemy;
    }
}
