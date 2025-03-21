using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Asteroider.UI
{
    public class Menu設計 : 抽象Layout
    {
        [SerializeField] private Transform menu = null;
        [SerializeField] private Transform selector = null;

        private Button buttonStart;

        private void Awake()
        {
            var options = menu.GetComponentsInChildren<Button>();

            foreach (var button in options)
            {
                var entry = new EventTrigger.Entry { eventID = EventTriggerType.Select };

                entry.callback.AddListener(OnOptionSelect);

                button.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
            }

            buttonStart = options[0];
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(OnClickHack());
        }

        protected virtual void OnDisable()
        {
            buttonStart.onClick.RemoveListener(OnClickGameStart);
        }

        private IEnumerator OnClickHack()
        {
            yield return new WaitForEndOfFrame();

            buttonStart.onClick.AddListener(OnClickGameStart);
            buttonStart.Select();
        }

        private void OnOptionSelect(BaseEventData baseEvent)
        {
            selector.SetParent(baseEvent.selectedObject.transform, false);
        }

        public void OnClickGameStart() => GameRules長.StartGame();

        private void OnValidate()
        {
            Debug.Assert(selector != null);
            Debug.Assert(menu != null);
        }

        public void OnClickGameQuit()
        {
            Application.Quit();
        }


    }
}