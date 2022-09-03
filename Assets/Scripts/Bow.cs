using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Bow : XRGrabInteractable
{
    public class BowSelectedEvent : UnityEvent<bool> { }
    public BowSelectedEvent BowSelected = new BowSelectedEvent();
    public GameObject ammunitionImage;
    public TMP_Text ammunitionText;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        BowSelected.Invoke(true);
        ammunitionImage.SetActive(true);
        ammunitionText.text = ": ∞";
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        BowSelected.Invoke(false);
        ammunitionImage.SetActive(false);
        ammunitionText.text = "";
    }
}
