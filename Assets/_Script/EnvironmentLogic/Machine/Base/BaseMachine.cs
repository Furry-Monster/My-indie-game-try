using UnityEngine;

public class BaseMachine : MonoBehaviour
{
    public void SetMachineParent(IMachineParent newParent)
    {
        if (newParent != null)
        {
            transform.parent = newParent.GetHeldPoint();

            //adjust the transform
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            //if the new parent is null, it's a bug,so destroy the machine
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        
        Destroy(gameObject);
    }
}
