using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : BaseUIScreen
{
    public event Action RestartClickedEvent;
    public event Action MenuClickedEvent;
    public event Action<string> SaveScoreClickedEvent;

    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
    [SerializeField] private TMPro.TMP_InputField _nameInputField;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _saveScoreHolder;
    [SerializeField] private Button _saveScoreButton;

    private readonly string _scorePrefix = "Your total score =";

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartClick);
        _menuButton.onClick.AddListener(OnMenuClick);
        _saveScoreButton.onClick.AddListener(OnSaveClick);

        _nameInputField.onValueChanged.AddListener(ValidatePlayerName);
        _saveScoreButton.interactable = false;
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(OnRestartClick);
        _menuButton.onClick.RemoveListener(OnMenuClick);
        _saveScoreButton.onClick.RemoveListener(OnSaveClick);

        _nameInputField.onValueChanged.AddListener(ValidatePlayerName);
    }

    public void SetScoreText(string text) 
    {
        _scoreText.text = $"{_scorePrefix} {text}";
    }

    private void OnRestartClick() 
    {
        RestartClickedEvent?.Invoke(); 
        _saveScoreHolder.SetActive(true);
    }

    private void OnMenuClick()
    {
        MenuClickedEvent?.Invoke(); 
        _saveScoreHolder.SetActive(true);
    }

    private void OnSaveClick()
    {
        _saveScoreHolder.SetActive(false);
        string name = _nameInputField.text;
        SaveScoreClickedEvent?.Invoke(name);
    }

    private void ValidatePlayerName(string text)
    {
        char[] charsToTrim = { ' ', '\t', '\n' };
        text = text.Trim(charsToTrim);
        bool valid = !string.IsNullOrEmpty(text);

        _saveScoreButton.interactable = valid;
    }
}
