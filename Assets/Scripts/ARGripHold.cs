using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ARGripHold : XRGrabInteractable
{
    private GameObject weapon;

    protected void Start()
    {
        weapon = GameObject.Find("assault1");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("OnSelect");

        weapon.transform.rotation = transform.rotation;
    }

}
