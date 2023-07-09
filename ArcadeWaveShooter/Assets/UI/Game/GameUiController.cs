#nullable enable

using Assets.Scripts.Exceptions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUiController : MonoBehaviour
{
    [SerializeField]
    private UIDocument mainMenu;

    private VisualElement root;
    private VisualElement menuPopup;

    public void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(mainMenu, nameof(mainMenu));

        root = mainMenu.rootVisualElement;
        menuPopup = root.Q<VisualElement>("MenuPopup");

        root.Q<Button>("MenuButton").clicked += OnMenuClicked;
        root.Q<Button>("ContinueButton").clicked += OnContinueClicked;
        root.Q<Button>("QuitButton").clicked += OnQuitClicked;
    }

    private void OnQuitClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnContinueClicked()
    {
        menuPopup.style.display = DisplayStyle.None;
    }

    private void OnMenuClicked()
    {
        menuPopup.style.display = DisplayStyle.Flex;
    }
}
