using UnityEngine;

public class BaseMachine : MonoBehaviour,IMachine
{
    private IMachineParent currentParent;




    #region ParentMethods
    public void SetMachineParent(IMachineParent newParent)
    {
        if (newParent != null)
        {
            //adjust the transform
            transform.parent = newParent.GetHeldPoint();
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            //reset the reference in other scripts
            currentParent?.SetHeldMachine(null);
            newParent.SetHeldMachine(this);

            //reset the reference
            currentParent = newParent;
        }
    }

    public IMachineParent GetCurrentParent()
    {
        return currentParent;
    }
    #endregion
}
