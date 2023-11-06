using UnityEngine;

public class FirstVersionInputController : IInputController<DefaultMovement>
{
    public FirstVersionInputState FirstVersionInputState = new();
    public DefaultMovement controls { get; set; }
    
    public void Init()
    {
        
    }
}

public struct FirstVersionInputState
{
    public Vector2 characterPlaneInput;
    public Vector2 characterAxisInput;

    public bool snapCommand;
    public bool lockOnToggleCommand;
}