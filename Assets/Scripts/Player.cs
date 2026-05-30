using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClimber : MonoBehaviour
{
    [SerializeField] private InputActionReference climbAction;

    private Climber climber;

    private void Awake()
    {
        climber = GetComponent<Climber>();
    }

    private void OnEnable()
    {
        climbAction.action.Enable();
        climbAction.action.performed += OnClimb;
    }

    private void OnDisable()
    {
        climbAction.action.performed -= OnClimb;
        climbAction.action.Disable();
    }

    private void OnClimb(InputAction.CallbackContext ctx)
    {
        climber.TryClimb();
    }
}
