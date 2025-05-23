using UnityEngine;

public class Health
{
    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public int MaxHealth { get; }

    public int CurrentHealth { get; private set; }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogError("Урон не может быть меньше 0");
            return;
        }

        CurrentHealth -= damage;

        if (CurrentHealth < 0)
            CurrentHealth = 0;
    }
}
