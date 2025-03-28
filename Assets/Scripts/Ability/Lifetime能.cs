using UnityEngine;

namespace Asteroider
{
    public class Lifetime能 : 抽象Ability<Lifetime能, Lifetime設定>
    {
        private float timeEnd;

        private void OnEnable()
        {
            timeEnd = Time.time + 設定.Duration;
        }

        private void Update()
        {
            if (Time.time > timeEnd) gameObject.SetActive(false);
        }
    }
}
