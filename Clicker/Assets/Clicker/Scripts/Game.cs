using System.Collections;
using System.Collections.Generic;
using Clicker.Scripts.UI;
using Events;
using Settings;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameSettings _gameSettings;

    private ulong _score;

    private int _numUpgradesY;
    private int _numUpgradesZ;

    // Cached value (X + N * Y)
    private ulong _clickIncrement;

    // Cached value (M * Z)
    private ulong _autoIncrement;
    private Coroutine _tickCoroutine;


    private void Start()
    {
        ShowSettings();

        ReInit();
    }

    private void ShowSettings()
    {
        UIEvent.Trigger(UIEventType.UpdateSettings, _gameSettings);
    }

    private void ReInit()
    {
        _score = 0;
        _clickIncrement = _gameSettings.X;
        _autoIncrement = 0;

        _numUpgradesY = 0;
        _numUpgradesZ = 0;

        // jTODO optimize
        UIEvent.Trigger(UIEventType.UpdateScore, ScoreFormatter.Format(0, 0));
        UIEvent.Trigger(UIEventType.UpdateButtonY, new ButtonData(GetUpgradeButtonInfo(_numUpgradesY, _gameSettings.UpgradePricesY), _numUpgradesY < _gameSettings.UpgradePricesY.Count));
        UIEvent.Trigger(UIEventType.UpdateButtonZ, new ButtonData(GetUpgradeButtonInfo(_numUpgradesZ, _gameSettings.UpgradePricesZ), _numUpgradesZ < _gameSettings.UpgradePricesZ.Count));


        if (_tickCoroutine != null)
            StopCoroutine(_tickCoroutine);

        _tickCoroutine = StartCoroutine(Tick());
    }

    private void OnEnable()
    {
        GameEvent.OnGameEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEvent.OnGameEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType type, object data = null)
    {
        switch (type)
        {
            case GameEventType.Click:
                OnClick();
                break;
            case GameEventType.UpgradeY:
                PurchaseUpgradeY();
                break;
            case GameEventType.UpgradeZ:
                PurchaseUpgradeZ();
                break;
            case GameEventType.ReInit:
                ReInit();
                break;
        }
    }

    private void OnClick()
    {
        if (CheckForGameOver())
        {
            HandleGameOver();
            return;
        }

        UpdateScore(_score + _clickIncrement);
    }

    private void PurchaseUpgradeY()
    {
        if (_numUpgradesY >= _gameSettings.UpgradePricesY.Count)
        {
            // Max upgrades reached. Button should be disabled
            return;
        }

        var upgradePrice = _gameSettings.UpgradePricesY[_numUpgradesY];
        if (_score < upgradePrice)
        {
            // jTODO maybe event
            // Not enough "money"
            return;
        }

        UpdateScore(_score - upgradePrice);

        _numUpgradesY += 1;

        UIEvent.Trigger(UIEventType.UpdateButtonY, new ButtonData(GetUpgradeButtonInfo(_numUpgradesY, _gameSettings.UpgradePricesY), _numUpgradesY < _gameSettings.UpgradePricesY.Count));

        // Update cached value
        _clickIncrement += _gameSettings.Y;
    }

    private void PurchaseUpgradeZ()
    {
        if (_numUpgradesZ >= _gameSettings.UpgradePricesZ.Count)
        {
            // Max upgrades reached. Button should be disabled
            return;
        }

        var upgradePrice = _gameSettings.UpgradePricesZ[_numUpgradesZ];
        if (_score < upgradePrice)
        {
            // jTODO maybe event
            // Not enough "money"
            return;
        }

        UpdateScore(_score - upgradePrice);

        _numUpgradesZ += 1;

        UIEvent.Trigger(UIEventType.UpdateButtonZ, new ButtonData(GetUpgradeButtonInfo(_numUpgradesZ, _gameSettings.UpgradePricesZ), _numUpgradesZ < _gameSettings.UpgradePricesZ.Count));

        // Update cached value
        _autoIncrement += _gameSettings.Z;
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (CheckForGameOver())
            {
                HandleGameOver();
                yield break;
            }

            if (_autoIncrement > 0)
                UpdateScore(_score + _autoIncrement);
        }
    }

    private bool CheckForGameOver()
    {
        // Check for overflow
        if (_score > ulong.MaxValue - _clickIncrement)
        {
            HandleGameOver();

            return true;
        }

        return false;
    }

    private void HandleGameOver()
    {
        // if (_tickCoroutine != null)
            // StopCoroutine(_tickCoroutine);

        _tickCoroutine = null;

        UIEvent.Trigger(UIEventType.Success);
    }

    private void UpdateScore(ulong newValue)
    {
        var delta = newValue > _score ? newValue - _score : 0;

        _score = newValue;

        UIEvent.Trigger(UIEventType.UpdateScore, ScoreFormatter.Format(_score, delta));
    }

    private string GetUpgradeButtonInfo(int numUpgrades, List<ulong> prices)
    {
        var nextUpgradePrice = (numUpgrades < prices.Count) ? $"${prices[numUpgrades].ToString()}, " : "";
        var numUpgradesY = $"{numUpgrades}/{prices.Count}";
        return $"{nextUpgradePrice}{numUpgradesY}";
    }
}