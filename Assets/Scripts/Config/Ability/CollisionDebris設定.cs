using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "CollisionDebris", menuName = "Scriptable Objects/Ability/CollisionDebris")]
    public class CollisionDebris設定 : 抽象AbilityConfig<CollisionDebris能>
    {
        public InitialMotion能 Prefab = default;
        public int Count = 0;
        public AudioClip Sound = null;
    }
}

