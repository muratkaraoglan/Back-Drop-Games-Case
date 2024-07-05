using UnityEngine;

public class FirstPersonController : PlayerBaseController
{

    protected override void CameraRotation()
    {
        //if there is and input
        if (InputReader.Instance.LookInput.sqrMagnitude >= _threshhold)
        {
            _cinemachineTargetPitch += InputReader.Instance.LookInput.y * RotationSpeed;
            _rotationVelocity = InputReader.Instance.LookInput.x * RotationSpeed;

            _cinemachineTargetPitch = Utils.ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0f, 0f);

            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }

    protected override void JumpAndGravity()
    {
        if (Grounded)
        {
            if (_verticalVelocity < .1f)
            {
                _verticalVelocity = -2f;
            }

            //Jump
            if (InputReader.Instance.JumpInput && _jumpTimeoutDelta <= 0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            if (_jumpTimeoutDelta >= 0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            //InputReader.Instance.JumpInput = false;
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    protected override void Move()
    {
        float targetSpeed = MoveSpeed;

        if (InputReader.Instance.MoveInput == Vector2.zero) targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;

        float speedOffset = .1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);

            _speed = Mathf.Round(_speed * 1000) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        //normalise input direction
        Vector3 inputDirection = new Vector3(InputReader.Instance.MoveInput.x, 0f, InputReader.Instance.MoveInput.y).normalized;

        if (InputReader.Instance.MoveInput != Vector2.zero)
        {
            inputDirection = transform.right * InputReader.Instance.MoveInput.x + transform.forward * InputReader.Instance.MoveInput.y;
        }

        _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }


}
