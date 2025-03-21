using Asteroider.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public enum LayoutType { Initial, Menu, Game }

    public class Layout長 : 抽象Manager<Layout長, Layout設定>
    {
        private static LayoutType layoutType;

        public static void ChangeScreen(LayoutType layoutType)
            => Instance.ChangeScreen_Implementation(layoutType);

        public static EventProxy<LayoutType> OnLayout終 = new(out onLayout終);
        public static EventProxy<LayoutType> OnLayout始 = new(out onLayout始);

        private static Action<LayoutType> onLayout終;
        private static Action<LayoutType> onLayout始;

        private Canvas canvas;
        private readonly Dictionary<LayoutType, 抽象Layout> layoutsLoaded = new();

        protected void Awake()
        {
            canvas = FindFirstObjectByType<Canvas>();
        }

        private void ChangeScreen_Implementation(LayoutType layoutType)
        {
            Debug.Log($"[{GetType().Name}] 終 {Layout長.layoutType}");
            onLayout終?.Invoke(Layout長.layoutType);

            SelectScreen(layoutType);

            Debug.Log($"[{GetType().Name}] 始 {Layout長.layoutType}");
            onLayout始?.Invoke(Layout長.layoutType);
        }

        private void SelectScreen(LayoutType layoutType)
        {
            if (!layoutsLoaded.ContainsKey(layoutType))
            {
                LoadScreen(layoutType);
            }

            foreach (var screen in layoutsLoaded)
            {
                screen.Value.gameObject.SetActive(screen.Key == layoutType);
            }

            Layout長.layoutType = layoutType;
        }

        private void LoadScreen(LayoutType layoutType)
        {
            var original = 設定.LayoutByLayoutType[layoutType];
            var parent = canvas.transform;

            layoutsLoaded.Add(layoutType, Instantiate(original, parent));
        }
    }
}