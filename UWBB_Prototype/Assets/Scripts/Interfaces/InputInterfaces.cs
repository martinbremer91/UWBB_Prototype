using UnityEngine.InputSystem;

namespace UWBB.Interfaces
{
    public interface IInputLogic
    {
        public void Init();
    }

    public interface IInputLogic<T> : IInputLogic where T : IInputActionCollection2
    {
        public T controls { get; set; }
    }
}