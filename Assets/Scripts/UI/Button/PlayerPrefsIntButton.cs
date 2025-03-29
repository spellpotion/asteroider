using UnityEngine;
using UnityEngine.UI;

namespace Asteroider
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Text))]
    public class PlayerPrefsIntButton : MonoBehaviour
    {
        [SerializeField] private string[] values = null;
        [SerializeField] private PlayerPrefsKey key = default(PlayerPrefsKey);

        private int ValueInt
        {
            get => PlayerPrefs’·.Get(key);
            set => PlayerPrefs’·.Set(key, value);
        }

        private string Value => values[ValueInt];

        private Button button;
        private Text text;

        private string label;

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
            ValueInt = (ValueInt +1) % values.Length;

            UpdateLabel();
        }

        private void UpdateLabel()
        {
            text.text = $"{label} <color={Screen’·.FContrast1.ToHex()}><b>{Value}</b></color>";
        }

        private void OnValidate()
        {
            Debug.Assert(values.Length > 0);
            Debug.Assert(key != PlayerPrefsKey.None);
        }
    }
}
