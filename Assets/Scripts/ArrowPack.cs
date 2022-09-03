using UnityEngine;

public class ArrowPack : MonoBehaviour
{
    public GameObject arrowGenerator;
    public void OnQuiverLoad()
    {
        arrowGenerator.SetActive(true);
        gameObject.SetActive(false);
    }
}
