using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LVLLevelPlayer : MonoBehaviour
{

    [System.Serializable]
    public struct Instance
    {
        [SerializeField, TextArea]
        public string label;
        public float timeToActivate;
        public List<GameObject> objectReferences;
        public bool waitForDestruction;
    }
    public List<Instance> instances;
    private float timer = 0f;
    private int currentIndex = 0;
    private bool waitingForDestruction;

    private void Update()
    {
        if (!waitingForDestruction)
            timer += Time.deltaTime;

        if (currentIndex < instances.Count)
        {
            var currentInstance = instances[currentIndex];

            if (timer >= currentInstance.timeToActivate && currentInstance.objectReferences.Any(@break => @break && !@break.activeSelf))
            {
                currentInstance.objectReferences.ForEach(@for => @for.SetActive(true));
                waitingForDestruction = currentInstance.waitForDestruction;
                currentIndex++;
                if (currentIndex >= instances.Count)
                    waitingForDestruction = true;
            }
        }

        if (waitingForDestruction && currentIndex > 0)
        {
            var previousInstance = instances[currentIndex - 1];

            if (previousInstance.objectReferences.All(@return => !@return))
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
