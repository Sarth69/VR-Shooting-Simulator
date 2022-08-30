using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BowSocket : XRSocketInteractor
{
    /*public PullMeasurer PullMeasurer { get; private set; } = null;*/
    public bool IsReady { get; private set; } = false;

    public GameObject rope;
    public GameObject activeRope;

    private Pull pullManager;
    private Bow bow;
    public CustomInteractionManager customInteractionManager;

    protected override void Start()
    {
        base.Start();

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Listeners for the events about the pull and the grabbing of the bow
        pullManager = GetComponent<Pull>();
        Pull.Pulled.AddListener(MoveSocketOnPull);
        Pull.Shooted.AddListener(ReleaseArrow);
        bow = GetComponentInParent<Bow>();
        bow.BowSelected.AddListener(SetReady);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        rope.SetActive(false);
        activeRope.SetActive(true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        rope.SetActive(true);
        activeRope.SetActive(false);

        // Reset position of the rope
        transform.position = pullManager.minPull.position;
    }

    public void ReleaseArrow(float pullStrenght)
    {
        // Release the arrow from the socket if the rope was pulled far enough, and reset the socket 
        if (pullStrenght > 0 && isSelectActive)
        {
            customInteractionManager.ForceExit(this);
        } else
        {
            // Reset position of the rope
            transform.position = pullManager.minPull.position;
        }
    }

    public void MoveSocketOnPull(Vector3 pullDirection)
    {
        // Move attach when bow is pulled, this updates the renderer as well
        transform.position = pullManager.minPull.position - pullDirection;
    }

    public void SetReady(bool state)
    {
        // Set the notch ready if bow is selected
        IsReady = state;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // We add the "CanHover" in case the socket is in the recycle time
        return base.CanSelect(interactable) && IsReady && CanHover(interactable as IXRHoverInteractable);
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && IsReady;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (CanHover(args.interactableObject) && IsReady)
        {
            if ((args.interactableObject as XRBaseInteractable).interactorsSelecting.Count > 0) {
                IXRSelectInteractor selectingInteractor = (args.interactableObject as XRBaseInteractable).interactorsSelecting[0];
                customInteractionManager.SelectEnter(this, args.interactableObject as IXRSelectInteractable);
                customInteractionManager.SelectEnter(selectingInteractor, pullManager);
            }
        }
    }
}
