using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECSpawner : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;

    public void Spawn()
    {
        Instantiate(toSpawn, transform.position, transform.rotation);
    }
}
