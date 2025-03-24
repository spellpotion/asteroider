using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Config/Gameboard Object/Enemy")]
    public class Enemy設定 : 抽象Config
    {
        public float Speed = 80f;
        public float FireInterval = 1f;
        public float BehaviorChangeWaitMax = 5f;
    }
}
