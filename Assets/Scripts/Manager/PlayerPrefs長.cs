using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroider
{
    public enum PlayerPrefsKey { None, Music, Sound, AutoFire }

    public static class PrefsExtensions
    {
        public static string String(this PlayerPrefsKey pref) => pref switch
        {
            PlayerPrefsKey.Music => "music",
            PlayerPrefsKey.Sound => "sound",
            PlayerPrefsKey.AutoFire => "auto-fire",
            _ => pref.ToString().ToLowerInvariant()
        };
    }

    public class PlayerPrefs長 : 抽象Manager<PlayerPrefs長>
    {
        private const string keyInputBindings = "inputBindings";

        [SerializeField] private InputActionAsset inputActionAsset;

        private static readonly Dictionary<PlayerPrefsKey, Action<int>> callback辞典 = new();

        public static EventProxy OnUpdate = new(out onUpdate);

        public static void AddListener(PlayerPrefsKey key, Action<int> callback)
            => AddListener_Implementation(key, callback);
        public static void RemoveListener(PlayerPrefsKey key, Action<int> callback)
            => RemoveListener_Implementation(key, callback);

        public static void Set(PlayerPrefsKey key, int value)
            => Instance.Set_Implementation(key, value);

        public static int Get(PlayerPrefsKey key)
            => Instance.Get_Implementation(key);

        public static void RestoreDefaults()
            => Instance.RestoreDefaults_Implementation();

        private static Action onUpdate;

        protected override void OnEnable()
        {
            base.OnEnable();

            BroadcastIntAll();

            LoadInputBindings();
        }

        private void BroadcastIntAll()
        {
            foreach (var pair in callback辞典)
            {
                BroadcastInt(pair.Key, PlayerPrefs.GetInt(pair.Key.String()));
            }
        }

        protected override void OnDisable()
        {
            SaveInputBindings();

            base.OnDisable();
        }

        private void LoadInputBindings()
        {
            if (PlayerPrefs.HasKey(keyInputBindings))
            {
                inputActionAsset.LoadBindingOverridesFromJson(PlayerPrefs.GetString(keyInputBindings));
            }
        }

        private void SaveInputBindings()
        {
            PlayerPrefs.SetString(keyInputBindings, inputActionAsset.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }

        private void RestoreDefaults_Implementation()
        {
            foreach (PlayerPrefsKey key in Enum.GetValues(typeof(PlayerPrefsKey)))
            {
                PlayerPrefs.DeleteKey(key.String());
            }

            PlayerPrefs.DeleteKey(keyInputBindings);
            inputActionAsset.RemoveAllBindingOverrides();

            BroadcastIntAll();

            onUpdate?.Invoke();
        }

        private static void AddListener_Implementation(PlayerPrefsKey key, Action<int> callback)
        {
            if (key == PlayerPrefsKey.None) return;

            if (callback辞典.TryGetValue(key, out var callbackOut))
            {
                callback辞典[key] = callbackOut += callback;
            }
            else
            {
                callback辞典[key] = callback;
            }

            if (Instance != null) Instance.BroadcastInt(key, PlayerPrefs.GetInt(key.String()));
        }

        private static void RemoveListener_Implementation(PlayerPrefsKey key, Action<int> callback)
        {
            if (callback辞典.TryGetValue(key, out var callbackOut))
            {
                callbackOut -= callback;

                if (callbackOut == null)
                {
                    callback辞典.Remove(key);
                }
                else
                {
                    callback辞典[key] = callbackOut;
                }
            }
        }

        private void Set_Implementation(PlayerPrefsKey key, int value)
        {
            if (key == PlayerPrefsKey.None) return;

            PlayerPrefs.SetInt(key.String(), value);

            BroadcastInt(key, value);
        }

        private int Get_Implementation(PlayerPrefsKey key)
        {
            return PlayerPrefs.GetInt(key.String());
        }

        private void BroadcastInt(PlayerPrefsKey key, int value)
        {
            if (callback辞典.TryGetValue(key, out var callback))
            {
                callback.Invoke(value);
            }
        }

        private void OnValidate()
        {
            Debug.Assert(inputActionAsset != null);
        }
    }
}
