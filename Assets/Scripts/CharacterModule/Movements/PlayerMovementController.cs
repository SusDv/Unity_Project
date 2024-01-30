using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(CapsuleCollider))]
public class PlayerMovementController : MonoBehaviour
{
    [field: SerializeField]
    public PlayerMovementSO Data { get; private set; }

    [field: SerializeField]
    public CapsuleColliderUtility ColliderUtility { get; private set; }

    [field: SerializeField]
    public PhysicsUtility PhysicsUtility { get; private set; }

    [field: SerializeField]
    public Animator Animator { get; private set; }
    public Transform CameraFollowPoint { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput PlayerInput { get; private set; }

    private PlayerMovementStateMachineBase _movementStateMachineBase;
    

    private void Awake()
    {
        _movementStateMachineBase = new PlayerMovementStateMachineBase(this);

        CameraFollowPoint = Camera.main.transform;

        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();

        PhysicsUtility.Initialize();

        PlayerInput = GetComponent<PlayerInput>();
        Rigidbody = GetComponent<Rigidbody>();
    }
    private void OnValidate()
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }
    private void Start()
    {
        _movementStateMachineBase.ChangeState(_movementStateMachineBase.IdleState);
    }

    private void Update()
    {
        _movementStateMachineBase.OnUpdate();
    }
    private void FixedUpdate()
    {
        _movementStateMachineBase.OnFixedUpdate();
    }
}