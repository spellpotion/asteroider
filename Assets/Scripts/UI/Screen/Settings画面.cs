using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Asteroider
{
    public class Settings画面 : 抽象Screen
    {
        [SerializeField] private Transform content = null;
        [SerializeField] private Transform selector = null;

        private GameObject selected;

        private void Awake()
        {
            var options = GetComponentsInChildren<Button>();
            foreach (var button in options)
            {
                var entry = new EventTrigger.Entry { eventID = EventTriggerType.Select };

                entry.callback.AddListener(OnOptionSelect);

                button.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
            }

            selected = options[0].gameObject;
        }

        private void OnEnable()
        {
            if (EventSystem.current.TryGetComponent<InputSystemUIInputModule>(out var UIInputModule))
            {
                UIInputModule.cancel.action.performed += OnCancel;
            }

            selected.GetComponent<Button>().Select();
            selector.SetParent(selected.transform, false);
        }

        private void OnDisable()
        {
            if (EventSystem.current != null &&
                EventSystem.current.TryGetComponent<InputSystemUIInputModule>(out var UIInputModule))
            {
                UIInputModule.cancel.action.performed -= OnCancel;
            }
        }

        private void OnOptionSelect(BaseEventData baseEvent)
        {
            selector.SetParent(baseEvent.selectedObject.transform, false);

            selected = baseEvent.selectedObject;
        }

        private void OnCancel(InputAction.CallbackContext context)
        {
            General長.Back();
        }

        private void OnValidate()
        {
            Debug.Assert(content != null);
            Debug.Assert(selector != null);
        }
    }
}
