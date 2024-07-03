public interface IDamagable
{
    public float CurrentHealth { get; }

    public abstract void GetDamage(float value);
    public abstract void Die();
}
