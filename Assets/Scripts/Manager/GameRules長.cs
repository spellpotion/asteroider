using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroider
{
    public class GameRules長 : 抽象Manager<GameRules長, GameRules設定>
    {
        public static EventProxy<int> OnSetPlayerLife = new(out onSetPlayerLife);
        public static EventProxy<int> OnSetScore = new (out onSetScore);
        public static EventProxy<bool> OnGamePaused = new (out onGamePaused);

        public static void StartGame() => Instance.StartGame_Implementation();
        public static void PauseGame() => Instance.PauseGame_Implementation();
        public static void ExitGame() => Instance.ExitGame_Implementation();

        private static Action<int> onSetPlayerLife;
        private static Action<int> onSetScore;
        private static Action<bool> onGamePaused;

        private float spawnAnomalyProbability;
        private float spawnAnomalyDelay;

        private int playerLife;
        private int score;
        private int asteroidCount;
        private bool gamePaused;

        private Coroutine spawnSpecialWait;
        private Coroutine spawnShipWait;
        private Coroutine spawnAsteroidsWait;

        override protected void OnEnable()
        {
            base.OnEnable();

            Reward能.OnReward.AddListener(OnReward);
            Asteroid.OnCountUpdate.AddListener(OnAsteroidCountUpdate);
            GameState長.OnGameState始.AddListener(OnGameState始);
            Layout長.OnLayout終.AddListener(OnLayout終);
        }

        override protected void OnDisable()
        {
            Layout長.OnLayout終.RemoveListener(OnLayout終);
            GameState長.OnGameState始.RemoveListener(OnGameState始);
            Asteroid.OnCountUpdate.RemoveListener(OnAsteroidCountUpdate);
            Reward能.OnReward.RemoveListener(OnReward);

            base.OnDisable();
        }

        #region UI

        private void StartGame_Implementation()
        {
            GameState長.ChangeState(GameState.GameStart);
        }

        private void PauseGame_Implementation()
        {
            gamePaused = !gamePaused;

            Time.timeScale = gamePaused ? 0f : 1f;
            onGamePaused?.Invoke(gamePaused);
        }

        private void ExitGame_Implementation()
            => GameState長.ChangeState(GameState.Menu);

        #endregion UI
        #region Layout

        private void OnLayout終(LayoutType layoutType)
        {
            if (layoutType == LayoutType.Game)
            {
                if (spawnShipWait != null)
                {
                    StopCoroutine(spawnShipWait);
                    spawnShipWait = null;
                }
                if (spawnSpecialWait != null)
                { 
                    StopCoroutine(spawnSpecialWait);
                    spawnSpecialWait = null;
                }
                if (spawnAsteroidsWait != null)
                {
                    StopCoroutine(spawnAsteroidsWait);
                    spawnAsteroidsWait = null;
                }
            }
        }

        #endregion Layout
        #region GameState

        private void OnGameState始(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {
                SpawnAsteroids();
            }
            else if (gameState == GameState.GameStart)
            {
                InitializeGame();

                SpawnShip();
                SpawnAsteroids();
                SpawnAnomalyDelayed();

                GameState長.ChangeState(GameState.GameRunning);
            }

            UnPause();
        }

        private void UnPause()
        {
            gamePaused = false;
            Time.timeScale = 1f;
        }

        private void InitializeGame()
        {
            SetPlayerLife(設定.LifeMax);
            SetScore(0);
            asteroidCount = 0;

            spawnAnomalyDelay = 設定.DelaySpawnAnomalyInitial;
            spawnAnomalyProbability = 1f;
        }

        private void SetPlayerLife(int playerLife)
        {
            this.playerLife = playerLife;
            onSetPlayerLife?.Invoke(playerLife);
        }

        #endregion GameState
        #region Score

        private void OnReward(int reward)
        {
            SetScore(score + reward);
        }

        private void SetScore(int score)
        {
            this.score = score;
            onSetScore?.Invoke(score);
        }

        #endregion Score
        #region Ship

        private void SpawnShip()
        {
            Instantiate(設定.ShipPrefab).OnDisabled.AddListener(OnSpawnShipDisabled);
        }

        private void OnSpawnShipDisabled(GameboardObject ship)
        {
            Gameboard長.Remove(ship);

            SetPlayerLife(playerLife - 1);

            if (playerLife > 0)
            {
                spawnShipWait = StartCoroutine(SpawnShipWait());
            }
            else
            {
                GameState長.ChangeState(GameState.GameEnd);
            }
        }
        private IEnumerator SpawnShipWait()
        {
            yield return new WaitForSeconds(設定.DelaySpawnShip);

            SpawnShip();

            spawnShipWait = null;
        }

        #endregion Ship
        #region Asteroid

        private void OnAsteroidCountUpdate(bool add)
        {
            if (GameState長.GameState != GameState.GameRunning &&
                GameState長.GameState != GameState.GameStart)
            {
                return;
            }

            asteroidCount += add ? 1 : -1;
            
            if (asteroidCount == 0)
            {
                spawnAsteroidsWait = StartCoroutine(SpawnAsteroidsWait());
            }
        }

        private void SpawnAsteroids()
        {
            for (var i = 0; i < 設定.AsteroidCountInitial; i++)
            {
                Instantiate(
                    設定.AsteroidPrefab,
                    Gameboard長.GetRandomPositionInside(),
                    Quaternion.identity
                    );
            }
        }

        private IEnumerator SpawnAsteroidsWait()
        {
            yield return new WaitForSeconds(設定.DelaySpawnShip);

            SpawnAsteroids();

            spawnAsteroidsWait = null;
        }

        #endregion Asteroid
        #region Anomaly

        private void SpawnAnomalyDelayed()
            => spawnSpecialWait = StartCoroutine(SpawnAnomalyWait());

        private IEnumerator SpawnAnomalyWait()
        {
            yield return new WaitForSeconds(spawnAnomalyDelay);

            var prefab = IsEnemy() ? 設定.EnemyPrefab : 設定.CuriosityPrefab;
            var position = Gameboard長.GetRandomPositionOnTheEdge();

            var special = Instantiate(prefab, position, Quaternion.identity);
            special.OnDisabled.AddListener(OnAnomalyDisabled);

            spawnSpecialWait = null;

            bool IsEnemy() => (Random.Range(0f, 1f) > spawnAnomalyProbability);
        }

        private void OnAnomalyDisabled(GameboardObject special)
        {
            Gameboard長.Remove(special);

            spawnAnomalyDelay *= 設定.RatioSpawnCuriosity;
            spawnAnomalyProbability *= 設定.RatioSpawnCuriosity;

            if (GameState長.GameState == GameState.GameRunning)
            {
                SpawnAnomalyDelayed();
            }
        }

        #endregion Anomaly

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}