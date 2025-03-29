using UnityEngine;
using UnityEngine.UI;

namespace Asteroider
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Text))]
    public class RestoreDefaults : MonoBehaviour
    {
        private Button button;
        private Text text;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);

            text = GetComponent<Text>();
        }

        private void OnClick()
        {
            General��.RestoreDefaults();
        }

        private void Start()
        {
            text.color = Screen��.�FContrast2;
        }
    }
}
