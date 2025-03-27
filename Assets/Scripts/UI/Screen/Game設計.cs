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
        [SerializeField] private GameObject endGameOverlay = null;
        [SerializeField] private GameObject pauseOverlay = null;
        [SerializeField] private Transform selector = null;

        private Button buttonExitPause;
        private Button buttonExit;

        private void Awake()
        {
            var options = pauseOverlay.GetComponentsInChildren<Button>(true);
            foreach (var button in options)
            {
                var entry = new EventTrigger.Entry { eventID = EventTriggerType.Select };

                entry.callback.AddListener(OnOptionSelect);

                button.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
            }

            buttonExitPause = options[^1];

            options = endGameOverlay.GetComponentsInChildren<Button>(true);
            foreach (var button in options)
            {
                var entry = new EventTrigger.Entry { eventID = EventTriggerType.Select };

                entry.callback.AddListener(OnOptionSelect);

                button.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
            }

            buttonExit = options[^1];
        }

        private void OnEnable()
        {
            pauseOverlay.SetActive(false);
            endGameOverlay.SetActive(false);

            Game長.OnSetPlayerLife.AddListener(OnSetPlayerLife);
            Game長.OnSetScore.AddListener(OnSetScore);
            Game長.OnGamePause.AddListener(OnGamePause);
            Game長.OnGameEnd.AddListener(OnGameEnd);

            if (EventSystem.current.TryGetComponent<InputSystemUIInputModule>(out var UIInputModule))
            {
                UIInputModule.cancel.action.performed += OnCancel;
            }
        }

        private void OnDisable()
        {
            if (EventSystem.current.TryGetComponent<InputSystemUIInputModule>(out var UIInputModule))
            {
                UIInputModule.cancel.action.performed -= OnCancel;
            }

            Game長.OnGameEnd.RemoveListener(OnGameEnd);
            Game長.OnGamePause.RemoveListener(OnGamePause);
            Game長.OnSetPlayerLife.RemoveListener(OnSetPlayerLife);
            Game長.OnSetScore.RemoveListener(OnSetScore);
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

        private void OnGamePause(bool gamePaused)
        {
            pauseOverlay.SetActive(gamePaused);

            if (gamePaused) buttonExitPause.Select();
        }

        private void OnOptionSelect(BaseEventData baseEvent)
        {
            selector.SetParent(baseEvent.selectedObject.transform, false);
        }

        public void OnClickResume()
        {
            Game長.PauseGame();
        }

        public void OnClickExit()
        {
            General長.EndGame();
        }

        public void OnClickRestart()
        {
            General長.RestartGame();
        }

        private void OnGameEnd()
        {
            endGameOverlay.SetActive(true);
        }


        public void OnCancel(InputAction.CallbackContext context)
        {
            if (endGameOverlay.activeInHierarchy)
            {
                General長.EndGame();
            }
            else
            {
                Game長.PauseGame();
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            Debug.Assert(lifes.Length > 0);
            Debug.Assert(digits.Length > 0);
            Debug.Assert(endGameOverlay != null);
            Debug.Assert(pauseOverlay != null);
            Debug.Assert(selector != null);
        }
    }
}