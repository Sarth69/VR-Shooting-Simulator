using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractionManager : XRInteractionManager
{
    public void ForceExit(XRBaseInteractor interactor)
    {
        SelectExit(interactor, interactor.firstInteractableSelected);
    }
}
