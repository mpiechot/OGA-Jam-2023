using UnityEngine;

public class ECHealth : MonoBehaviour
{

    [SerializeField, Range(1, 5)] protected float maxHealth;

    protected float health;

    private void Start()
    {
        health = maxHealth;
    }

    public virtual void ApplyDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
