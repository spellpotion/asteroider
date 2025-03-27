using UnityEngine;

namespace Asteroider
{
    public class SelectorAnimation : MonoBehaviour
    {
        private const float distance = 30f;
        private const float duration = .8f;
        private const float duration—¦ = .2f;

        private float Šú => ‹t ? duration : duration * duration—¦;

        private Vector3 localPosition;

        private bool ‹t;
        private float timeEnd;

        private void Awake()
        {
            localPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            timeEnd = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup > timeEnd)
            {
                ‹t = !‹t;
                timeEnd += Šú;
            }

            float progress = (Šú - (timeEnd - Time.realtimeSinceStartup)) / Šú;
            float offset = ‹t ? Mathf.SmoothStep(0f, distance, progress) : distance * (1f - progress);

            transform.localPosition = localPosition + (Vector3.left * offset);
        }

    }
}