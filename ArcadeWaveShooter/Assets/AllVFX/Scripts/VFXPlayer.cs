using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class VFXPlayer : MonoBehaviour
{
    bool stopSpawning = false;

    private void OnEnable()
    {
        // Subscribe to the "SpawnVFX" method
        Mailbox.AddSubscriber<VFXRequestMail>(SpawnVFX);
#if UNITY_EDITOR
        // Subscribe to the play mode state changed event
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
    }

    private void OnDisable()
    {
        // Unsubscribe from the "SpawnVFX" method
        Mailbox.RemoveSubscriber<VFXRequestMail>(SpawnVFX);
        stopSpawning = true;
#if UNITY_EDITOR
        // Unsubscribe from the play mode state changed event
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
    }

    private void SpawnVFX(VFXRequestMail vfxRequestMail)
    {
        if(stopSpawning) return;
        // Load the VFX prefab using Addressables
        vfxRequestMail.vfxAssetReference.LoadAssetAsync<GameObject>().Completed += (handle) =>  OnVFXLoaded(handle, vfxRequestMail.spawnPosition);
    }

    private void OnVFXLoaded(AsyncOperationHandle<GameObject> handle, Vector3 spawnPosition)
    {
        if(stopSpawning) return;
        // Spawn the VFX object at the current position
        GameObject vfxObject = Instantiate(handle.Result, spawnPosition, Quaternion.identity);
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode || state == PlayModeStateChange.EnteredEditMode)
        {
            // Unsubscribe from the "SpawnVFX" method
            Mailbox.RemoveSubscriber<VFXRequestMail>(SpawnVFX);
            stopSpawning = true;
        }
    }
#endif
}