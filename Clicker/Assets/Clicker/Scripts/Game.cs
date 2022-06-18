using UnityEngine;

namespace DefaultNamespace
{
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
        
        
        
        private void Start()
        {
            
            
        }
        
        private void ReInit()
        {
            _score = 0;
            _clickIncrement = _gameSettings.X;
            _autoIncrement = 0;

            _numUpgradesY = 0;
            _numUpgradesZ = 0;
        }
        
        private void PurchaseUpgradeY()
        {
            if (_numUpgradesY >= _gameSettings.MaxUpgradesY)
            {
                // jTODO event
                // Max upgrades reached. Button should be disabled
                return;
            }

            var upgradePrice = _gameSettings.UpgradePricesY[_numUpgradesY];
            if (_score < upgradePrice)
            {
                // jTODO event
                // Not enough "money"
                return;
            }

            _score -= upgradePrice;
            _numUpgradesY += 1;

            _clickIncrement += _gameSettings.Y;

            // jTODO event
        }
        
        private void PurchaseUpgradeZ()
        {
            if (_numUpgradesZ >= _gameSettings.MaxUpgradesZ)
            {
                // jTODO event
                // Max upgrades reached. Button should be disabled
                return;
            }

            var upgradePrice = _gameSettings.UpgradePricesZ[_numUpgradesZ];
            if (_score < upgradePrice)
            {
                // jTODO event
                // Not enough "money"
                return;
            }

            _score -= upgradePrice;
            _numUpgradesZ += 1;

            _autoIncrement += _gameSettings.Z;

            // jTODO event
        }
        
        private void Tick()
        {
            // jTODO apply auto increment
            
            
        }
    }
}