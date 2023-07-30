#nullable enable

using ArcardeWaveShooter.Exceptions;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelLoader? levelLoader;

    [SerializeField]
    private GameObject? loadingScreenMask;

    [SerializeField]
    private float loadingScreenMaskSpeed = 3f;

    private LevelLoader LevelLoader => levelLoader == null ? throw new SerializeFieldNotAssignedException() : levelLoader;

    private GameObject LoadingScreenMask => loadingScreenMask == null ? throw new SerializeFieldNotAssignedException() : loadingScreenMask;

    private void Start()
    {
        LoadLevel(true);
        Mailbox.AddSubscriber<GameOverMail>(OnGameOver);
    }

    private void OnGameOver(GameOverMail obj)
    {
        ToLevel(obj.isWon);
    }

    private void ToLevel(bool next)
    {
        LoadingScreenMask.transform.position = new Vector3(-Screen.width * .05f, 0, 0);
        LoadingScreenMask.transform.DOMoveX(0, loadingScreenMaskSpeed).SetEase(Ease.OutBounce).OnComplete(() => LoadLevel(next));
    }

    private void LoadLevel(bool next)
    {
        if (next)
        {
            LevelLoader.NextLevel();
        }
        else
        {
            LevelLoader.ReloadLoadedLevel();
        }

        LoadingScreenMask.transform.DOMoveX(Screen.width * .05f, loadingScreenMaskSpeed).SetDelay(.5f).OnComplete(() =>
        {
            LoadingScreenMask.transform.position = new Vector3(-Screen.width * .05f, 0, 0);
        });
    }
}
