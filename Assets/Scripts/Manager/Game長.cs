using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Asteroider
{
    [RequireComponent(typeof(PlayerInput))]
    public class Game長 : 抽象Manager<Game長, Game設定>
    {
        enum State { None, Demo, GameStart, Running, GameEnd }
        private State state;

        public static EventProxy<int> OnSetPlayerLife = new(out onSetPlayerLife);
        public static EventProxy<int> OnSetScore = new(out onSetScore);
        public static EventProxy<bool> OnGamePause = new(out onGamePause);
        public static EventProxy OnGameEnd = new(out onGameEnd);

        public static void StartDemo() => Instance.StartDemo_Implementation();
        public static void StopDemo() => Instance.StopDemo_Implementation();
        public static void StartGame() => Instance.StartGame_Implementation();
        public static void PauseGame() => Instance.PauseGame_Implementation();
        public static void StopGame() => Instance.StopGame_Implementation();

        private static Action<int> onSetPlayerLife;
        private static Action<int> onSetScore;
        private static Action<bool> onGamePause;
        private static Action onGameEnd;

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
        }

        override protected void OnDisable()
        {
            Asteroid.OnCountUpdate.RemoveListener(OnAsteroidCountUpdate);
            Reward能.OnReward.RemoveListener(OnReward);

            base.OnDisable();
        }

        private void StartDemo_Implementation()
        {
            state = State.Demo;

            SpawnAsteroids();
            SetGamePaused(false);
        }

        private void StopDemo_Implementation()
        {
            state = State.None;

            Gameboard長.Clear();
            SetGamePaused(true);
        }

        private void StartGame_Implementation()
        {
            state = State.GameStart;

            InitializeGame();

            SpawnShip();
            SpawnAsteroids();
            SpawnAnomalyDelayed();

            state = State.Running;

            SetGamePaused(false);
        }

        private void PauseGame_Implementation()
        {
            if (state != State.Running) return;

            gamePaused = !gamePaused;

            Time.timeScale = gamePaused ? 0f : 1f;

            onGamePause?.Invoke(gamePaused);
        }

        private void StopGame_Implementation()
        {
            state = State.None;

            Gameboard長.Clear();

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

            SetGamePaused(true);
        }

        private void SetGamePaused(bool gamePaused)
        {
            Time.timeScale = gamePaused ? 0f : 1f;

            this.gamePaused = gamePaused;
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
            if (state != State.Running) return;

            Gameboard長.Remove(ship);

            SetPlayerLife(playerLife - 1);

            if (playerLife > 0)
            {
                spawnShipWait = StartCoroutine(SpawnShipWait());
            }
            else
            {
                state = State.GameEnd;

                onGameEnd?.Invoke();
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
            if (state != State.Running && state != State.GameStart) return;

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

            if (state == State.Running)
            {
                SpawnAnomalyDelayed();
            }
        }

        #endregion Anomaly
        #region Debug
#if UNITY_EDITOR

        private void OnSpawnEnemy()
        {
            var prefab = 設定.EnemyPrefab;
            var position = Gameboard長.GetRandomPositionOnTheEdge();

            var enemy = Instantiate(prefab, position, Quaternion.identity);

            if (state == State.Running)
            {
                if (spawnAsteroidsWait != null)
                {
                    StopCoroutine(spawnAsteroidsWait);
                    spawnAsteroidsWait = null;
                }
                enemy.OnDisabled.AddListener(OnAnomalyDisabled);
            }
            else
            {
                enemy.OnDisabled.AddListener(x => Gameboard長.Remove(x));
            }
        }

        private void OnSpawnCuriosity()
        {
            var prefab = 設定.CuriosityPrefab;
            var position = Gameboard長.GetRandomPositionOnTheEdge();

            var curiosity = Instantiate(prefab, position, Quaternion.identity);

            if (state == State.Running)
            {
                if (spawnAsteroidsWait != null)
                {
                    StopCoroutine(spawnAsteroidsWait);
                    spawnAsteroidsWait = null;
                }
                curiosity.OnDisabled.AddListener(OnAnomalyDisabled);
            }
            else
            {
                curiosity.OnDisabled.AddListener(x => Gameboard長.Remove(x));
            }
        }

#endif
        #endregion Debug

        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}