using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace Asteroider
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Text))]
    public class RemapButton : MonoBehaviour
    {
        [SerializeField] private InputActionReference actionReference;
        [SerializeField] private int option;

        private string Binding
            => actionReference.action.GetBindingDisplayString(option);

        private Button button;
        private Text text;

        private string label;

        private Coroutine catchKey;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);

            text = GetComponent<Text>();

            label = (text.text).Trim();
        }

        private void Start()
        {
            UpdateLabel();
        }

        private void OnClick()
        {
            if (catchKey != null) return;

            catchKey = StartCoroutine(CatchKey());
        }

        private IEnumerator CatchKey()
        {
            button.interactable = false;

            yield return new WaitUntil(() => !Keyboard.current.anyKey.isPressed);

            yield return new WaitUntil(() => Keyboard.current.anyKey.wasPressedThisFrame);

            foreach (var keyControl in Keyboard.current.allKeys)
            {
                if (keyControl.wasPressedThisFrame)
                {
                    SetKeyBinding(keyControl);
                    break;
                }
            }

            UpdateLabel();

            yield return new WaitUntil(() => !Keyboard.current.anyKey.isPressed);

            button.interactable = true;
            button.Select();

            catchKey = null;
        }

        private void SetKeyBinding(KeyControl keyControl)
        {
            string keyPath = keyControl.path;

            var action = actionReference.action;
            if (option >= action.bindings.Count)
            {
                return;
            }

            action.ApplyBindingOverride(option, keyPath);

        }

        private void UpdateLabel()
        {
            text.text = $"{label} <color={Screen’·.ColorHexContrast}><b>{Binding.ToUpper()}</b></color>";
        }

        private void OnValidate()
        {
            Debug.Assert(actionReference != null);
        }
    }
}
