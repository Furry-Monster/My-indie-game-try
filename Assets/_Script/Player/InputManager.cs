using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Input System Reference
    private InputSystem inputActions;

    //Input Cache
    private Vector2 _moveInputCache;

    //Input Events
    public event EventHandler OnInteract;

    //Input Properties
    public Vector2 moveInput
    {
        get
        {
            return _moveInputCache;
        }
    }

    //Methods
    private void Awake()
    {
        //Initialize Input System
        if(inputActions == null)
        {
            inputActions = new InputSystem();
        }

        //Enable Input System for Player
        inputActions.Player.Enable();
    }

    private void Start()
    {
        //Subscribe to Input Events
        inputActions.Player.Interact.performed += ctx => OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        //Read form Input System
        _moveInputCache = inputActions.Player.Move.ReadValue<Vector2>();
    }
}
