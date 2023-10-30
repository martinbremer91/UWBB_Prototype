namespace DefaultNamespace
{
    public interface IAttackable
    {
        IDamageable damageable { get; }
        IKnockbackable knockbackable { get; }
    }
}