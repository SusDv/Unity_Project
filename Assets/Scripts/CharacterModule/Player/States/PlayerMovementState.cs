using CharacterModule.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachineBase StateMachineBase;

    protected PlayerGroundedData movementData;

    protected Vector3 playerInput;

    protected Vector3 playerVelocity, playerDesiredVelocity;

    protected CapsuleColliderUtility colliderUtility;

    protected PhysicsUtility physicsUtility;

    protected float distanceToGround;


    public PlayerMovementState(PlayerMovementStateMachineBase playerMovementStateMachineBase) 
    {
        StateMachineBase = playerMovementStateMachineBase;

        movementData = StateMachineBase.PlayerMovementController.Data.GroundedData;

        colliderUtility = StateMachineBase.PlayerMovementController.ColliderUtility;

        physicsUtility = StateMachineBase.PlayerMovementController.PhysicsUtility;

        InitializeData();
    }

    private void InitializeData() 
    {
        StateMachineBase.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
    }

    public virtual void OnEnter()
    {
        Debug.Log($"State: {this}");
        AddInputActionCallbacks();
    }

    public virtual void OnExit()
    {
        RemoveInputActionCallbacks();
    }

    public virtual void OnFixedUpdate()
    {
        Move();
        RotateTowardsTargetRotation();
    }

    public virtual void OnUpdate()
    {
        ReadInput();
        playerInput = GetPlayerInput();
        playerInput = Vector3.ClampMagnitude(playerInput, 1f);

        playerDesiredVelocity = GetCameraRelativeVelocity() * GetMovementModifier();
    }

    private void ReadInput() 
    {
        StateMachineBase.ReusableData.MovementInput = StateMachineBase.PlayerMovementController.PlayerInput.InputActions.Player.Move.ReadValue<Vector2>();
    }
    private void Move()
    {
        StateMachineBase.PlayerMovementController.Rigidbody.AddForce(-GetPlayerHorizontalVelocity(), ForceMode.VelocityChange);
        if (playerInput == Vector3.zero && StateMachineBase.ReusableData.MovementSpeedModifier == 0f) 
        {
            return;
        }
        playerVelocity = playerInput;

        playerVelocity = GetNormalRelativeVelocity();
        
        RotatePlayerTowardsInputDirection(playerInput);
        CheckStairs();
        //playerVelocity = SteepSlopeHandle();

        StateMachineBase.PlayerMovementController.Rigidbody.AddForce(playerVelocity, ForceMode.VelocityChange);
    }

    private void RotatePlayerTowardsInputDirection(Vector3 direction)
    {
        UpdateTargetRotation(direction);
        RotateTowardsTargetRotation();
    }
    private static float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }
    private float AddCameraRotationToAngle(float angle)
    {
        angle += StateMachineBase.PlayerMovementController.CameraFollowPoint.eulerAngles.y;

        if (angle > 0)
        {
            angle -= 360f;
        }

        return angle;
    }
    private void UpdateTargetRotationData(float targetAngle)
    {
        StateMachineBase.ReusableData.CurrentTargetRotation.y = targetAngle;

        StateMachineBase.ReusableData.DampedTargetRotationTimePassed.y = 0f;
    }

    

    private Vector3 GetNormalRelativeVelocity() 
    {
        Vector3 currentVelocity = playerVelocity;

        float groundAngle = Vector3.Angle(Vector3.up, movementData.GroundHit.normal);

        Vector3 rightDirection = ProjectOnContactPlane(Vector3.right).normalized;
        Vector3 forwardDirection = ProjectOnContactPlane(Vector3.forward).normalized;

        float currentVelocityX = Vector3.Dot(currentVelocity, rightDirection);
        float currentVelocityZ = Vector3.Dot(currentVelocity, forwardDirection);

        float maxSpeedChange = GetMovementModifier();

        float newVelocityX = Mathf.MoveTowards(currentVelocityX, playerDesiredVelocity.x, maxSpeedChange);
        float newVelocityZ = Mathf.MoveTowards(currentVelocityZ, playerDesiredVelocity.z, maxSpeedChange);

        currentVelocity += (newVelocityX - currentVelocityX) * rightDirection + (newVelocityZ - currentVelocityZ) * forwardDirection;

        return currentVelocity;
    }

    private void CheckStairs() 
    {
        RaycastHit stairsHit;
        float sphereCastRadius = colliderUtility.DefaultColliderData.Radius;

        Physics.SphereCast(StateMachineBase.PlayerMovementController.Rigidbody.transform.position, sphereCastRadius, playerDesiredVelocity, out stairsHit, colliderUtility.DefaultColliderData.SkinWidth * 2f);

        
        if (stairsHit.collider) 
        {
            //Debug.Log(stairsHit.point);
            Debug.DrawRay(stairsHit.point, Vector3.up, Color.red);
        }
    }

    private Vector3 SteepSlopeHandle() 
    {
        Vector3 currentVelocity = playerVelocity;
        RaycastHit capsuleCastHit;
        float capsuleCastRadius = colliderUtility.CapsuleColliderData.Collider.radius;
        Vector3 startSpherePosition = StateMachineBase.PlayerMovementController.Rigidbody.transform.position + Vector3.up * colliderUtility.DefaultColliderData.CenterY + Vector3.up * -colliderUtility.DefaultColliderData.Height * 0.5f;
        Vector3 endSpherePosition = startSpherePosition + Vector3.up * colliderUtility.DefaultColliderData.Height * 0.5f;
        
        if (Physics.CapsuleCast(startSpherePosition, endSpherePosition, capsuleCastRadius, playerDesiredVelocity, out capsuleCastHit, colliderUtility.DefaultColliderData.SkinWidth * 2f, LayerMask.GetMask("Default"))) 
        {
            float hitNormalAngle = Mathf.RoundToInt(Vector3.Angle(Vector3.up, capsuleCastHit.normal));
            
            if (hitNormalAngle <= 90f && hitNormalAngle > physicsUtility.SlopeData.MaximumSlopeAngle) 
            {
                Vector3 scaledNormal = Vector3.Scale(Vector3.Normalize(capsuleCastHit.normal), new Vector3(1, 0, 1));
                currentVelocity = Vector3.ProjectOnPlane(playerVelocity, scaledNormal);
            }
        }
        return currentVelocity;
    }

    protected Vector3 GetPlayerInput()
    {
        return new Vector3(StateMachineBase.ReusableData.MovementInput.x, 0.0f, StateMachineBase.ReusableData.MovementInput.y);
    }
    protected float GetMovementModifier()
    {
        return movementData.BaseSpeed * StateMachineBase.ReusableData.MovementSpeedModifier;
    }
    protected Vector3 GetModifiedVelocity() 
    {
        float movementSpeed = GetMovementModifier();
        return new Vector3(playerInput.x * movementSpeed, playerVelocity.y, playerInput.z * movementSpeed);
    }
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 currentHorizontalVelocity = StateMachineBase.PlayerMovementController.Rigidbody.velocity;

        currentHorizontalVelocity.y = 0f;

        return currentHorizontalVelocity;
    }
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0.0f, StateMachineBase.PlayerMovementController.Rigidbody.velocity.y, 0.0f);
    }
    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = StateMachineBase.PlayerMovementController.Rigidbody.rotation.eulerAngles.y;

        if (currentYAngle == StateMachineBase.ReusableData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, StateMachineBase.ReusableData.CurrentTargetRotation.y, ref StateMachineBase.ReusableData.DampedTargetRotationCurrentVelocity.y, StateMachineBase.ReusableData.TimeToReachTargetRotation.y - StateMachineBase.ReusableData.DampedTargetRotationTimePassed.y);

        StateMachineBase.ReusableData.DampedTargetRotationTimePassed.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0.0f, smoothedYAngle, 0.0f);

        StateMachineBase.PlayerMovementController.Rigidbody.MoveRotation(targetRotation);
    }
    protected void UpdateTargetRotation(Vector3 direction, bool considerCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);
        if (considerCameraRotation) 
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }

        if (directionAngle != StateMachineBase.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }
    }
    protected Vector3 GetCameraRelativeVelocity()
    {
        Vector3 currentVelocity = playerInput;

        Vector3 cameraForwardDirection = StateMachineBase.PlayerMovementController.CameraFollowPoint.transform.forward;
        Vector3 cameraRightDirection = StateMachineBase.PlayerMovementController.CameraFollowPoint.transform.right;
        
        cameraForwardDirection.y = cameraRightDirection.y = 0;

        cameraForwardDirection.Normalize();
        cameraRightDirection.Normalize();

        currentVelocity = cameraForwardDirection * currentVelocity.z + cameraRightDirection * currentVelocity.x;

        return currentVelocity;
    }

    protected Vector3 ProjectOnContactPlane(Vector3 vector) 
    {
        return vector - movementData.GroundHit.normal * Vector3.Dot(vector, movementData.GroundHit.normal);
    }

    protected void ResetVelocity() 
    {
        StateMachineBase.PlayerMovementController.Rigidbody.velocity = Vector3.zero;
    }
    protected virtual void AddInputActionCallbacks()
    {
        StateMachineBase.PlayerMovementController.PlayerInput.PlayerActions.Walk.started += OnWalkStarted;
        StateMachineBase.PlayerMovementController.PlayerInput.PlayerActions.Run.started += OnSprintStarted;
    }
    protected virtual void RemoveInputActionCallbacks()
    {
        StateMachineBase.PlayerMovementController.PlayerInput.PlayerActions.Walk.started -= OnWalkStarted;
        StateMachineBase.PlayerMovementController.PlayerInput.PlayerActions.Run.started -= OnSprintStarted;
    }

    public virtual void OnWalkStarted(InputAction.CallbackContext context)
    {
        StateMachineBase.ReusableData.IsWalking = !StateMachineBase.ReusableData.IsWalking;
    }

    public virtual void OnSprintStarted(InputAction.CallbackContext context) 
    {
        StateMachineBase.ReusableData.IsSprinting = !StateMachineBase.ReusableData.IsSprinting;
    }
}
