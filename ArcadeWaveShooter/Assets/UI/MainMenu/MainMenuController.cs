#nullable enable

using ArcardeWaveShooter.Exceptions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private UIDocument? mainMenu;

    private VisualElement? root;
    private VisualElement? credits;

    private VisualElement Credits => credits ?? throw new NullReferenceException("Can't access credits because it is null");


    public void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(mainMenu, nameof(mainMenu));

        root = mainMenu.rootVisualElement;
        credits = root.Q<VisualElement>("Credits");

        root.Q<Button>("StartButton").clicked += OnStartClicked;
        root.Q<Button>("LoadButton").clicked += OnLoadClicked;
        root.Q<Button>("QuitButton").clicked += OnQuitClicked;
        root.Q<Button>("CreditsButton").clicked += OnOpenCreditsClicked;
        root.Q<Button>("CloseCreditsButton").clicked += OnCloseCreditsClicked;

    }

    private void OnCloseCreditsClicked()
    {
        Credits.style.display = DisplayStyle.None;
    }

    private void OnOpenCreditsClicked()
    {
        Credits.style.display = DisplayStyle.Flex;
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
