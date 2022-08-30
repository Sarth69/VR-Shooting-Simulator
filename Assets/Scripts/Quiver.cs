using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRBaseInteractable
{
    public GameObject arrowPrefab;
    private bool enabling = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnArrowGrab);
    }

    public void OnArrowGrab(SelectEnterEventArgs args)
    {
        if(enabling)
        {
            enabling = false;
        } else
        {
            GameObject arrow = Instantiate(arrowPrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation);
            enabling = true;
            interactionManager.SelectEnter(args.interactorObject, arrow.GetComponent<XRBaseInteractable>());
        }
    }
}
