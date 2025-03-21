using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : GameboardObject
    {
        public Rigidbody2D Body { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Body = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
            => gameObject.SetActive(false);
    }
}
