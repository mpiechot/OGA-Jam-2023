using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKI : MonoBehaviour
{
    [SerializeField] private ECSpawner weapon;
    public float shootInterval = 1.0f; // Time interval between shots
    private float timeSinceLastShot = 0.0f; // Time since last shot

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime; // Increment time since last shot

        if (Input.GetButton("Fire1") && timeSinceLastShot >= shootInterval)
        {
            weapon.Spawn(); // Call the Shoot method
            timeSinceLastShot = 0.0f; // Reset time since last shot
            Mailbox.InvokeSubscribers(new HeatDecreaseMail() { decreaseAmount = -1 });
        }
    }
}