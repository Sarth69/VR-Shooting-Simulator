using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [Header("Arrow Shoot Data")]
    public float shootStrenghtFactor;
    public Transform tip;
    public AudioClip arrowAudio;
    public LayerMask layermask = -Physics.IgnoreRaycastLayer;
    private Rigidbody arrowRigidbody;
    private BoxCollider arrowCollider;
    private bool launched = false;
    private Vector3 lastPosition;
    private AudioSource audioSource;

    protected override void OnEnable()
    {
        base.OnEnable();
        Pull.Shooted.AddListener(ShootArrow);
        audioSource = GetComponent<AudioSource>();
        arrowRigidbody = GetComponent<Rigidbody>();
        arrowCollider = GetComponent<BoxCollider>();
        lastPosition = tip.position;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        arrowCollider.isTrigger = false;
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.useGravity = true;
        launched = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (!launched)
        {
            arrowRigidbody.useGravity = true;
            arrowRigidbody.isKinematic = false;
        }
    }

    private void ShootArrow(float pullStrengh)
    {
        if(pullStrengh > 0)
        {
            audioSource.PlayOneShot(arrowAudio);
            arrowRigidbody.AddForce(-transform.up.normalized * pullStrengh * shootStrenghtFactor, ForceMode.Impulse);
            launched = true;
            arrowCollider.isTrigger = true;
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
        {
            // During physics update, we adapt the direction of the arrow
            if (launched && arrowRigidbody.velocity.z > 0.5f)
            {
                transform.up = -arrowRigidbody.velocity;
            }
        }

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (launched)
            {
                CheckForHit();
                lastPosition = tip.position;
            }
        }
    }

    private void CheckForHit()
    {
        if (Physics.Linecast(lastPosition, tip.position, out RaycastHit hit, layermask) && hit.transform.gameObject.layer == 8 && launched)
        {
            Debug.Log("CheckForHit");
            arrowRigidbody.useGravity = false;
            arrowRigidbody.isKinematic = true;
            transform.SetParent(hit.transform);
            // Remove physics and attach the arrow to the target
            if (hit.transform.gameObject.GetComponent<Target>() != null)
            {
                hit.transform.gameObject.GetComponent<Target>().targetHit();
            }
            else if (hit.transform.gameObject.GetComponentInParent<Target>())
            {
                hit.transform.gameObject.GetComponentInParent<Target>().targetHit();
            }
            launched = false;
        }
    }
}
