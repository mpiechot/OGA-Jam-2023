using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class VFXPlayer : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the "SpawnVFX" method
        Mailbox.AddSubscriber<VFXRequestMail>(SpawnVFX);
    }

    private void SpawnVFX(VFXRequestMail vfxRequestMail)
    {
        // Load the VFX prefab using Addressables
        vfxRequestMail.vfxAssetReference.LoadAssetAsync<GameObject>().Completed += OnVFXLoaded;
    }

    private void OnVFXLoaded(AsyncOperationHandle<GameObject> handle)
    {
        // Spawn the VFX object at the current position
        GameObject vfxObject = Instantiate(handle.Result, transform.position, Quaternion.identity);

        // Set the VFX object as a child of this object
        vfxObject.transform.parent = transform;
    }
}