using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform top;
    public Transform middle;
    public Transform bottom;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        Application.onBeforeRender += UpdateLine;
    }

    private void UpdateLine()
    {
        lineRenderer.SetPositions(new Vector3[] { top.position, middle.position, bottom.position });
    }
}
