using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Collider2D))]
    public class Ship : íäè€GameboardObject<Ship, Shipê›íË>
    {
        private new Collider2D collider;

        private readonly List<SpriteRenderer> sprites = new();

        private Coroutine spawnProtection;

        protected override void Awake()
        {
            base.Awake();

            collider = GetComponent<Collider2D>();

            CollectEnabledSprites();

            void CollectEnabledSprites()
            {
                foreach (var sprite in GetComponents<SpriteRenderer>())
                {
                    if (sprite.enabled) sprites.Add(sprite);
                }
                foreach (var sprite in GetComponentsInChildren<SpriteRenderer>(false))
                {
                    if (sprite.enabled) sprites.Add(sprite);
                }
            }
        }

        protected virtual void OnEnable()
        {
            spawnProtection = StartCoroutine(SpawnProtection());
        }

        protected override void OnDisable()
        {
            if (spawnProtection != null)
            {
                StopCoroutine(spawnProtection);
                spawnProtection = null;
            }

            base.OnDisable();
        }

        private IEnumerator SpawnProtection()
        {
            collider.enabled = false;

            var timeEnd = Time.time + ê›íË.DurationSpawnProtection;
            while (timeEnd > Time.time)
            {
                foreach (var sprite in sprites) sprite.enabled = !sprite.enabled;

                yield return new WaitForSeconds(.1f);
            }

            foreach (var sprite in sprites) sprite.enabled = true;

            collider.enabled = true;

            spawnProtection = null;
        }
    }
}
