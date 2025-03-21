using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public class Gun機器 : 抽象Equipment<Gun機器, Gun設定>
    {
        private const float InertiaConstant = 50f;

        private readonly Queue<Projectile> bulletPool = new();

        private float timeReady = 0f;
        private bool triggered = false;

        protected override void Awake()
        {
            base.Awake();

            audioSource.clip = Config.SoundFire;
        }

        private void Update()
        {
            if (triggered && Time.time > timeReady)
            {
                Fire_Implementation();

                timeReady = Time.time + Config.Cooldown;
                triggered = false;
            }
        }

        public void Fire() => triggered = true;

        private void Fire_Implementation()
        {
            Projectile bullet;

            if (bulletPool.Count == 0)
            {
                bullet = Instantiate(
                    Config.BulletPrefab,
                    transform.position,
                    transform.rotation
                    );

                bullet.gameObject.layer = gameObject.layer;
                bullet.OnDisabled.AddListener(OnBulletDisabled);
            }
            else
            {
                bullet = bulletPool.Dequeue();

                bullet.transform.SetPositionAndRotation(
                    transform.position,
                    transform.rotation
                    );

                bullet.gameObject.SetActive(true);
            }

            bullet.Body.AddForce(bodyParent.linearVelocity * InertiaConstant);
            bullet.Body.AddRelativeForce(Vector2.up * Config.MuzzleEnergy);

            audioSource.Play();
        }

        private void OnBulletDisabled(GameboardObject bullet) => bulletPool.Enqueue((Projectile)bullet);
    }
}
