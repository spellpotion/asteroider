﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Asteroider.UI
{
    public class Menu画面 : 抽象Screen
    {
        [SerializeField] private Transform menu = null;
        [SerializeField] private Transform selector = null;

        private GameObject selected;

        private void Awake()
        {
            var options = menu.GetComponentsInChildren<Button>();

            foreach (var button in options)
            {
                var entry = new EventTrigger.Entry { eventID = EventTriggerType.Select };

                entry.callback.AddListener(OnOptionSelect);

                button.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
            }

            selected = options[0].gameObject;
        }

        protected virtual void OnEnable()
        {
            selected.GetComponent<Button>().Select();
            selector.SetParent(selected.transform, false);
        }

        private void OnOptionSelect(BaseEventData baseEvent)
        {
            selector.SetParent(baseEvent.selectedObject.transform, false);

            selected = baseEvent.selectedObject;
        }

        private void OnValidate()
        {
            Debug.Assert(selector != null);
            Debug.Assert(menu != null);
        }
    }
}