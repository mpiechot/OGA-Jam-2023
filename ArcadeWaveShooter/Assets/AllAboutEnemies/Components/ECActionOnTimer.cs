using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ECActionOnTimer : MonoBehaviour
{
    [SerializeField] private float timeInterval;
    [SerializeField] private int amountToInvoke;
    [SerializeField] private float sequentialInvocationTime;
    [SerializeField] private UnityEvent actionToInvoke;

    private float lastInvocationTime;
    private float lastInvocationTimeSpace;
    private int invokedAmount;

    void Update()
    {
        if(lastInvocationTime > timeInterval)
        {
            invokedAmount = amountToInvoke;
            lastInvocationTime = 0;
        }

        if(invokedAmount > 0 && lastInvocationTimeSpace <= 0)
        {
            invokedAmount--;
            actionToInvoke?.Invoke();
            lastInvocationTimeSpace = sequentialInvocationTime;
        }
        else 
            lastInvocationTime += Time.deltaTime;

        lastInvocationTimeSpace -= Time.deltaTime;
    }
}
