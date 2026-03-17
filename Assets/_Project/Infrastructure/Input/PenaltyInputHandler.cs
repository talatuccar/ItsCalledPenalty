using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PenaltyInputHandler : MonoBehaviour
{
    private GameInput _controls;
    public event Action OnShootPerformed;

    private void Awake()
    {
        _controls = new GameInput();
    }

    private void OnEnable()
    {
        _controls.Gameplay.Shoot.performed += HandleShoot;
        _controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controls.Gameplay.Shoot.performed -= HandleShoot;
        _controls.Gameplay.Disable();
    }

    private void HandleShoot(InputAction.CallbackContext context)
    {
        OnShootPerformed?.Invoke();
    }
}