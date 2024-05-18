using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    public Vector2 MoveInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool InteractionInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _attackAction;
    private InputAction _interactionAction;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }


        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAction();
    }

    private void SetupInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _dashAction = _playerInput.actions["Dash"];
        _interactionAction = _playerInput.actions["Interaction"];
        _attackAction = _playerInput.actions["Attack"];
    }

    private void UpdateAction()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        AttackInput = _attackAction.WasPressedThisFrame();
        DashInput = _dashAction.WasPressedThisFrame();
        InteractionInput = _interactionAction.WasPressedThisFrame();
    }
}
