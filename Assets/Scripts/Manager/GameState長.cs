using System;
using UnityEngine;

namespace Asteroider
{
    public enum GameState
    {
        Initial, Menu, GameStart, GameRunning, GameEnd
    }

    public class GameState長 : 抽象Manager<GameState長>
    {
        public static GameState GameState => gameState;
        private static GameState gameState;

        public static void ChangeState(GameState gameState)
            => Instance?.ChangeState_Implementation(gameState);

        public static EventProxy<GameState> OnGameState終 = new(out onGameState終);
        public static EventProxy<GameState> OnGameState始 = new(out onGameState始);

        private static Action<GameState> onGameState終;
        private static Action<GameState> onGameState始;

        private void Start()
        {
            ChangeState(GameState.Menu);
        }

        private void ChangeState_Implementation(GameState gameState)
        {
            Debug.Log($"[{GetType().Name}] 終 {GameState長.gameState}");
            onGameState終?.Invoke(GameState長.gameState);

            SetState(gameState);

            Debug.Log($"[{GetType().Name}] 始 {GameState長.gameState}");
            onGameState始?.Invoke(GameState長.gameState);
        }

        private void SetState(GameState gameState)
        {
            if (gameState == GameState.GameStart)
            {
                Layout長.ChangeScreen(LayoutType.Game);
            }
            if (gameState == GameState.Menu)
            {
                Layout長.ChangeScreen(LayoutType.Menu);
            }

            GameState長.gameState = gameState;
        }
    }
}
