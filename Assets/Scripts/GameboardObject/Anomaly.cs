using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(AudioSource))]
    public class Anomaly : 抽象GameboardObject<Anomaly, Anomaly設定>
    {
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = 設定.Sound;
        }

        protected virtual void OnEnable()
        {
            audioSource.Play();
        }

        protected void Update()
        {
            if (transform.position.x < Gameboard長.ScreenMin.x ||
                transform.position.x > Gameboard長.ScreenMax.x)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
