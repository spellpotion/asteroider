using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "Scriptable Objects/抽象Manager/GameRules")]
    public class GameRules設定 : 抽象ManagerConfig<Game長>
    {
        [Header("Player")]
        public Ship ShipPrefab;
        public int LifeMax = 3;
        public float DelaySpawnShip = 3f;

        [Header("Anomaly")]
        public GameboardObject CuriosityPrefab;
        public GameboardObject EnemyPrefab;
        public float DelaySpawnAnomalyInitial = 30f;
        [Range(0f, 1f)]
        public float RatioSpawnCuriosity = .8f;

        [Header("Asteroid")]
        public GameboardObject AsteroidPrefab;
        public int AsteroidCountInitial = 6;
    }
}
