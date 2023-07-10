using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LVLLevelPlayer : MonoBehaviour
{

    [System.Serializable]
    public struct Instance
    {
        [SerializeField, TextArea]
        public string label;
        public float timeToActivate;
        public GameObject objectReference;
        public bool waitForDestruction;
    }    
    public List<Instance> instances;
    private float timer = 0f;
    private int currentIndex = 0;
    private bool waitingForDestruction;

    private void Update()
    {
        if(!waitingForDestruction)
            timer += Time.deltaTime;

        if (currentIndex < instances.Count)
        {
            var currentInstance = instances[currentIndex];

            if (timer >= currentInstance.timeToActivate && !currentInstance.objectReference.activeSelf)
            {
                currentInstance.objectReference.SetActive(true);
                waitingForDestruction = currentInstance.waitForDestruction;
                currentIndex++;
                if (currentIndex >= instances.Count)
                    waitingForDestruction = true;
            }
        }
        
        if (waitingForDestruction && currentIndex > 0)
        {
            var previousInstance = instances[currentIndex - 1];

            if (!previousInstance.objectReference)
            {
                waitingForDestruction = false;
                if (currentIndex >= instances.Count) 
                { 
                    enabled = false;
                    Mailbox.InvokeSubscribers<GameOverMail>(true);
                }
            }

        }
    }
}
