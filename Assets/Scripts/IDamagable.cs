public interface IDamagable
{
    public float Health { get; }

    public abstract void GetDamage(float value);
    public abstract void Die();
}
