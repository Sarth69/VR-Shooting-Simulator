using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ARShot : XRGrabInteractable
{
    [Header("ARShot Data")]
    public GameObject bulletPrefab;
    public Transform attach;
    public GameObject ammunitionImage;
    public TMP_Text ammunitionText;
    private AudioSource audioSource;
    private XRBaseInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    [SerializeField]
    private int magazine = 20;
    private bool shooting = false;
    public int Magazine
    {
        get
        {
            return magazine;
        }
        set
        {
            if (magazine < 20)
            {
                magazine = 20;
                ammunitionText.text = ": " + magazine;
            }
        }
    }

    public XRSimpleInteractable secondHandGrabPoint;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        secondHandGrabPoint.onSelectEntered.AddListener(OnSecondHandGrab);
        secondHandGrabPoint.onSelectExited.AddListener(OnSecondHandRelease);
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        StartCoroutine(Shoot());
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);

        shooting = false;
        audioSource.Stop();
    }

    IEnumerator Shoot()
    {
        shooting = true;
        while (magazine > 0 && shooting)
        {
            magazine -= 1;
            ammunitionText.text = ": " + magazine;
            audioSource.PlayOneShot(audioSource.clip);
            Instantiate(bulletPrefab, attach.position, attach.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        audioSource.Stop();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        ammunitionImage.SetActive(true);
        ammunitionText.text = ": " + magazine;
        attachInitialRotation = args.interactor.attachTransform.localRotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        ammunitionImage.SetActive(false);
        ammunitionText.text = "";
        secondInteractor = null;
        args.interactor.attachTransform.localRotation = attachInitialRotation;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = interactorsSelecting.Count > 0 && !firstInteractorSelecting.Equals(interactor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        Debug.Log("ON SECOND HAND GRAB");
        secondInteractor = interactor;
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        Debug.Log("ON SECOND HAND RELEASE");
        secondInteractor = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondInteractor && interactorsSelecting.Count > 0)
        {
            // Compute rotation
            selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        base.ProcessInteractable(updatePhase);
    }
}
