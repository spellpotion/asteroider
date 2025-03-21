using UnityEngine;

namespace Asteroider
{
    public class Sound長 : 抽象Manager<Sound長, Sound設定>
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource[] soundSources;

        public static void Play(AudioClip audioClip)
            => Instance.PlayOneShot_Implementation(audioClip);

        public static bool Music
        { 
            get => !Instance.musicSource.mute;
            set
            {
                Instance.musicSource.mute = !value;
                PlayerPrefs.SetInt("music", value ? 1 : 0);
            }
        }

        protected void Awake()
        {
            musicSource = GetComponent<AudioSource>();

            musicSource.mute = PlayerPrefs.GetInt("music", 1) != 1;
        }

        override protected void OnEnable()
        {
            base.OnEnable();

            Layout長.OnLayout終.AddListener(OnLayout終);
            Layout長.OnLayout始.AddListener(OnLayout始);
        }

        override protected void OnDisable()
        {
            Layout長.OnLayout終.RemoveListener(OnLayout終);
            Layout長.OnLayout始.RemoveListener(OnLayout始);

            base.OnDisable();
        }

        private void OnLayout終(LayoutType layoutType) => musicSource.Stop();

        private void OnLayout始(LayoutType layoutType)
        {
            if (layoutType == LayoutType.Game)
            {
                musicSource.clip = 設定.MusicGame[Random.Range(0, 設定.MusicGame.Length)];
                musicSource.Play();
            }
            if (layoutType == LayoutType.Menu)
            {
                musicSource.clip = 設定.MusicMenu[Random.Range(0, 設定.MusicMenu.Length)];
                musicSource.Play();
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