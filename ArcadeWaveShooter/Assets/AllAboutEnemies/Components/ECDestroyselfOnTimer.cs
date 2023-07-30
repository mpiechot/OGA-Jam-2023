using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECDestroyselfOnTimer : MonoBehaviour
{
    [SerializeField] private float destroyTime = 5;
    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }

}
