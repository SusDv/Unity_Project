using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{

    
    public PlayerGroundedState(PlayerMovementStateMachineBase playerMovementStateMachineBase) : base(playerMovementStateMachineBase)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnFixedUpdate()
    {
        physicsUtility.GroundCheckData.OnGround = PlayerIsGrounded();
        base.OnFixedUpdate();
        ApplyPlayerGravity();
        PlayerFloat();
    }

    private bool PlayerIsGrounded()
    {
        float sphereCastRadius = colliderUtility.CapsuleColliderData.Collider.radius * 0.9f;
        float sphereCastLength = colliderUtility.ColliderLocalHalfHeight + colliderUtility.FloatHeight + physicsUtility.GroundCheckData.GroundCheckBuffer;

        Vector3 sphereCastOrigin = colliderUtility.CapsuleColliderData.Collider.bounds.center;

        bool didHit = Physics.SphereCast(sphereCastOrigin, sphereCastRadius, Vector3.down, out movementData.GroundHit, sphereCastLength, LayerMask.GetMask("Default"));
        if (!didHit)
        {
            return false;
        }
        float sphereCastHitDistance = movementData.GroundHit.distance + sphereCastRadius;
        distanceToGround = sphereCastHitDistance - colliderUtility.ColliderLocalHalfHeight;

        return distanceToGround <= physicsUtility.GroundCheckData.GroundCheckTolerance;
    }
    private void PlayerFloat()
    {
        if (physicsUtility.GroundCheckData.OnGround)
        {
            Vector3 currentVelocity = StateMachineBase.PlayerMovementController.Rigidbody.velocity;
            Vector3 forceDirection = StateMachineBase.PlayerMovementController.transform.TransformDirection(Vector3.down);
            float dotVelocity = Vector3.Dot(forceDirection, currentVelocity);

            float floatForce = (distanceToGround * physicsUtility.GroundCheckData.FloatForceMultiplier) - (dotVelocity * physicsUtility.GroundCheckData.FloatVelocityMultiplier);

            StateMachineBase.PlayerMovementController.Rigidbody.AddForce(forceDirection * floatForce, ForceMode.VelocityChange);
        }
    }

    private void ApplyPlayerGravity()
    {
        if (physicsUtility.GroundCheckData.OnGround)
        {
            physicsUtility.GravityData.IsFalling = false;
            physicsUtility.GravityData.CurrentFallGravity = 0;
        }
        else
        {
            physicsUtility.GravityData.IsFalling = true;
            physicsUtility.GravityData.GravityFallIncerementTimer -= Time.fixedDeltaTime;

            if (physicsUtility.GravityData.GravityFallIncerementTimer < 0.0f)
            {
                if (physicsUtility.GravityData.CurrentFallGravity < physicsUtility.GravityData.MaximumFallGravity)
                {
                    physicsUtility.GravityData.CurrentFallGravity += physicsUtility.GravityData.GravityFallIncrementAmount * Time.fixedDeltaTime;
                }
                physicsUtility.GravityData.GravityFallIncerementTimer = physicsUtility.GravityData.GravityFallIncrementTime;
            }
            StateMachineBase.PlayerMovementController.Rigidbody.AddForce(Vector3.down * physicsUtility.GravityData.CurrentFallGravity, ForceMode.VelocityChange);
        }
    }

    protected override void AddInputActionCallbacks()
    {
        base.AddInputActionCallbacks();

        StateMachineBase.PlayerMovementController.PlayerInput.PlayerActions.Move.canceled += OnMovementCanceled;
    }

    protected override void RemoveInputActionCallbacks()
    {
        base.RemoveInputActionCallbacks();

        StateMachineBase.PlayerMovementController.PlayerInput.PlayerActions.Move.canceled -= OnMovementCanceled;
    }
    protected virtual void OnMove()
    {
        if (StateMachineBase.ReusableData.IsWalking)
        {
            StateMachineBase.ChangeState(StateMachineBase.WalkingState);
            return;
        }

        StateMachineBase.ChangeState(StateMachineBase.RunningState);
    }
    protected virtual void OnMovementCanceled(InputAction.CallbackContext obj)
    {
        StateMachineBase.ChangeState(StateMachineBase.IdleState);
    }
}
