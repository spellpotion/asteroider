using UnityEngine;
using UnityEngine.UI;

namespace Asteroider
{
    [RequireComponent(typeof(Button))]
    public class MusicButton : MonoBehaviour
    {
        private const string labelPre = "MUSIC ";
        private const string labelPostTrue = "ON";
        private const string labelPostFalse = "OFF";

        private Button button;
        private Text text;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(onClick);

            text = GetComponent<Text>();

        }
        private void Start()
        {
            UpdateButtonLabel();
        }

        private void onClick()
        {
            Sound長.Music = !Sound長.Music;

            UpdateButtonLabel();
        }

        private void UpdateButtonLabel()
        {
            text.text = labelPre + (Sound長.Music ? labelPostTrue : labelPostFalse);
        }
    }
}