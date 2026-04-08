using KBCore.Refs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 20.0f;
    [SerializeField] private float gravity = -5.0f;
    [SerializeField] private float rotationSpeed = 20.0f;
    [SerializeField] private float mouseSensY = 12.0f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;

    [SerializeField] private float mobileScale = 11f;

    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private Vector3 _velocity;
    private float _camXAxisRotation;

    private void OnValidate() => this.ValidateRefs();

    private void Start()
    {
        _move = InputSystem.actions.FindAction("Player/Move");
        _look = InputSystem.actions.FindAction("Player/Look");
        _jump = InputSystem.actions.FindAction("Player/Jump");
        _jump.started += Jump;

#if !UNITY_ANDROID
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    private void OnDisable()
    {
        _jump.started -= Jump;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        AudioController.Instance.PlayJumpSFX();
    }

    private void Update()
    {
        Vector2 readMove = _move.ReadValue<Vector2>();
        Vector2 readLook = _look.ReadValue<Vector2>();// (0,0)
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;

        _velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += _velocity;
        controller.Move(movement);

#if UNITY_ANDROID
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * mobileScale * Time.deltaTime);
        _camXAxisRotation += mouseSensY * readLook.y * Time.deltaTime * rotationSpeed * -1;
#else
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);
        _camXAxisRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
#endif
        _camXAxisRotation = Mathf.Clamp(_camXAxisRotation, -80f, 50f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(_camXAxisRotation, 0, 0);
    }

	public void ChangeMouseSensibility(float value)
	{
		mouseSensY = value;
		rotationSpeed = value;
	}
}
