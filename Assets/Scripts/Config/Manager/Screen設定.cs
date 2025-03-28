using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroider.Manager
{
    [CreateAssetMenu(fileName = "Screen", menuName = "Scriptable Objects/Config/Manager/Screen")]
    public class Screen�ݒ� : ����ManagerConfig<Screen��>
    {
        [Serializable]
        public class ScreenByScreenTypeEntry
        {
            public ScreenType ScreenType;
            public ����Screen Screen;
        }

        [SerializeField] private List<ScreenByScreenTypeEntry> screenByScreenType = new();

        public Color32 Contrast�F;

        public Dictionary<ScreenType, ����Screen> ScreenByScreenType { get; private set; }

        private void OnEnable()
        {
            ScreenByScreenType = screenByScreenType.ToDictionary(
                entry => entry.ScreenType,
                entry => entry.Screen
                );
        }
    }
}