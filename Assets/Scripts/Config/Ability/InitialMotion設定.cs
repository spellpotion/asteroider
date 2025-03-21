using UnityEngine;

namespace Asteroider
{
    public enum TrajectoryType { Random, Across }

    [CreateAssetMenu(fileName = "InitialMotion", menuName = "Scriptable Objects/抽象Ability/InitialMotion")]
    public class InitialMotion設定 : 抽象AbilityConfig<InitialMotion能>
    {
        public TrajectoryType Trajectory;
        public float InitialSpeed = 0f;
        public float InitialTorqueMax = 0f;
    }
}
