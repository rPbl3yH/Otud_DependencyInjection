using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    private GameObject _target;

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void Attack()
    {
        if (_target == null) return;

        HealthComponent enemyHealthComponent = _target.GetComponent<HealthComponent>();
        if (enemyHealthComponent != null)
        {
            // Условный урон
            enemyHealthComponent.TakeDamage(25);
        }
    }
}