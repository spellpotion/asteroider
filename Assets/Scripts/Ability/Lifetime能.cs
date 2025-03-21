using UnityEngine;

namespace Asteroider
{
    public class Lifetime�\ : ����Ability<Lifetime�\, Lifetime�ݒ�>
    {
        private float timeEnd;

        private void OnEnable()
        {
            timeEnd = Time.time + �ݒ�.Duration;
        }

        private void Update()
        {
            if (Time.time > timeEnd) gameObject.SetActive(false);
        }
    }
}
