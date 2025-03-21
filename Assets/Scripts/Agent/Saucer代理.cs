using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Saucerë„óù : íäè€Agent
    {
        [SerializeField] private Enemyê›íË Config;
        [SerializeField] private Transform pivot = null;

        private Rigidbody2D body;
        private Gunã@äÌ gun;

        private float timeFire;
        private float timeChangeDirection;
        private bool direction;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            gun = GetComponentInChildren<Gunã@äÌ>();
        }

        protected void OnEnable()
        {
            timeFire = Time.time + Config.FireInterval;
            direction = transform.position.x < 0f;

            body.linearVelocity = direction ? Vector2.left : Vector2.right;

            ChangeDirection();
        }

        private void ChangeDirection()
        {
            Vector2 force;
            do
            {
                force = new Vector2(
                    Random.Range(direction ? 0 : -1, direction ? 2 : 1),
                    Random.Range(-1, 2));
            } while (force == Vector2.zero || Vector2.Dot(force, body.linearVelocity) == 1);

            force *= Config.Speed;
            Vector2.ClampMagnitude(force, Config.Speed);

            body.linearVelocity = Vector2.zero;
            body.AddForce(force);

            timeChangeDirection = Time.time +
                Random.Range(0f, Config.BehaviorChangeWaitMax);
        }

        private void Fire()
        {
            var angle = Random.Range(0f, 360f);
            pivot.Rotate(Vector3.forward, angle);
            gun.Fire();

            timeFire = Time.time + Config.FireInterval;
        }

        private void Update()
        {
            if (Time.time > timeChangeDirection) ChangeDirection();
            if (Time.time > timeFire) Fire();
        }

        private void OnValidate()
        {
            Debug.Assert(pivot != null);
        }
    }
}
