using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroider.Manager
{
    [CreateAssetMenu(fileName = "’ŠÛLayoutConfig", menuName = "Scriptable Objects/’ŠÛManager/’ŠÛLayoutConfig")]
    public class Layoutİ’è : ’ŠÛManagerConfig<Layout’·>
    {
        [Serializable]
        public class LayoutByLayoutTypeEntry
        {
            public LayoutType LayoutType;
            public ’ŠÛLayout Layout;
        }

        [SerializeField] private List<LayoutByLayoutTypeEntry> layoutByLayoutType = new();

        public Dictionary<LayoutType, ’ŠÛLayout> LayoutByLayoutType { get; private set; }

        private void OnEnable()
        {
            LayoutByLayoutType = layoutByLayoutType.ToDictionary(
                entry => entry.LayoutType,
                entry => entry.Layout
                );
        }
    }
}