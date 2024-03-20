using UWBB.CharacterController;

namespace UWBB.Interfaces
{
    public interface IPlayerLogic<in T, U> : IInitializable<Player>
        where T : IInputState 
        where U : IPlayerLogicData
    {
        public U RunUpdate(T inputState);
    }

    public interface IPlayerLogicData {}
}