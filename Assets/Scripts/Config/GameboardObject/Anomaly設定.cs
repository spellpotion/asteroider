using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Anomaly", menuName = "Scriptable Objects/Config/Gameboard Object/Anomaly")]
    public class Anomaly設定 : 抽象GameboardObjectConfig<Anomaly>
    {
        public AudioClip Sound;
    }
}
