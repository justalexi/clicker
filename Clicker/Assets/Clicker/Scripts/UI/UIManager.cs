using Clicker.Scripts.UI;
using Events;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _settingsText;

    [SerializeField]
    private TextMeshProUGUI _scoreText;


    [SerializeField]
    private TextMeshProUGUI _upgradeYText;

    [SerializeField]
    private TextMeshProUGUI _upgradeZText;


    [SerializeField]
    private Button _clickButton;

    [SerializeField]
    private Button _upgradeYButton;

    [SerializeField]
    private Button _upgradeZButton;


    [SerializeField]
    private Button _reInitButton;

    private bool _isGameOver;

    private void OnEnable()
    {
        EnableGameInput();
        _reInitButton.onClick.AddListener(OnReInitButton);

        UIEvent.OnUIEvent += OnUIEvent;
    }

    private void OnDisable()
    {
        DisableGameInput();
        _reInitButton.onClick.RemoveListener(OnReInitButton);

        UIEvent.OnUIEvent -= OnUIEvent;
    }

    private void OnUIEvent(UIEventType type, object data = null)
    {
        switch (type)
        {
            case UIEventType.UpdateScore:
                _scoreText.text = (string)data;
                break;
            case UIEventType.UpdateSettings:
                var gameSettings = (GameSettings)data;
                _settingsText.text = $@"Click increment: {gameSettings.X}
Click upgrade increment:{gameSettings.Y}
Number of click upgrades:{gameSettings.UpgradePricesY.Count}
Auto upgrade increment:{gameSettings.Z}
Number of click upgrades:{gameSettings.UpgradePricesZ.Count}";
                break;
            case UIEventType.UpdateButtonY:
                var buttonYData = (ButtonData)data;
                _upgradeYText.text = $"Upgrade Click\n{buttonYData.Info}";
                _upgradeYButton.interactable = buttonYData.IsEnabled;
                break;
            case UIEventType.UpdateButtonZ:
                var buttonZData = (ButtonData)data;
                _upgradeZText.text = $"Upgrade Auto\n{buttonZData.Info}";
                _upgradeZButton.interactable = buttonZData.IsEnabled;
                break;
            case UIEventType.Success:
                _isGameOver = true;
                _scoreText.text = "SUCCESS!";
                DisableGameInput();
                break;
        }
    }

    private void EnableGameInput()
    {
        _clickButton.onClick.AddListener(OnClickButton);
        _upgradeYButton.onClick.AddListener(OnUpgradeYButton);
        _upgradeZButton.onClick.AddListener(OnUpgradeZButton);
    }

    private void DisableGameInput()
    {
        _clickButton.onClick.RemoveListener(OnClickButton);
        _upgradeYButton.onClick.RemoveListener(OnUpgradeYButton);
        _upgradeZButton.onClick.RemoveListener(OnUpgradeZButton);
    }

    private void OnClickButton()
    {
        GameEvent.Trigger(GameEventType.Click);
    }

    private void OnUpgradeYButton()
    {
        GameEvent.Trigger(GameEventType.UpgradeY);
    }

    private void OnUpgradeZButton()
    {
        GameEvent.Trigger(GameEventType.UpgradeZ);
    }

    private void OnReInitButton()
    {
        if (_isGameOver)
        {
            _isGameOver = false;
            EnableGameInput();
        }

        GameEvent.Trigger(GameEventType.ReInit);
    }
}