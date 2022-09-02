using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    private Vector3 lastPosition;
    public Transform tip;
    public float speed;
    public LayerMask layermask = -Physics.IgnoreRaycastLayer;
    public ParticleSystem explosion;

    private void OnEnable()
    {
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
            if (hit.transform.gameObject.GetComponent<Target>() != null)
            {
                hit.transform.gameObject.GetComponent<Target>().targetHit();
            } else if (hit.transform.gameObject.GetComponentInParent<Target>())
            {
                hit.transform.gameObject.GetComponentInParent<Target>().targetHit();
            }
            explosion.transform.SetParent(transform.parent);
            explosion.transform.position = hit.point;
            explosion.Play();
            Destroy(gameObject);
        }
    }
}
