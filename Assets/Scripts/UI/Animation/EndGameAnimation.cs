using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroider
{
    public class EndGameAnimation : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] Text Title;
        [SerializeField] GameObject Menu;
        [SerializeField] Button buttonExit;

        private Color colorBackground;
        private Color colorTitle;

        Coroutine appear;

        private void Awake()
        {
            colorBackground = background.color;
            colorTitle = Title.color;
        }

        private void OnEnable()
        {
            var colorBackground = background.color;
            colorBackground.a = 0f;
            background.color = colorBackground;

            var colorTitle = Title.color;
            colorTitle.a = 0f;
            Title.color = colorTitle;

            Menu.SetActive(false);

            if (appear != null)
            {
                StopCoroutine(appear);
                appear = null;
            }

            appear = StartCoroutine(Appear());
        }

        private const float durationIdle = 1f;
        private const float durationTitle = 2f;

        private IEnumerator Appear()
        {
            yield return new WaitForSeconds(durationIdle);

            var timeEnd = Time.time + durationTitle;
            var progress = 0f;
            var colorTitle = Title.color;

            while (progress < 1f)
            {
                colorTitle.a = progress * this.colorTitle.a;
                Title.color = colorTitle;

                yield return new WaitForEndOfFrame();

                progress = (durationTitle - (timeEnd - Time.time)) / durationTitle;
            }

            Title.color = this.colorTitle;
            background.color = this.colorBackground;
            Menu.SetActive(true);
            buttonExit.Select();

            appear = null;
        }
    }
}
