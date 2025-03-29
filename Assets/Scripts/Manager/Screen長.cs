using Asteroider.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public enum ScreenType { Initial, Menu, Game, Settings }

    public class Screen長 : 抽象Manager<Screen長, Screen設定>
    {
        public static void OpenNew(ScreenType screenType) 
            => Instance.OpenNew_Implementation(screenType);

        public static void OpenAdd(ScreenType screenType)
            => Instance.OpenAdd_Implementation(screenType);

        public static void Back() => Instance.Back_Implementation();
        public static void Clear() => Instance.Clear_Implementation();

        public static EventProxy OnClear = new(out onClear);
        public static EventProxy<ScreenType> OnOpen = new(out onOpen);

        public static Color32 色Contrast1 => Instance.設定.Contrast色1;
        public static Color32 色Contrast2 => Instance.設定.Contrast色2;

        private static Action<ScreenType> onOpen;
        private static Action onClear;

        private Canvas canvas;
        
        private readonly Dictionary<ScreenType, 抽象Screen> screensLoaded = new();
        private readonly Stack<ScreenType> screenSequence = new();

        protected void Awake()
        {
            canvas = FindFirstObjectByType<Canvas>();
        }

        private void Clear_Implementation()
        {
            foreach (var screen in screensLoaded)
            {
                screen.Value.gameObject.SetActive(false);
            }

            onClear?.Invoke();
        }

        private void OpenNew_Implementation(ScreenType screenType)
        {
            SetActive(screenType);

            screenSequence.Clear();
            screenSequence.Push(screenType);

            onOpen?.Invoke(screenType);
        }

        private void OpenAdd_Implementation(ScreenType screenType)
        {
            SetActive(screenType);

            screenSequence.Push(screenType);

            onOpen?.Invoke(screenType);
        }

        private void Back_Implementation()
        {
            screenSequence.Pop();

            var screenType = screenSequence.Peek();

            SetActive(screenType);

            onOpen?.Invoke(screenType);
        }

        private void SetActive(ScreenType screenType)
        {
            LoadScreen(screenType);

            if (screensLoaded.TryGetValue(screenType, out 抽象Screen screen))
            {
                screen.gameObject.SetActive(true);
            }
        }

        private void LoadScreen(ScreenType layoutType)
        {
            if (screensLoaded.ContainsKey(layoutType)) return;

            var original = 設定.ScreenByScreenType[layoutType];
            var parent = canvas.transform;

            screensLoaded.Add(layoutType, Instantiate(original, parent));
        }
    }
}