using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingBullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime = 5;
    [SerializeField] private bool destroyOnHit;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.right * bulletSpeed;
        Destroy(gameObject, bulletLifetime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.right * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Apply Damage
        // Destroy self
        if (other.TryGetComponent<ECHealth>(out ECHealth health))
        {
            health.ApplyDamage(bulletDamage);
            if (destroyOnHit) Destroy(gameObject);
        }
    }
}
