using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 4f;
    [Tooltip("Rotation speed of the character")]
    public float RotationSpeed = 1f;
    [Tooltip("Acceleration and decelaration")]
    public float SpeedChangeRate = 10f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("Gravity")]
    public float Gravity = -15f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.1f;

    [Header("Player Ground")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the ground check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.26f;
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    [Tooltip("Follow Target")]
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 90f;
    public float BottomClamp = -90f;

    protected float _cinemachineTargetYaw;
    protected float _cinemachineTargetPitch;

    protected float _speed;
    protected float _targetRotation = 0.0f;
    protected float _rotationVelocity;
    protected float _verticalVelocity;
    protected float _terminalVelocity = 53.0f;

    protected float _jumpTimeoutDelta;

    protected CharacterController _controller;


    protected const float _threshhold = 0.01f;

    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();
        _jumpTimeoutDelta = JumpTimeout;
    }

    protected virtual void Update()
    {
        JumpAndGravity();
        GroundCheck();
        Move();
    }

    protected void LateUpdate()
    {
        CameraRotation();
    }


    protected virtual void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    protected abstract void CameraRotation();
    protected abstract void Move();
    protected abstract void JumpAndGravity();


    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
}
