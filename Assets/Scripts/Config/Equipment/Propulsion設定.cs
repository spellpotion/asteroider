using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Propulsion", menuName = "Scriptable Objects/Config/Equipment/Propulsion")]
    public class Propulsion設定 : 抽象EquipmentConfig<Propulsion機器>
    {
        public AudioClip SoundActive;
    }
}
