using UnityEngine;

public class ECCooldownOnContact : MonoBehaviour
{
    [SerializeField] private float coolingAmoung;
    [SerializeField] private bool destroyOnHit;

    private void OnTriggerEnter(Collider other)
    {
        // Apply Damage
        // Destroy self
        if (other.TryGetComponent<HeatKI>(out HeatKI heatKi))
        {
            heatKi.DecreaseHeat(coolingAmoung);

            if (destroyOnHit) Destroy(gameObject);
        }
    }
}
