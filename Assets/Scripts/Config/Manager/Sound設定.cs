using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Sound設定", menuName = "Scriptable Objects/抽象Manager/Sound設定")]
    public class Sound設定 : 抽象ManagerConfig<Sound長>
    {
        public AudioClip[] MusicGame = null;
        public AudioClip[] MusicMenu = null;
    }
}
