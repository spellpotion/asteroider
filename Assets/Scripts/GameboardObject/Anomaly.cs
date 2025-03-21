using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(AudioSource))]
    public class Anomaly : ����GameboardObject<Anomaly, Anomaly�ݒ�>
    {
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = �ݒ�.Sound;
        }

        protected virtual void OnEnable()
        {
            audioSource.Play();
        }

        // TODO LateUpdate
    }
}
