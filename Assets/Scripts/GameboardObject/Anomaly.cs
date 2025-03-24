using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(AudioSource))]
    public class Anomaly : íäè€GameboardObject<Anomaly, Anomalyê›íË>
    {
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = ê›íË.Sound;
        }

        protected virtual void OnEnable()
        {
            audioSource.Play();
        }

        protected void Update()
        {
            if (transform.position.x < Gameboardí∑.ScreenMin.x ||
                transform.position.x > Gameboardí∑.ScreenMax.x)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
