using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.left * bulletSpeed;
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Apply Damage
        // Destroy self
    }
}
