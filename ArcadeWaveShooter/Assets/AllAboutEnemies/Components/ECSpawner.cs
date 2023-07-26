using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECSpawner : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private int amountToSpawn;
    [SerializeField] private float angle;

    public void Spawn()
    {
        for(int i = 0; i < amountToSpawn; i++)
        {
            float lowestAngle = -angle / 2;
            float angleDelta = amountToSpawn > 1 ? angle / (amountToSpawn - 1) : 0;
            Instantiate(toSpawn, transform.position, transform.rotation * Quaternion.Euler(0,0, lowestAngle + angleDelta * i));
        }
    }
}
