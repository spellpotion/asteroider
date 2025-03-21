using System;

namespace Asteroider
{
    public class Asteroid : GameboardObject
    {
        public static EventProxy<bool> OnCountUpdate = new(out onCountUpdate);
        private static Action<bool> onCountUpdate;

        protected virtual void OnEnable()
        {
            onCountUpdate?.Invoke(true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            onCountUpdate?.Invoke(false);
        }
    }
}