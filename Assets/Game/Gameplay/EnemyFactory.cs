using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    public GameObject SpawnEnemy(Vector3 position)
    {
        if (_enemyPrefab == null)
        {
            _enemyPrefab = CreateDefaultEnemyPrefab(); // fallback for test
        }

        GameObject enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
        return enemy;
    }

    private GameObject CreateDefaultEnemyPrefab()
    {
        GameObject prefab = new GameObject("Enemy");
        prefab.AddComponent<HealthComponent>();
        prefab.AddComponent<EnemyAI>();
        return prefab;
    }
}