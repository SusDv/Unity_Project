using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    protected PlayerGroundedData movementData;

    protected Vector3 playerInput;

    protected Vector3 playerVelocity, playerDesiredVelocity;

    protected CapsuleColliderUtility colliderUtility;

    protected PhysicsUtility physicsUtility;

    protected float distanceToGround;


    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine) 
    {
        stateMachine = playerMovementStateMachine;

        movementData = stateMachine.PlayerMovementController.Data.GroundedData;

        colliderUtility = stateMachine.PlayerMovementController.ColliderUtility;

        physicsUtility = stateMachine.PlayerMovementController.PhysicsUtility;

        InitializeData();
    }

    private void InitializeData() 
    {
        stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
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
        stateMachine.ReusableData.MovementInput = stateMachine.PlayerMovementController.PlayerInput.InputActions.Player.Move.ReadValue<Vector2>();
    }
    private void Move()
    {
        stateMachine.PlayerMovementController.Rigidbody.AddForce(-GetPlayerHorizontalVelocity(), ForceMode.VelocityChange);
        if (playerInput == Vector3.zero && stateMachine.ReusableData.MovementSpeedModifier == 0f) 
        {
            return;
        }
        playerVelocity = playerInput;

        playerVelocity = GetNormalRelativeVelocity();
        
        RotatePlayerTowardsInputDirection(playerInput);
        CheckStairs();
        //playerVelocity = SteepSlopeHandle();

        stateMachine.PlayerMovementController.Rigidbody.AddForce(playerVelocity, ForceMode.VelocityChange);
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
        angle += stateMachine.PlayerMovementController.CameraFollowPoint.eulerAngles.y;

        if (angle > 0)
        {
            angle -= 360f;
        }

        return angle;
    }
    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

        stateMachine.ReusableData.DampedTargetRotationTimePassed.y = 0f;
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

        Physics.SphereCast(stateMachine.PlayerMovementController.Rigidbody.transform.position, sphereCastRadius, playerDesiredVelocity, out stairsHit, colliderUtility.DefaultColliderData.SkinWidth * 2f);

        
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
        Vector3 startSpherePosition = stateMachine.PlayerMovementController.Rigidbody.transform.position + Vector3.up * colliderUtility.DefaultColliderData.CenterY + Vector3.up * -colliderUtility.DefaultColliderData.Height * 0.5f;
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
        return new Vector3(stateMachine.ReusableData.MovementInput.x, 0.0f, stateMachine.ReusableData.MovementInput.y);
    }
    protected float GetMovementModifier()
    {
        return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;
    }
    protected Vector3 GetModifiedVelocity() 
    {
        float movementSpeed = GetMovementModifier();
        return new Vector3(playerInput.x * movementSpeed, playerVelocity.y, playerInput.z * movementSpeed);
    }
    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 currentHorizontalVelocity = stateMachine.PlayerMovementController.Rigidbody.velocity;

        currentHorizontalVelocity.y = 0f;

        return currentHorizontalVelocity;
    }
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return new Vector3(0.0f, stateMachine.PlayerMovementController.Rigidbody.velocity.y, 0.0f);
    }
    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = stateMachine.PlayerMovementController.Rigidbody.rotation.eulerAngles.y;

        if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationTimePassed.y);

        stateMachine.ReusableData.DampedTargetRotationTimePassed.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0.0f, smoothedYAngle, 0.0f);

        stateMachine.PlayerMovementController.Rigidbody.MoveRotation(targetRotation);
    }
    protected void UpdateTargetRotation(Vector3 direction, bool considerCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);
        if (considerCameraRotation) 
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }

        if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }
    }
    protected Vector3 GetCameraRelativeVelocity()
    {
        Vector3 currentVelocity = playerInput;

        Vector3 cameraForwardDirection = stateMachine.PlayerMovementController.CameraFollowPoint.transform.forward;
        Vector3 cameraRightDirection = stateMachine.PlayerMovementController.CameraFollowPoint.transform.right;
        
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
        stateMachine.PlayerMovementController.Rigidbody.velocity = Vector3.zero;
    }
    protected virtual void AddInputActionCallbacks()
    {
        stateMachine.PlayerMovementController.PlayerInput.PlayerActions.Walk.started += OnWalkStarted;
        stateMachine.PlayerMovementController.PlayerInput.PlayerActions.Run.started += OnSprintStarted;
    }
    protected virtual void RemoveInputActionCallbacks()
    {
        stateMachine.PlayerMovementController.PlayerInput.PlayerActions.Walk.started -= OnWalkStarted;
        stateMachine.PlayerMovementController.PlayerInput.PlayerActions.Run.started -= OnSprintStarted;
    }

    public virtual void OnWalkStarted(InputAction.CallbackContext context)
    {
        stateMachine.ReusableData.IsWalking = !stateMachine.ReusableData.IsWalking;
    }

    public virtual void OnSprintStarted(InputAction.CallbackContext context) 
    {
        stateMachine.ReusableData.IsSprinting = !stateMachine.ReusableData.IsSprinting;
    }
}
