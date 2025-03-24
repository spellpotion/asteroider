using Asteroider.UI;
using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Screen", menuName = "Scriptable Objects/Config/Screen/Game")]
    public class GameLayout設定 : 抽象LayoutConfig<Game設計>
    {
        public Sprite[] Numerals;
    }
}
