using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseUIScreen
{
    public event Action PlayClickedEvent;
    public event Action ShowLBClickedEvent;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _showLBButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayClicked);
        _showLBButton.onClick.AddListener(OnShowLBClicked);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(OnPlayClicked);
        _showLBButton.onClick.RemoveListener(OnShowLBClicked);
    }

    private void OnPlayClicked() 
    {
        PlayClickedEvent?.Invoke();
    }

    private void OnShowLBClicked()
    {
        ShowLBClickedEvent?.Invoke();
    }
}
