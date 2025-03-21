using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Asteroider.UI
{
public class Game設計 : 抽象Layout<Game設計, GameLayout設定>
    {
        [SerializeField] private Image[] lifes = null;
        [SerializeField] private Image[] digits = null;
        [SerializeField] private GameObject gameOver = null;
        [SerializeField] private GameObject pauseOverlay = null;

        private void OnEnable()
        {
            GameRules長.OnSetPlayerLife.AddListener(OnSetPlayerLife);
            GameRules長.OnSetScore.AddListener(OnSetScore);
            GameRules長.OnGamePaused.AddListener(OnGamePaused);
            GameState長.OnGameState始.AddListener(OnGameState始);

            var UIInputModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();
            UIInputModule.cancel.action.performed += OnCancel;
            UIInputModule.submit.action.performed += OnSubmit;
        }

        private void OnDisable()
        {
            var UIInputModule = EventSystem.current?.GetComponent<InputSystemUIInputModule>();
            if (UIInputModule != null)
            {
                UIInputModule.cancel.action.performed -= OnSubmit;
                UIInputModule.cancel.action.performed -= OnCancel;
            }

            GameState長.OnGameState始.RemoveListener(OnGameState始);
            GameRules長.OnGamePaused.RemoveListener(OnGamePaused);
            GameRules長.OnSetPlayerLife.RemoveListener(OnSetPlayerLife);
            GameRules長.OnSetScore.RemoveListener(OnSetScore);
        }

        private void OnGameState始(GameState gameState)
        {
            if (gameState == GameState.GameStart)
            {
                pauseOverlay.SetActive(false);
                gameOver.SetActive(false);
            }
            if (gameState == GameState.GameEnd)
            {
                gameOver.SetActive(true);
            }
        }

        private void OnSetPlayerLife(int value)
        {
            for (int i = 0; i < lifes.Length; i++)
            {
                lifes[i].enabled = (value > i);
            }
        }

        private void OnSetScore(int value)
        {
            digits[0].sprite = 設定.Numerals[value % 10];

            for (int i = 1; i < digits.Length; i++)
            {
                var number = Mathf.Pow(10f, i);
                digits[i].enabled = (value >= number);
                var index = ((int)((value / number) % number)) % 設定.Numerals.Length;
                digits[i].sprite = 設定.Numerals[index % 設定.Numerals.Length];
            }
        }

        private void OnGamePaused(bool value)
        {
            pauseOverlay.SetActive(value);
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (gameOver.activeInHierarchy)
            {
                GameRules長.ExitGame();
            }
            else
            {
                GameRules長.PauseGame();
            }
        }

        private void OnSubmit(InputAction.CallbackContext context)
        {
            if (gameOver.activeInHierarchy || pauseOverlay.activeInHierarchy)  GameRules長.ExitGame();
        }
    }
}