using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

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
        vfxRequestMail.vfxAssetReference.LoadAssetAsync<GameObject>().Completed += (handle) =>  OnVFXLoaded(handle, vfxRequestMail.spawnPosition);
    }

    private void OnVFXLoaded(AsyncOperationHandle<GameObject> handle, Vector3 spawnPosition)
    {
        // Spawn the VFX object at the current position
        GameObject vfxObject = Instantiate(handle.Result, spawnPosition, Quaternion.identity);
    }
}