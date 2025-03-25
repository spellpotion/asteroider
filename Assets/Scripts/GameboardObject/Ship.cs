using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Collider2D))]
    public class Ship : íäè€GameboardObject<Ship, Shipê›íË>
    {
        private new Collider2D collider;

        private readonly List<SpriteRenderer> renderers = new();

        private Coroutine spawnProtection;

        protected override void Awake()
        {
            base.Awake();

            collider = GetComponent<Collider2D>();

            CollectEnabledSprites();

            void CollectEnabledSprites()
            {
                foreach (var sprite in GetComponentsInChildren<SpriteRenderer>(false))
                {
                    if (sprite.enabled) renderers.Add(sprite);
                }
            }
        }

        protected virtual void OnEnable()
        {
            Gameí∑.OnGamePause.AddListener(OnGamePause);

            spawnProtection = StartCoroutine(SpawnProtection());
        }

        protected override void OnDisable()
        {
            Gameí∑.OnGamePause.RemoveListener(OnGamePause);

            if (spawnProtection != null)
            {
                StopCoroutine(spawnProtection);
                spawnProtection = null;
            }

            base.OnDisable();
        }

        private void OnGamePause(bool gamePaused)
        {
            if (spawnProtection == null) return;

            if (!gamePaused) return;

            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
        }

        private IEnumerator SpawnProtection()
        {
            collider.enabled = false;

            var timeEnd = Time.time + ê›íË.DurationSpawnProtection;
            while (timeEnd > Time.time)
            {
                foreach (var renderer in renderers) renderer.enabled = !renderer.enabled;

                yield return new WaitForSeconds(.1f);
            }

            foreach (var sprite in renderers) sprite.enabled = true;

            collider.enabled = true;

            spawnProtection = null;
        }
    }
}
