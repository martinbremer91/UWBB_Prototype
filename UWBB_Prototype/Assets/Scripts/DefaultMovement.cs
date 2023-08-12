//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/DefaultMovement.inputactions
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

public partial class @DefaultMovement: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultMovement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultMovement"",
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
                    ""name"": ""Y Movement and Rotation"",
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
                    ""action"": ""Y Movement and Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4f8f615-2afc-44a9-8234-3c047c4047e7"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SnapToHorizon"",
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
        m_FreeMovement_YMovementandRotation = m_FreeMovement.FindAction("Y Movement and Rotation", throwIfNotFound: true);
        m_FreeMovement_SnapToHorizon = m_FreeMovement.FindAction("SnapToHorizon", throwIfNotFound: true);
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
    private readonly InputAction m_FreeMovement_YMovementandRotation;
    private readonly InputAction m_FreeMovement_SnapToHorizon;
    public struct FreeMovementActions
    {
        private @DefaultMovement m_Wrapper;
        public FreeMovementActions(@DefaultMovement wrapper) { m_Wrapper = wrapper; }
        public InputAction @XZMovement => m_Wrapper.m_FreeMovement_XZMovement;
        public InputAction @YMovementandRotation => m_Wrapper.m_FreeMovement_YMovementandRotation;
        public InputAction @SnapToHorizon => m_Wrapper.m_FreeMovement_SnapToHorizon;
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
            @YMovementandRotation.started += instance.OnYMovementandRotation;
            @YMovementandRotation.performed += instance.OnYMovementandRotation;
            @YMovementandRotation.canceled += instance.OnYMovementandRotation;
            @SnapToHorizon.started += instance.OnSnapToHorizon;
            @SnapToHorizon.performed += instance.OnSnapToHorizon;
            @SnapToHorizon.canceled += instance.OnSnapToHorizon;
        }

        private void UnregisterCallbacks(IFreeMovementActions instance)
        {
            @XZMovement.started -= instance.OnXZMovement;
            @XZMovement.performed -= instance.OnXZMovement;
            @XZMovement.canceled -= instance.OnXZMovement;
            @YMovementandRotation.started -= instance.OnYMovementandRotation;
            @YMovementandRotation.performed -= instance.OnYMovementandRotation;
            @YMovementandRotation.canceled -= instance.OnYMovementandRotation;
            @SnapToHorizon.started -= instance.OnSnapToHorizon;
            @SnapToHorizon.performed -= instance.OnSnapToHorizon;
            @SnapToHorizon.canceled -= instance.OnSnapToHorizon;
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
        void OnYMovementandRotation(InputAction.CallbackContext context);
        void OnSnapToHorizon(InputAction.CallbackContext context);
    }
}
