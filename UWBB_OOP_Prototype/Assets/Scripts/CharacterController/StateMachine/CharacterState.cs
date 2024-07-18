namespace UWBB.CharacterController
{
    public enum CharacterState
    {
        Idle,
        Walk,
        Run,
        Dodge,
        AttackLight,
        AttackHeavy,
        UsingItem,
        Stunned,
    }

    public enum CharacterSubState
    {
        Idle,
        Walk,

        RunStart,
        RunMain,
        
        DodgeStart,
        DodgeMain,
        DodgeRecovery,

        AttackLightStart,
        AttackLightMain,
        AttackLightRecovery,

        AttackHeavyStart,
        AttackHeavyCharge,
        AttackHeavyMain,
        AttackHeavyRecovery,

        UseItemStart,
        UseItemMain,
        UseItemRecovery,
        
        StunMain,
        StunRecovery,
    }
}