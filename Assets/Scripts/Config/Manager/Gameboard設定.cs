using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Gameboard", menuName = "Scriptable Objects/Config/Manager/Gameboard")]
    public class Gameboard設定 : 抽象ManagerConfig<Gameboard長>
    {
        public float SafeDistance = 4f;
    }
}
