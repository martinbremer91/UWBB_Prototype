using System.Collections.Generic;
using UnityEngine;

namespace UWBB.CharacterController
{
    public interface ICharacter
    {
        public MonoBehaviour monoBehaviour { get; }
        public CharacterConfigs characterConfigs { get; }
        public Dictionary<ControllerType, ICharacterController> controllers { get; set; }

        public void RegisterControllers();
        public T GetModuleController<T>(ControllerType type) where T : class, ICharacterController 
            => controllers[type] as T;
    }

    public interface ICharacterController
    {
        public void Init(ICharacter character);
    }
}