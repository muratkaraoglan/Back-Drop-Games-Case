using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ThirdPersonController : PlayerBaseController
{
    [SerializeField, Range(0.0f, 0.3f)] private float _rotationSmoothTime = 0.12f;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        PlayerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = 4;
    }

    protected override void CameraRotation()
    {
        if (UIManager.Instance.AnyChestOpen || UIManager.Instance.IsPlayerInventoryOpen) return;
        //if there is and input
        if (InputReader.Instance.LookInput.sqrMagnitude >= _threshhold)
        {
            _cinemachineTargetYaw += InputReader.Instance.LookInput.x;
            _cinemachineTargetPitch += InputReader.Instance.LookInput.y;
        }
        // clamp our rotations  360 degrees
        _cinemachineTargetYaw = Utils.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = Utils.ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);


        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch,
            _cinemachineTargetYaw, 0.0f);

    }

    protected override void JumpAndGravity()
    {
        if (Grounded)
        {
            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (InputReader.Instance.JumpInput && _jumpTimeoutDelta <= 0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {

            _jumpTimeoutDelta = JumpTimeout;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    protected override void Move()
    {

        float targetSpeed = MoveSpeed;

        if (InputReader.Instance.MoveInput == Vector2.zero) targetSpeed = 0.0f;

        //player current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(InputReader.Instance.MoveInput.x, 0.0f, InputReader.Instance.MoveInput.y).normalized;

        if (InputReader.Instance.MoveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

                
            if (UIManager.Instance.AnyChestOpen || UIManager.Instance.IsPlayerInventoryOpen) return;
            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
}
