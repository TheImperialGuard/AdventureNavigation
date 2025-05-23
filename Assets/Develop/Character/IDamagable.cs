public interface IDamagable
{
    int MaxHealth { get; }

    int CurrentHealth { get; }

    void TakeDamage(int damage);
}
