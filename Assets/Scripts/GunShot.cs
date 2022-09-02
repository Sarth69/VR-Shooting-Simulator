using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShot : XRGrabInteractable
{
    public GameObject bulletPrefab;
    public Transform attach;
    public int ammunitionsInMagazine;

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        Shoot();
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, attach.position, attach.rotation);
    }
}
