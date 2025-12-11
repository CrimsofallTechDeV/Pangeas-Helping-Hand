using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class CustomSprintProvider : MonoBehaviour
{
    public InputActionProperty sprintKey;
    public float runSpeed;

    private float normalSpeed;
    private DynamicMoveProvider dynamicMoveProvider;

    private void Start()
    {
        dynamicMoveProvider = GetComponentInChildren<DynamicMoveProvider>();
        normalSpeed = dynamicMoveProvider.moveSpeed;
    }

    private void Update()
    {
        if (sprintKey.action.WasPressedThisFrame())
        {
            dynamicMoveProvider.moveSpeed = runSpeed;
        }
        else if (sprintKey.action.WasReleasedThisFrame())
        {
            dynamicMoveProvider.moveSpeed = normalSpeed;
        }
    }
}
