namespace UWBB.Interfaces
{
    public interface IAttackable
    {
        IDamageable damageable { get; }
        IKnockbackable knockbackable { get; }
    }
}