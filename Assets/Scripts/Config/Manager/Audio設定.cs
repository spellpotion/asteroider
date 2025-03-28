using UnityEngine;

namespace Asteroider
{
    [CreateAssetMenu(fileName = "Audio", menuName = "Scriptable Objects/Config/Manager/Audio")]
    public class Audio設定 : 抽象ManagerConfig<Audio長>
    {
        public AudioClip[] MusicGame = null;
        public AudioClip[] MusicMenu = null;
    }
}
