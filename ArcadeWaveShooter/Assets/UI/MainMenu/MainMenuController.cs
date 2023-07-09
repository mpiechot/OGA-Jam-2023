#nullable enable

using Assets.Scripts.Exceptions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private UIDocument mainMenu;

    private VisualElement? root;


    public void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(mainMenu, nameof(mainMenu));

        root = mainMenu.rootVisualElement;

        root.Q<Button>("StartButton").clicked += OnStartClicked;
        root.Q<Button>("LoadButton").clicked += OnLoadClicked;
        root.Q<Button>("QuitButton").clicked += OnQuitClicked;
        root.Q<Button>("CreditsButton").clicked += OnCreditsClicked;

    }

    private void OnCreditsClicked()
    {
        Debug.Log("Credits clicked!");
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }

    private void OnLoadClicked()
    {
        Debug.Log("Load clicked!");
    }

    private void OnStartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
