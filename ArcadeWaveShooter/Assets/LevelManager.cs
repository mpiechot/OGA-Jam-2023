#nullable enable

using ArcardeWaveShooter.Exceptions;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader? levelLoader;

    private void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(levelLoader, nameof(levelLoader));

        levelLoader.NextLevel();
    }
}
