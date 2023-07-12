#nullable enable

using ArcardeWaveShooter.Exceptions;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader? levelLoader;

    private LevelLoader LevelLoader => levelLoader == null ? throw new SerializeFieldNotAssignedException() : levelLoader;

    private void Start()
    {
        LevelLoader.NextLevel();
        Mailbox.AddSubscriber<GameOverMail>(OnGameOver);
    }

    private void OnGameOver(object obj)
    {
        if (obj is not bool gameOver)
        {
            return;
        }

        if (gameOver)
        {
            // Player reached the end of the level
            LevelLoader.NextLevel();
        }
        else
        {
            // Player died
            LevelLoader.ReloadLoadedLevel();
        }
    }
}
