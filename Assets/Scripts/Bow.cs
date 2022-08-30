using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    public class BowSelectedEvent : UnityEvent<bool> { }
    public BowSelectedEvent BowSelected = new BowSelectedEvent();

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        BowSelected.Invoke(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        BowSelected.Invoke(false);
    }
}
