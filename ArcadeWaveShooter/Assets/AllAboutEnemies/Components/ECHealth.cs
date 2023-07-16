using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECHealth : MonoBehaviour
{

    [SerializeField] private float maxHealth;

    private float health;

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
