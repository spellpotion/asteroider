using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Objects/Config/Equipment/Gun")]
    public class Gun設定 : 抽象EquipmentConfig<Gun機器>
    {
        public AudioClip SoundFire;

        public Projectile BulletPrefab = null;
        public float MuzzleEnergy = 200f;
        public float Cooldown = .2f;
    }
}