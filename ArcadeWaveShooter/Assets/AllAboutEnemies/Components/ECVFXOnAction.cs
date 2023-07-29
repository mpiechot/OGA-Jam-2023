using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ECVFXOnAction : MonoBehaviour
{
    [SerializeField] private AssetReferenceT<GameObject> vfxAssetPrefabReference;

    public enum CallScenario
    {
        OnDestroy,
        OnTriggerEnter,
        Manual
    }

public CallScenario callScenario = CallScenario.OnDestroy;

    private void OnDestroy()
    {
        if (callScenario == CallScenario.OnDestroy)
        {
            SendVFXSpawnMail();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (callScenario == CallScenario.OnTriggerEnter)
        {
            SendVFXSpawnMail();
        }
    }

    private void SendVFXSpawnMail()
    {
        if (vfxAssetPrefabReference != null)
        {
            Mailbox.InvokeSubscribers<VFXRequestMail>(new VFXRequestMail() { spawner = gameObject, spawnPosition = transform.position, vfxAssetReference = vfxAssetPrefabReference });
        }
    }
}