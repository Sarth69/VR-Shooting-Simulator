using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowPack : MonoBehaviour
{
    public GameObject arrowGenerator;
    public void OnQuiverLoad()
    {
        arrowGenerator.SetActive(true);
        gameObject.SetActive(false);
    }
}
