using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerEnemyCombatIntegrationTests
{
    [UnityTest]
    public IEnumerator WhenPlayerAttacks_AndEnemyHasHealth_ThenEnemyTakesDamage()
    {
        var player = new GameObject("Player");
        var enemy = new GameObject("Enemy");

        var playerAttack = player.AddComponent<PlayerAttackComponent>();
        var enemyHealth = enemy.AddComponent<HealthComponent>();
        enemyHealth.SetMaxHealth(100);

        playerAttack.SetTarget(enemy);
        playerAttack.Attack();

        yield return new WaitForSeconds(0.5f); // ждём завершения анимации атаки

        Assert.Less(enemyHealth.CurrentHealth, 100);
    }
}