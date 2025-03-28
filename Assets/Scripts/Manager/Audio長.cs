using UnityEngine;

namespace Asteroider
{
    public enum MusicType { Menu, Game }

    public class Audio長 : 抽象Manager<Audio長, Audio設定>
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource[] soundSources;

        public static void Play(AudioClip audioClip)
            => Instance.PlayOneShot_Implementation(audioClip);

        public static void PlayMusic(MusicType musicType)
            => Instance.PlayMusic_Implementation(musicType);

        public static void StopMusic() => Instance.musicSource.Stop();

        protected void Awake()
        {
            musicSource = GetComponent<AudioSource>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            PlayerPrefs長.AddListener("music", OnMusic);
        }

        protected override void OnDisable()
        {
            PlayerPrefs長.RemoveListener("music", OnMusic);

            base.OnDisable();
        }

        private void OnMusic(int value)
        {
            musicSource.mute = value != 1;
        }

        private void PlayMusic_Implementation(MusicType musicType)
        {
            if (musicType == MusicType.Menu)
            {
                musicSource.clip = 設定.MusicMenu[Random.Range(0, 設定.MusicMenu.Length)];
                musicSource.Play();
                return;
            }
            if (musicType == MusicType.Game)
            {
                musicSource.clip = 設定.MusicGame[Random.Range(0, 設定.MusicGame.Length)];
                musicSource.Play();
                return;
            }
        }

        private void PlayOneShot_Implementation(AudioClip audioClip)
        {
            foreach (var soundSource in soundSources)
            {
                if (!soundSource.isPlaying)
                {
                    soundSource.clip = audioClip;
                    soundSource.Play();
                    return;
                }
            }
        }
    }
}