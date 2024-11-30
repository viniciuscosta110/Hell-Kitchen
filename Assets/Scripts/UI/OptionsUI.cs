using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onOptionsClose;

    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => { SoundManager.Instance.ChangeVolume(); UpdateVisuals(); });
        musicButton.onClick.AddListener(() => { MusicManager.Instance.ChangeVolume(); UpdateVisuals(); });
        closeButton.onClick.AddListener(() => { Hide(); this.onOptionsClose(); });
        moveUpButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Interact_Alternate); });
        pauseButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Gamepad_Interact); });
        gamepadInteractAltButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Gamepad_Interact_Alternate); });
        gamepadPauseButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Gamepad_Pause); });
    }

    private void Start() {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisuals();

        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void UpdateVisuals() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_Alternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onOptionsClose) {
        this.onOptionsClose = onOptionsClose;
        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBiding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBiding(binding, () => {
            HidePressToRebindKey();
            UpdateVisuals();
        });
    }
}
