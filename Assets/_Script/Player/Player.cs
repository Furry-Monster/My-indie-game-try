using System;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : MonoBehaviour,IMachineParent
{
    //Singleton
    public static Player instance;

    //components and references
    [Header("Components")]
    [SerializeField] private InputManager input;
    [SerializeField] private Rigidbody rb;

    //parameters
    [Header("Movement and Rotation")]
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 8f;
    [SerializeField] private float rotateSpeed = 100f;

    [Header("Interaction")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private Transform heldPoint;

    //lerp variables
    private float targetMoveSpeed;
    private float currentMoveSpeed;
    private Quaternion targetRotation;
    private Quaternion currentRotation;

    //variables
    private Vector3 moveDir;
    private BasePlate selectedPlate;
    private BaseMachine heldMachine;

    //events and args
    public event EventHandler<OnSelectedPlateChangedArgs> OnSelectPlateChanged;
    public class OnSelectedPlateChangedArgs : EventArgs
    {
        public BasePlate newSelectedPlate;
        public OnSelectedPlateChangedArgs(BasePlate inputSelectedPlate)
        {
            newSelectedPlate = inputSelectedPlate;
        }
    }


    //methods
    private void Awake()
    {
        //Singleton check
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There couldn't be more than one player in the scene");
            Destroy(gameObject);
        }

        //component check
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (input == null)
        {
            input = GetComponent<InputManager>();
        }
    }

    private void Start()
    {
        input.OnInteract += Interact;
    }

    private void Update()
    {
        AutoSelectPlate();
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    #region MainMethods
    private void MoveAndRotate()
    {
        Vector2 moveInput = input.moveInput;
        moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (input.moveInput == Vector2.zero)
        {
            targetMoveSpeed = 0f;
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, deceleration * Time.deltaTime);
        }
        else
        {
            
            targetMoveSpeed = maxMoveSpeed;
            targetRotation = Quaternion.LookRotation(moveDir);

            //lerp for speed and rotation
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, acceleration * Time.deltaTime);
            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        rb.velocity = moveDir * currentMoveSpeed;
        transform.rotation = currentRotation;
    }

    private void AutoSelectPlate()
    {
        BasePlate newSelectedPlate;

        if(Physics.Raycast(transform.position,transform.forward,out RaycastHit hitInfo, interactDistance, interactLayer))
        {
            if(hitInfo.collider.TryGetComponent(out newSelectedPlate))
            {
                selectedPlate = newSelectedPlate;
            }
            else
            {
                selectedPlate = null;
            }
        }
        else
        {
            selectedPlate = null;
        }

        OnSelectPlateChanged?.Invoke(this, new OnSelectedPlateChangedArgs(selectedPlate));
    }

    private void Interact(object sender, EventArgs e)
    {
        selectedPlate?.Interact();
    }




    #endregion


    #region MachineParentMethods
    public Transform GetHeldPoint()
    {
        return heldPoint;
    }

    public BaseMachine GetHeldMachine()
    {
        return heldMachine;
    }

    public bool HasHeldMachine()
    {
        return heldMachine != null;
    }

    public void SetHeldMachine(BaseMachine newHeldMachine)
    {
        heldMachine = newHeldMachine;
    }


    #endregion
}
