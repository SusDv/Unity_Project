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

    private PlayerMovementStateMachine movementStateMachine;
    

    private void Awake()
    {
        movementStateMachine = new PlayerMovementStateMachine(this);

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
        movementStateMachine.ChangeState(movementStateMachine.IdleState);
    }

    private void Update()
    {
        movementStateMachine.OnUpdate();
    }
    private void FixedUpdate()
    {
        movementStateMachine.OnFixedUpdate();
    }
}