using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractionManager : XRInteractionManager
{
    public void ForceExit(XRBaseInteractor interactor)
    {
        if (interactor.firstInteractableSelected != null)
        {
            SelectExit(interactor, interactor.firstInteractableSelected);
        }
    }
}
