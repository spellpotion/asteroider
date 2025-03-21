using Asteroider.UI;
using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "GameLayout設定", menuName = "Scriptable Objects/UI/GameLayout設定")]
    public class GameLayout設定 : 抽象LayoutConfig<Game設計>
    {
        public Sprite[] Numerals;
    }
}
