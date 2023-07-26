using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingBullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool destroyOnHit;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.right * bulletSpeed;
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Apply Damage
        // Destroy self
        if(other.TryGetComponent<ECHealth>(out ECHealth health))
        {
            health.ApplyDamage(bulletDamage);
            if(destroyOnHit) Destroy(gameObject);
        }
    }
}
