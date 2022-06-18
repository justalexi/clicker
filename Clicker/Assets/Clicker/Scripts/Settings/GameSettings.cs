using System.Collections.Generic;

namespace Settings
{
    [UnityEngine.CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings", order = 0)]
    public class GameSettings : UnityEngine.ScriptableObject
    {
        // Base increment
        public ulong X;

        // Click upgrades
        public ulong Y;
        public List<ulong> UpgradePricesY;

        // Auto increment upgrades
        public ulong Z;
        public List<ulong> UpgradePricesZ;
    }
}