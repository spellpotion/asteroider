using UnityEngine;

namespace Asteroider
{
    public class Lifetime”\ : ’ŠÛAbility<Lifetime”\, Lifetimeİ’è>
    {
        private float timeEnd;

        private void OnEnable()
        {
            timeEnd = Time.time + İ’è.Duration;
        }

        private void Update()
        {
            if (Time.time > timeEnd) gameObject.SetActive(false);
        }
    }
}
