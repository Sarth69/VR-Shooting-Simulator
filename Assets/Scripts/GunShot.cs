using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GunShot : XRGrabInteractable
{
    public GameObject bulletPrefab;
    public Transform attach;
    public AudioClip[] gunShotsSounds;
    public GameObject ammunitionImage;
    public TMP_Text ammunitionText;
    private RandomAudioPlayer randomClipPlayer;
    private float shotCooldown = 1;
    private float lastShot = 0;
    [SerializeField]
    private int magazine = 6;
    public int Magazine {
        get {
            return magazine;
        }
        set {
            if (magazine < 6)
            {
                magazine = value;
                ammunitionText.text = ": " + magazine;
            }
        }
    }

    private void Start()
    {
        randomClipPlayer = GetComponent<RandomAudioPlayer>();
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        Shoot();
    }

    private void Shoot()
    {
        if (Time.time-shotCooldown > lastShot && magazine > 0)
        {
            lastShot = Time.time;
            magazine -= 1;
            ammunitionText.text = ": " + magazine;
            randomClipPlayer.playRandomAudio(gunShotsSounds);
            Instantiate(bulletPrefab, attach.position, attach.rotation);
        }
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
