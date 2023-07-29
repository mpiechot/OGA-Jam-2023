using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttackKI : MonoBehaviour
{
    public float radiusIncreaseRate = 1f;

    private SphereCollider sphereCollider;
    private float currentRadius = 0f;
    private float targetRadius = 10f;
    private float targetDamage = 0f;

    private float cooldownTime = 10f;
    private float lastUseTime;

    private HeatKI heat;
    
    private bool canAttack = true;

    private Coroutine heatLimitRoutine;


    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        heat = GetComponentInParent<HeatKI>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0f;
        sphereCollider.enabled = false;
        Mailbox.AddSubscriber<HeatLimitReachedMail>(OnHeatLimitReached);
    }

    void Update()
    {
        // Check for input
        if (canAttack && Input.GetButtonDown("Fire2") && Time.time - lastUseTime > cooldownTime)
        {
            // Set the radius of the sphere collider to 0
            currentRadius = 0f;

            targetRadius = heat.heat / 3f;

            targetDamage = heat.heat * 2;

            // Enable the sphere collider
            sphereCollider.enabled = true;

            lastUseTime = Time.time;
            Mailbox.InvokeSubscribers(new HeatDecreaseMail() { decreaseAmount = 100 });
        }

        // Increase the radius of the sphere collider over time until it reaches 10
        if (currentRadius < targetRadius)
        {
            currentRadius += radiusIncreaseRate * Time.deltaTime;
            sphereCollider.radius = currentRadius;
        }
        else
        {
            // Disable the sphere collider when the radius reaches 10
            sphereCollider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has an ECHealth script attached to it
        ECHealth health = other.GetComponent<ECHealth>();
        if (health != null)
        {
            health.ApplyDamage(targetDamage);
        }
    }

    private void OnHeatLimitReached(HeatLimitReachedMail mail)
    {
        canAttack = false;
        if (heatLimitRoutine != null) StopCoroutine(heatLimitRoutine);
        heatLimitRoutine = StartCoroutine(EnableMovementAfterDelay(5.0f));
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
        heatLimitRoutine = null;
    }
}