using UnityEngine;

public class ECDestroyAfterAnimationEvent : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
