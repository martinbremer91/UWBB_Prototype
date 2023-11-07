using UnityEngine.InputSystem;

namespace UWBB.Interfaces
{
    public interface IInputController
    {
        public void Init();
    }

    public interface IInputController<T> : IInputController where T : IInputActionCollection2
    {
        public T controls { get; set; }
    }

    public interface IInputState {}
}