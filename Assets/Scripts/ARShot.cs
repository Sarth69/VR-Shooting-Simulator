using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ARShot : XRGrabInteractable
{
    public GameObject bulletPrefab;
    public Transform attach;
    public GameObject ammunitionImage;
    public TMP_Text ammunitionText;
    private AudioSource audioSource;
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        ammunitionImage.SetActive(false);
        ammunitionText.text = "";
    }
}
