using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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

        private void OnEnable()
        {
            PlayerPrefs’·.OnUpdate.AddListener(UpdateLabel);
        }

        private void OnDisable()
        {
            PlayerPrefs’·.OnUpdate.RemoveListener(UpdateLabel);
        }

        private void Start()
        {
            UpdateLabel();
        }

        private void OnClick()
        {
            if (catchKey != null) return;

            text.text = label;

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
                    actionReference.action.ApplyBindingOverride(option, keyControl.path);
                    break;
                }
            }

            UpdateLabel();

            yield return new WaitUntil(() => !Keyboard.current.anyKey.isPressed);

            button.interactable = true;
            button.Select();

            catchKey = null;
        }

        private void UpdateLabel()
        {
            text.text = $"{label} <color={Screen’·.FContrast1.ToHex()}><b>{Binding.ToUpper()}</b></color>";
        }

        private void OnValidate()
        {
            Debug.Log(actionReference.action.bindings[option].path);
            Debug.Assert(actionReference != null);
        }
    }
}
