using Asteroider.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public enum LayoutType { Initial, Menu, Game }

    public class Screen長 : 抽象Manager<Screen長, Layout設定>
    {
        public static void SetScreen(LayoutType screenType)
            => Instance.SetScreen_Implementation(screenType);

        public static void ClearScreen()
            => Instance.ClearScreen_Implementation();

        public static EventProxy OnScreenClear = new(out onScreenClear);
        public static EventProxy<LayoutType> OnScreenSet = new(out onSetScreen);

        private static Action onScreenClear;
        private static Action<LayoutType> onSetScreen;

        private Canvas canvas;
        private readonly Dictionary<LayoutType, 抽象Layout> layoutsLoaded = new();

        protected void Awake()
        {
            canvas = FindFirstObjectByType<Canvas>();
        }

        private void ClearScreen_Implementation()
        {
            foreach (var screen in layoutsLoaded)
            {
                screen.Value.gameObject.SetActive(false);
            }

            onScreenClear?.Invoke();
        }

        private void SetScreen_Implementation(LayoutType screenType)
        {
            if (!layoutsLoaded.ContainsKey(screenType))
            {
                LoadScreen(screenType);
            }

            if (layoutsLoaded.TryGetValue(screenType, out 抽象Layout screen))
            {
                screen.gameObject.SetActive(true);
            }

            onSetScreen?.Invoke(screenType);
        }

        private void LoadScreen(LayoutType layoutType)
        {
            var original = 設定.LayoutByLayoutType[layoutType];
            var parent = canvas.transform;

            layoutsLoaded.Add(layoutType, Instantiate(original, parent));
        }
    }
}