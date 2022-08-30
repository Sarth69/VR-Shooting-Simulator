using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Pull : XRBaseInteractable
{
    [Header("Pull Data")]
    public Transform minPull;
    public Transform maxPull;
    public float minPullToShoot = 0.5f;

    public XRSocketInteractor arrowSocket;

    private Vector3 pullDirection;
    private float pullStrenght;
    private bool pulling = false;
    public class PullEvent : UnityEvent<Vector3> { }
    public static PullEvent Pulled = new PullEvent();

    public class ShootEvent : UnityEvent<float> { }
    public static ShootEvent Shooted = new ShootEvent();

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic) {
            if (isSelected && arrowSocket.hasSelection)
            {
                // The player is pulling the rope
                if (!pulling)
                {
                    pulling = true;
                }
                HandlePull(interactorsSelecting[0]);
            }
            if (!isSelected && pulling)
            {
                // The player released the trigger
                HandleShoot();
            }
        }
    }

    private void HandlePull(IXRSelectInteractor interactor)
    {
        // Compute how the rope is pulled
        Vector3 ropeDirection = minPull.position - maxPull.position;
        Vector3 interactorPosition = interactor.transform.position;
        float dot = Vector3.Dot(minPull.position - interactorPosition, ropeDirection.normalized);
        if (dot < 0)
        {
            pullDirection = new Vector3(0, 0, 0);
        } else if(dot > ropeDirection.magnitude)
        {
            pullDirection = ropeDirection;
        } else
        {
            pullDirection = dot * ropeDirection.normalized;
        }
        pullStrenght = CalculatePullStrenght();

        // Adapt the visual of the rope
        Pulled.Invoke(pullDirection);
    }

    private void HandleShoot()
    {
        Shooted.Invoke((minPullToShoot < pullStrenght) ? pullStrenght : 0);
        pullDirection = new Vector3(0, 0, 0);
        pullStrenght = 0;
        pulling = false;
    }

    private float CalculatePullStrenght()
    {
        // Returns a float between 0 and 1 depending on how much the string is pulled
        float maxStrenght = (minPull.position - maxPull.position).magnitude;
        float currentMagnitude = pullDirection.magnitude;
        if (currentMagnitude > maxStrenght)
        {
            return 1;
        } else
        {
            return currentMagnitude/maxStrenght;
        }
    }

    
}
