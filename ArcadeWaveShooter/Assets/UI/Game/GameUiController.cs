#nullable enable

using ArcardeWaveShooter.Exceptions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUiController : MonoBehaviour
{
    [SerializeField]
    private UIDocument? gameMenu;

    private VisualElement root = null!;
    private VisualElement menuPopup = null!;

    public void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(gameMenu, nameof(gameMenu));

        root = gameMenu.rootVisualElement;
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
