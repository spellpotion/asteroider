using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class InitialMotion能 : 抽象Ability<InitialMotion能, InitialMotion設定>
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
            if (設定.Trajectory == TrajectoryType.Random)
            {
                body.AddForce(RandomForce(設定.InitialSpeedMin, 設定.InitialSpeedMax));
            }
            else if (設定.Trajectory == TrajectoryType.Across)
            {
                body.AddForce(HorizontalForce(設定.InitialSpeedMin, transform));
            }

            body.AddTorque(RandomTorque(設定.InitialTorqueMax));
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
                    Gameboard長.ScreenMin.y,
                    Gameboard長.ScreenMax.y
                    ));

            return (destination - (Vector2)transform.position) * magnitudeMax;
        }

        private static float RandomTorque(float torqueMax)
        {
            return Random.Range(-torqueMax, torqueMax);
        }
    }
}
