using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Scriptable Objects/Config/Gameboard Object/Ship")]
    public class Ship設定 : 抽象GameboardObjectConfig<Ship>
    {
        public float DurationSpawnProtection = 1.4f;
    }
}