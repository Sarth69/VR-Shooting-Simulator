using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AmmoGenerator : XRBaseInteractable
{
    public GameObject ammoPrefab;
    private bool enabling = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnBulletGrab);
    }

    public void OnBulletGrab(SelectEnterEventArgs args)
    {
        if (enabling)
        {
            enabling = false;
        }
        else
        {
            Debug.Log(args.interactorObject.transform.position + " " + args.interactorObject.transform.rotation);
            GameObject bullet = Instantiate(ammoPrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation);
            enabling = true;
            interactionManager.SelectEnter(args.interactorObject, bullet.GetComponent<XRBaseInteractable>());
        }
    }
}
