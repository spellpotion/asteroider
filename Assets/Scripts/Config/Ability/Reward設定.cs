using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Reward", menuName = "Scriptable Objects/Ability/Reward")]
    public class Reward設定 : 抽象AbilityConfig<Reward能>
    {
        public int Reward = 0;
    }
}
