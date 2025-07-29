using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int CurrentHealth { get; private set; }

    public void SetMaxHealth(int value)
    {
        CurrentHealth = value;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0)
            CurrentHealth = 0;
    }
}