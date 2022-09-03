using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ARReload : XRSocketInteractor
{
    private GameObject AR;

    protected override void Start()
    {
        base.Start();
        AR = GameObject.Find("assault1");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Destroy");
        base.OnSelectEntered(args);

        AR.GetComponent<ARShot>().Magazine = 20;
        Destroy(args.interactableObject.transform.gameObject);
    }
}
