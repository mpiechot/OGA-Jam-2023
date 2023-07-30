using System.Collections.Generic;
using UnityEngine;

public class ECPlayerHealth : ECHealth
{
    [SerializeField]
    private Drone dronePrefab;

    private List<Drone> drones = new List<Drone>();

    private void Start()
    {
        health = maxHealth;
        dronePrefab.gameObject.SetActive(false);

        int fullHealth = (int)maxHealth;
        for (int i = 0; i < fullHealth; i++)
        {
            var drone = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            drone.Initialize(gameObject);
            drone.gameObject.SetActive(true);
            drones.Add(drone);
        }

        if (fullHealth < maxHealth)
        {
            var drone = Instantiate(dronePrefab, transform.position, Quaternion.identity);
            drone.Initialize(gameObject);
            drone.gameObject.SetActive(true);
            drone.TakeDamage(maxHealth - fullHealth);
            drones.Add(drone);
        }
    }

    public override void ApplyDamage(float damage)
    {
        var fullDronesDamage = (int)damage;
        if (fullDronesDamage > 0 && drones.Count > 0)
        {
            for (int i = 0; i < fullDronesDamage; i++)
            {
                var drone = drones[drones.Count - 1];
                drones.Remove(drone);
                Destroy(drone.gameObject);

                if (drones.Count == 0)
                {
                    break;
                }
            }
        }
        health -= damage;
        var minorHealth = health - ((int)health);
        if (minorHealth > 0)
        {
            drones[0].TakeDamage(minorHealth);
        }

        if (health < 0 || drones.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
