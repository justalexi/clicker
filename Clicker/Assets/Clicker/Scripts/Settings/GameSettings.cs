using System.Collections.Generic;

namespace DefaultNamespace
{
    [UnityEngine.CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings", order = 0)]
    public class GameSettings : UnityEngine.ScriptableObject
    {
        // Base increment
        public ulong X;

        // Click upgrades
        public ulong Y;
        public List<ulong> UpgradePricesY;
        public int MaxUpgradesY;

        // Auto increment upgrades
        public ulong Z;
        public List<ulong> UpgradePricesZ;
        public int MaxUpgradesZ;
    }
}