using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BoxCollider bulletCollider;
    private Rigidbody bulletRigidbody;
    private Vector3 lastPosition;
    public Transform tip;
    public float speed;
    public LayerMask layermask = -Physics.IgnoreRaycastLayer;
    public ParticleSystem explosion;

    private void OnEnable()
    {
        bulletCollider = GetComponent<BoxCollider>();
        bulletRigidbody = GetComponent<Rigidbody>();
        lastPosition = tip.position;
    }
    private void Start()
    {
        bulletRigidbody.AddForce(transform.up * speed, ForceMode.Impulse);
        transform.SetParent(transform);
    }

    private void Update()
    {
        CheckForCollision();
    }

    private void CheckForCollision()
    {
        if (Physics.Linecast(lastPosition, transform.position, out RaycastHit hit, layermask))
        {
            // Launch explosion and delete the bullet
            explosion.transform.position = hit.point;
            explosion.Play();
            Destroy(this);
        }
    }
}
