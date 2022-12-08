using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityTutorial.Manager;

namespace UnityTutorial.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        [SerializeField, Range(10, 500)] private float JumpFactor = 300f;
        [SerializeField] private float AirResistance = 0.8f;
        // [SerializeField] private bool _yInversion;
        
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private CapsuleCollider _collider;
        private bool _hasAnimator;
        private bool _grounded;
        private int _xVelHash;
        private int _yVelHash;
        private int _zVelHash;
        private int _jumpHash;
        private int _groundHash;
        private int _fallingHash;
        private int _crouchHash;
        private float _xRotation;
        private float maxSlope = 60f;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;

        private Vector2 _currentVelocity;
        
        void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();
            _collider = GetComponent<CapsuleCollider>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _zVelHash = Animator.StringToHash("Z_Velocity");
            _jumpHash = Animator.StringToHash("Jump");
            _groundHash = Animator.StringToHash("Grounded");
            _fallingHash = Animator.StringToHash("Falling");
            _crouchHash = Animator.StringToHash("Crouch");
        }

        private void FixedUpdate()
        {
            Move();
            HandleJump();
            HandleCrouch();
        }

        private void LateUpdate()
        {
            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator)
            {
                return;
            }

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            var colliderCenter = _collider.center;
            _collider.height = 1.79f;
            colliderCenter.y = 0.89f;
            if (_inputManager.Crouch)
            {
                targetSpeed = 1.5f;
                _collider.height = 1.39f;
                colliderCenter.y = 0.76f;
            }
            if (_inputManager.Move == Vector2.zero)
            {
                targetSpeed = 0f;
            }

            if (_grounded)
            {
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

                var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
                var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;
            
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);
            }
            else
            {
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(_currentVelocity.x * AirResistance,0,_currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }
            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void CamMovements()
        {
            if (!_hasAnimator)
            {
                return;
            }

            if (!PauseMenu.GameIsPaused)
            {
                var Mouse_X = _inputManager.Look.x;
                var Mouse_Y = _inputManager.Look.y;
                Camera.position = CameraRoot.position;

                if (SettingsMenu.instance != null)
                {
                    Debug.Log("SettingsMenu.instance " + SettingsMenu.instance);
                    if(SettingsMenu.instance._yInversion)
                    {
                        Mouse_Y *= -1;
                    }
                }

                _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
                _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);
            
                Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
                _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
            }
        }

        private void HandleJump()
        {
            if (!_hasAnimator)
            {
                return;
            }
            if (!_inputManager.Jump)
            {
                return;
            }
            if (!_grounded)
            {
                return;
            }
            _animator.SetTrigger(_jumpHash);
            _playerRigidbody.AddForce(-_playerRigidbody.velocity.y * Vector3.up, ForceMode.VelocityChange);
            _playerRigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            _animator.ResetTrigger(_jumpHash);

        }

        private void HandleCrouch() => _animator.SetBool(_crouchHash, _inputManager.Crouch);

        private void OnCollisionStay(Collision collisionInfo)
        {
            foreach(ContactPoint contact in collisionInfo.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
                {
                    _grounded = true;
                    SetAnimationGrounding();
                }
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            _grounded = false;
            _animator.SetFloat(_zVelHash, _playerRigidbody.velocity.y);
            SetAnimationGrounding();
        }

        private void SetAnimationGrounding()
        {
            _animator.SetBool(_fallingHash, !_grounded);
            _animator.SetBool(_groundHash, _grounded);
        }
    }
}
