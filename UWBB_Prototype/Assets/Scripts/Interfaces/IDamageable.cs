namespace UWBB.Interfaces
{
    public interface IDamageable
    {
        int totalHealthPoints { get; }
        int currentHealthPoints { get; set; }

        void TakeDamage(int value) => currentHealthPoints -= value;
    }
}