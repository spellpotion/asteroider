using UnityEngine;

namespace Asteroider
{
    public enum TrajectoryType { Random, Across }

    [CreateAssetMenu(fileName = "InitialMotion", menuName = "Scriptable Objects/Config/Ability/InitialMotion")]
    public class InitialMotion設定 : 抽象AbilityConfig<InitialMotion能>
    {
        public TrajectoryType Trajectory;
        public float InitialSpeedMin = 0f;
        public float InitialSpeedMax = 0f;
        public float InitialTorqueMax = 0f;
    }
}
