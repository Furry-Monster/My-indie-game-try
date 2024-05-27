using UnityEngine;

public class BasePlate : MonoBehaviour, IInteractable, IMachineParent
{
    [Header("Machine parent")]
    [SerializeField] private Transform heldPoint;
    private BaseMachine heldMachine;

    #region Interactable Methods
    public virtual void Interact()
    {
        Debug.Log("Interact with " + name);
    }

    #endregion

    #region MachineParentMethods

    public BaseMachine GetHeldMachine()
    {
        return heldMachine;
    }

    public Transform GetHeldPoint()
    {
        return heldPoint;
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
