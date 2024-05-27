using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMachine
{
    public void SetMachineParent(IMachineParent newMachineParent);

    public IMachineParent GetCurrentParent();
}
