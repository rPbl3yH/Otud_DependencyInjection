using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemySpawnIntegrationTests
{
    [UnityTest]
    public IEnumerator WhenSpawnEnemy_AndFactoryHasValidPrefab_ThenEnemyHasHealthAndAI()
    {
        var factoryObject = new GameObject("Factory");
        var factory = factoryObject.AddComponent<EnemyFactory>();

        GameObject enemy = factory.SpawnEnemy(new Vector3(1, 0, 0));
        
        yield return null;

        Assert.NotNull(enemy.GetComponent<HealthComponent>());
        Assert.NotNull(enemy.GetComponent<EnemyAI>());
        Assert.AreEqual(new Vector3(1, 0, 0), enemy.transform.position);
    }
}