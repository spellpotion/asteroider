using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InitialMotionî\ : íäè€Ability<InitialMotionî\, InitialMotionê›íË>
    {
        private Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            AddRandomForceAndTorque();
        }

        private void AddRandomForceAndTorque()
        {
            if (ê›íË.Trajectory == TrajectoryType.Random)
            {
                body.AddForce(RandomForce(ê›íË.InitialSpeed));
            }
            else if (ê›íË.Trajectory == TrajectoryType.Across)
            {
                body.AddForce(HorizontalForce(ê›íË.InitialSpeed, transform));
            }

            body.AddTorque(RandomTorque(ê›íË.InitialTorqueMax));
        }

        private static Vector2 RandomForce(float magnitudeMax)
        {
            var magnitude = Random.Range(0f, magnitudeMax);
            var angleDegrees = Random.Range(0f, 360f);
            var angleRadians = angleDegrees * Mathf.Deg2Rad;

            return new Vector2(
                magnitude * Mathf.Cos(angleRadians),
                magnitude * Mathf.Sin(angleRadians)
            );
        }

        private static Vector2 HorizontalForce(float magnitudeMax, Transform transform)
        {
            var destination = new Vector2(-transform.position.x,
                Random.Range(
                    Gameboardí∑.ScreenMin.y,
                    Gameboardí∑.ScreenMax.y
                    ));

            return (destination - (Vector2)transform.position) * magnitudeMax;
        }

        private static float RandomTorque(float torqueMax)
        {
            return Random.Range(-torqueMax, torqueMax);
        }
    }
}
