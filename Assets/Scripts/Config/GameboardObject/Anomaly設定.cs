using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Anomaly", menuName = "Scriptable Objects/GameboardObject/Anomaly")]
    public class Anomaly設定 : 抽象GameboardObjectConfig<Anomaly>
    {
        public AudioClip Sound;
    }
}
