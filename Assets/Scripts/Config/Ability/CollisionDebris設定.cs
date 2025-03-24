using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Collision Debris", menuName = "Scriptable Objects/Config/Ability/Collision Debris")]
    public class CollisionDebris設定 : 抽象AbilityConfig<CollisionDebris能>
    {
        public InitialMotion能 Prefab = default;
        public int Count = 0;
        public AudioClip Sound = null;
    }
}

