using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public class Asteroid : GameboardObject
    {
        public static EventProxy<bool> OnCountUpdate = new(out onCountUpdate);
        private static Action<bool> onCountUpdate;

        private readonly List<SpriteRenderer> renderers = new();

        private Coroutine blink;

        protected override void Awake()
        {
            base.Awake();

            renderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
        }

        protected virtual void OnEnable()
        {
            Game’·.OnGamePause.AddListener(OnGamePause);

            onCountUpdate?.Invoke(true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (blink != null)
            {
                StopCoroutine(blink);
                blink = null;
            }

            onCountUpdate?.Invoke(false);

            Game’·.OnGamePause.RemoveListener(OnGamePause);
        }

        public void Blink()
        {
            if (blink != null)
            {
                StopCoroutine(blink);
                blink = null;
            }

            blink = StartCoroutine(Blink_Implementation());
        }

        private void OnGamePause(bool gamePaused)
        {
            if (blink == null) return;

            if (!gamePaused) return;

            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
        }

        private IEnumerator Blink_Implementation()
        {
            var timeEnd = Time.time + 1f;

            while (timeEnd > Time.time)
            {
                foreach (var renderer in renderers)
                {
                    renderer.enabled = !renderer.enabled;
                }

                yield return new WaitForSeconds(.1f);
            }

            foreach (var renderer in renderers) renderer.enabled = true;

            blink = null;
        }
    }
}