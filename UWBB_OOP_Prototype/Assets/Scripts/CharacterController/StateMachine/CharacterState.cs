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
        RunRecovery,
        
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

        UsingItemStart,
        UsingItemMain,
        UsingItemRecovery,
        
        StunnedMain,
        StunnedRecovery,
    }
}