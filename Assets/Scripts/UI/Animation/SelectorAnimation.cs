using UnityEngine;

namespace Asteroider
{
    public class SelectorAnimation : MonoBehaviour
    {
        private const float distance = 30f;
        private const float duration = .8f;
        private const float duration�� = .2f;

        private float �� => �t ? duration : duration * duration��;

        private Vector3 localPosition;

        private bool �t;
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
                �t = !�t;
                timeEnd += ��;
            }

            float progress = (�� - (timeEnd - Time.realtimeSinceStartup)) / ��;
            float offset = �t ? Mathf.SmoothStep(0f, distance, progress) : distance * (1f - progress);

            transform.localPosition = localPosition + (Vector3.left * offset);
        }

    }
}