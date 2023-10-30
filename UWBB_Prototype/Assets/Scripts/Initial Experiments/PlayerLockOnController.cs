using System;
using UnityEngine;

public class PlayerLockOnController : MonoBehaviour
{
    [SerializeField] private MovementInputController inputController;
    
    public Transform target;
    [SerializeField] private Transform cameraTransform;

    [HideInInspector] public bool lockedOn;

    [Tooltip("Tolerance of the angle between camera and target for lock on to work. Higher value => more tolerant. \n \n" +
             "A value of 0 corresponds to a tolerance of 0°. \nA value of 1 corresponds to a tolerance of 180°.")]
    [SerializeField, Range(.1f, 1)] private float lockOnAngleTolerance;

    public Action onLockOn;
    
    private float maxLockOnDotProduct => 1 - lockOnAngleTolerance;

    private void Update()
    {
        if (inputController.inputState.lockOnToggleCommand)
        {
            if (lockedOn)
                ReleaseLockOn();
            else
                TryLockOn();
            
            inputController.inputState.lockOnToggleCommand = false;
        }
    }

    private void ReleaseLockOn()
    {
        lockedOn = false;
    }

    private void TryLockOn()
    {
        if (target == null)
            return;

        Vector3 directionToTarget = (target.position - cameraTransform.position).normalized;
        float camTargetDotProduct = Vector3.Dot(cameraTransform.forward, directionToTarget);
        lockedOn = camTargetDotProduct >= maxLockOnDotProduct;
        
        if (lockedOn)
            onLockOn?.Invoke();
    }
}
