using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolReload : XRSocketInteractor
{
    private GameObject Pistol;

    protected override void Start()
    {
        base.Start();
        Pistol = GameObject.Find("pistol1");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        Pistol.GetComponent<GunShot>().Magazine += 1;
        Destroy(args.interactableObject.transform.gameObject);
    }
}
