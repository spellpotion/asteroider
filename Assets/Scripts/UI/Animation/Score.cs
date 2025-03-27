using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroider
{
    [RequireComponent(typeof(Text))]
    public class Score : MonoBehaviour
    {
        private float timeWait = 0.04f;

        private Text text;

        private int valueSet;
        private int value;

        private Coroutine updateValue;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            valueSet = 0;
            value = 0;
            
            text.text = string.Empty;
        }

        public void Set(int valueSet)
        {
            if (valueSet <= this.valueSet) return;

            this.valueSet = valueSet;

            if (updateValue != null) return;

            updateValue = StartCoroutine(UpdateValue());
        }

        private IEnumerator UpdateValue()
        {
            while (value < valueSet)
            {
                value += 10;

                text.text = value.ToString();

                yield return new WaitForSeconds(timeWait);
            }

            updateValue = null;
        }
    }
}