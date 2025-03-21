using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Gameboard設定", menuName = "Scriptable Objects/抽象Manager/Gameboard設定")]
    public class Gameboard設定 : 抽象ManagerConfig<Gameboard長>
    {
        public float SafeDistance = 4f;
    }
}
