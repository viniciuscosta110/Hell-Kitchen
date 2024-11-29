using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    private void Awake() {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
    }

    private void OnOptionsButtonClicked() {
        OptionsUI.Instance.Show();
    }

    private void OnResumeButtonClicked(){
        GameManager.Instance.TogglePauseGame();
    }

    private void OnMainMenuButtonClicked() {
        Loader.Load(Loader.Scenes.MainMenuScene);
    }

    private void Start() {
        hide();
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        show();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        hide();
    }

    private void show() {
        gameObject.SetActive(true);
    }

    private void hide() {
        gameObject.SetActive(false);
    }
}
