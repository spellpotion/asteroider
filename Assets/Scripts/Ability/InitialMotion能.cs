using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InitialMotion�\ : ����Ability<InitialMotion�\, InitialMotion�ݒ�>
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
            if (�ݒ�.Trajectory == TrajectoryType.Random)
            {
                body.AddForce(RandomForce(�ݒ�.InitialSpeedMin, �ݒ�.InitialSpeedMax));
            }
            else if (�ݒ�.Trajectory == TrajectoryType.Across)
            {
                body.AddForce(HorizontalForce(�ݒ�.InitialSpeedMin, transform));
            }

            body.AddTorque(RandomTorque(�ݒ�.InitialTorqueMax));
        }

        private static Vector2 RandomForce(float magnitudeMin, float magnitudeMax)
        {
            var magnitude = Random.Range(magnitudeMin, magnitudeMax);
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
                    Gameboard��.ScreenMin.y,
                    Gameboard��.ScreenMax.y
                    ));

            return (destination - (Vector2)transform.position) * magnitudeMax;
        }

        private static float RandomTorque(float torqueMax)
        {
            return Random.Range(-torqueMax, torqueMax);
        }
    }
}
