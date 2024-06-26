//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Data/InputActions/DefaultControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @DefaultControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultControls"",
    ""maps"": [
        {
            ""name"": ""FreeMovement"",
            ""id"": ""1c06ef7f-e538-43e2-a293-f9160d65d458"",
            ""actions"": [
                {
                    ""name"": ""XZ Movement"",
                    ""type"": ""Value"",
                    ""id"": ""73cf6aaa-7931-48db-a037-6b0fbd8551d1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone(min=0.15,max=0.9)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Y Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""83b3335e-a3f9-48cb-9285-0b566caf0d16"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera Stick"",
                    ""type"": ""Value"",
                    ""id"": ""d995fb88-d497-447b-b3ef-f71dbc626228"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone(min=0.15,max=0.9)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SnapToHorizon"",
                    ""type"": ""Button"",
                    ""id"": ""a4dcadb0-e04f-46f1-a764-3fa0cc17174c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnCommand"",
                    ""type"": ""Button"",
                    ""id"": ""543e3563-54da-414a-bf80-8599f4202354"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""c028584e-3237-47e4-94cb-887e6697a443"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a00971e-4552-4afb-aa15-f3c4d267e49c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XZ Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8476cd04-57d5-47a7-b8d0-63fabdc759bf"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4f8f615-2afc-44a9-8234-3c047c4047e7"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapToHorizon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""075736a5-07c7-4445-85a6-2d01d7b461b2"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnCommand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8db73cc8-2607-42e6-a897-6393a5c226f0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d34877c0-22a8-42b6-90ef-d65f5fc44cf7"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5d3b591a-790b-48e7-8890-1bf97b4db916"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3ee71fb1-3b9a-473e-8201-0db1ae9e5a6e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FreeMovement
        m_FreeMovement = asset.FindActionMap("FreeMovement", throwIfNotFound: true);
        m_FreeMovement_XZMovement = m_FreeMovement.FindAction("XZ Movement", throwIfNotFound: true);
        m_FreeMovement_YMovement = m_FreeMovement.FindAction("Y Movement", throwIfNotFound: true);
        m_FreeMovement_CameraStick = m_FreeMovement.FindAction("Camera Stick", throwIfNotFound: true);
        m_FreeMovement_SnapToHorizon = m_FreeMovement.FindAction("SnapToHorizon", throwIfNotFound: true);
        m_FreeMovement_LockOnCommand = m_FreeMovement.FindAction("LockOnCommand", throwIfNotFound: true);
        m_FreeMovement_Dash = m_FreeMovement.FindAction("Dash", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // FreeMovement
    private readonly InputActionMap m_FreeMovement;
    private List<IFreeMovementActions> m_FreeMovementActionsCallbackInterfaces = new List<IFreeMovementActions>();
    private readonly InputAction m_FreeMovement_XZMovement;
    private readonly InputAction m_FreeMovement_YMovement;
    private readonly InputAction m_FreeMovement_CameraStick;
    private readonly InputAction m_FreeMovement_SnapToHorizon;
    private readonly InputAction m_FreeMovement_LockOnCommand;
    private readonly InputAction m_FreeMovement_Dash;
    public struct FreeMovementActions
    {
        private @DefaultControls m_Wrapper;
        public FreeMovementActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @XZMovement => m_Wrapper.m_FreeMovement_XZMovement;
        public InputAction @YMovement => m_Wrapper.m_FreeMovement_YMovement;
        public InputAction @CameraStick => m_Wrapper.m_FreeMovement_CameraStick;
        public InputAction @SnapToHorizon => m_Wrapper.m_FreeMovement_SnapToHorizon;
        public InputAction @LockOnCommand => m_Wrapper.m_FreeMovement_LockOnCommand;
        public InputAction @Dash => m_Wrapper.m_FreeMovement_Dash;
        public InputActionMap Get() { return m_Wrapper.m_FreeMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FreeMovementActions set) { return set.Get(); }
        public void AddCallbacks(IFreeMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_FreeMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_FreeMovementActionsCallbackInterfaces.Add(instance);
            @XZMovement.started += instance.OnXZMovement;
            @XZMovement.performed += instance.OnXZMovement;
            @XZMovement.canceled += instance.OnXZMovement;
            @YMovement.started += instance.OnYMovement;
            @YMovement.performed += instance.OnYMovement;
            @YMovement.canceled += instance.OnYMovement;
            @CameraStick.started += instance.OnCameraStick;
            @CameraStick.performed += instance.OnCameraStick;
            @CameraStick.canceled += instance.OnCameraStick;
            @SnapToHorizon.started += instance.OnSnapToHorizon;
            @SnapToHorizon.performed += instance.OnSnapToHorizon;
            @SnapToHorizon.canceled += instance.OnSnapToHorizon;
            @LockOnCommand.started += instance.OnLockOnCommand;
            @LockOnCommand.performed += instance.OnLockOnCommand;
            @LockOnCommand.canceled += instance.OnLockOnCommand;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
        }

        private void UnregisterCallbacks(IFreeMovementActions instance)
        {
            @XZMovement.started -= instance.OnXZMovement;
            @XZMovement.performed -= instance.OnXZMovement;
            @XZMovement.canceled -= instance.OnXZMovement;
            @YMovement.started -= instance.OnYMovement;
            @YMovement.performed -= instance.OnYMovement;
            @YMovement.canceled -= instance.OnYMovement;
            @CameraStick.started -= instance.OnCameraStick;
            @CameraStick.performed -= instance.OnCameraStick;
            @CameraStick.canceled -= instance.OnCameraStick;
            @SnapToHorizon.started -= instance.OnSnapToHorizon;
            @SnapToHorizon.performed -= instance.OnSnapToHorizon;
            @SnapToHorizon.canceled -= instance.OnSnapToHorizon;
            @LockOnCommand.started -= instance.OnLockOnCommand;
            @LockOnCommand.performed -= instance.OnLockOnCommand;
            @LockOnCommand.canceled -= instance.OnLockOnCommand;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
        }

        public void RemoveCallbacks(IFreeMovementActions instance)
        {
            if (m_Wrapper.m_FreeMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IFreeMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_FreeMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_FreeMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public FreeMovementActions @FreeMovement => new FreeMovementActions(this);
    public interface IFreeMovementActions
    {
        void OnXZMovement(InputAction.CallbackContext context);
        void OnYMovement(InputAction.CallbackContext context);
        void OnCameraStick(InputAction.CallbackContext context);
        void OnSnapToHorizon(InputAction.CallbackContext context);
        void OnLockOnCommand(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
