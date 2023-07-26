using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECDamageOnContact : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool destroyOnHit;

    private void OnTriggerEnter(Collider other)
    {
        // Apply Damage
        // Destroy self
        if (other.TryGetComponent<ECHealth>(out ECHealth health))
        {
            health.ApplyDamage(damage);
            if (destroyOnHit) Destroy(gameObject);
        }
    }
}
