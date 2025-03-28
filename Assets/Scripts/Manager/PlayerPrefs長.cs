using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public class PlayerPrefs長 : 抽象Manager<PlayerPrefs長>
    {
        private static readonly Dictionary<string, Action<int>> callback辞典 = new();

        public static void AddListener(string key, Action<int> callback)
            => AddListener_Implementation(key, callback);
        public static void RemoveListener(string key, Action<int> callback)
            => RemoveListener_Implementation(key, callback);

        public static void Set(string key, int value)
            => Instance.Set_Implementation(key, value);

        protected override void OnEnable()
        {
            base.OnEnable();

            foreach (var pair in callback辞典)
            {
                BroadcastInt(pair.Key, PlayerPrefs.GetInt(pair.Key));
            } 
        }

        private static void AddListener_Implementation(string key, Action<int> callback)
        {
            if (callback辞典.TryGetValue(key, out var callbackOut))
            {
                callback辞典[key] = callbackOut += callback;
            }
            else
            {
                callback辞典[key] = callback;
            }

            if (Instance != null) Instance.BroadcastInt(key, PlayerPrefs.GetInt(key));
        }

        private static void RemoveListener_Implementation(string key, Action<int> callback)
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

        private void Set_Implementation(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);

            BroadcastInt(key, value);
        }

        private void BroadcastInt(string key, int value)
        {
            if (callback辞典.TryGetValue(key, out var callback))
            {
                callback.Invoke(value);
            }
        }
    }
}
