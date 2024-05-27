public class NormalPlate : BasePlate
{
    private void Update()
    {
        CheckPlate();
    }

    private void CheckPlate()
    {
        if (GetHeldPoint().childCount > 0)
        {
            SetHeldMachine(GetHeldPoint().GetChild(0).GetComponent<BaseMachine>());
        }
        else
        {
            SetHeldMachine(null);
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (HasHeldMachine())
        {
            //if plate is occupied...
            if (Player.instance.HasHeldMachine())
            {
                //...and if player is holding sth...
                //...then do nothing
            }
            else
            {
                //..and if player hold nothing...
                //...then pick the machine up from plate
                GetHeldMachine().SetMachineParent(Player.instance);
            }
        }
        else
        {
            //if plate is empty
            if (Player.instance.HasHeldMachine())
            {
                //...and if player held sth...
                //...then put it on plate
                Player.instance.GetHeldMachine().SetMachineParent(this);
            }
            else
            {
                //...and if player held nothing...
                //then nothing would happend
            }
        }
    }

    public override void InteractAlt()
    {
        base.InteractAlt();

        //nothing would happend
    }
}
