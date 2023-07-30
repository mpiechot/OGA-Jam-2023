using ArcardeWaveShooter.Exceptions;
using UnityEngine;

public class ECDestroyAfterAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToDestroy;

    private void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(objectToDestroy, nameof(objectToDestroy));
    }

    public void Destroy()
    {
        Destroy(objectToDestroy);
    }
}
