using UnityEngine;

public interface IMachineParent
{
    public Transform GetHeldPoint();

    public BaseMachine GetHeldMachine();

    public void SetHeldMachine(BaseMachine newHeldMachine);

    public bool HasHeldMachine();
    
}
