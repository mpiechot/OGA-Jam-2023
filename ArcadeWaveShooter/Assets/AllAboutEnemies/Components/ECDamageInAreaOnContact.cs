using UnityEngine;

public class ECDamageInAreaOnContact : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool destroyOnHit;
    [SerializeField] private float damageRadius;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Apply damage to all health components in the area
        var damageReceivers = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (var receiver in damageReceivers)
        {
            if (receiver.gameObject != gameObject && receiver.TryGetComponent<ECHealth>(out ECHealth health))
            {
                health.ApplyDamage(damage);
            }
        }

        // Destroy if marked as destroy on hit
        if (destroyOnHit && animator != null)
        {
            animator.SetTrigger("Explode");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
