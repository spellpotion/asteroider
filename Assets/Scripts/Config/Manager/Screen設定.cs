using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroider.Manager
{
    [CreateAssetMenu(fileName = "Screen", menuName = "Scriptable Objects/Config/Manager/Screen")]
    public class Screenİ’è : ’ŠÛManagerConfig<Screen’·>
    {
        [Serializable]
        public class LayoutByLayoutTypeEntry
        {
            public LayoutType LayoutType;
            public ’ŠÛScreen Layout;
        }

        [SerializeField] private List<LayoutByLayoutTypeEntry> layoutByLayoutType = new();

        public Dictionary<LayoutType, ’ŠÛScreen> LayoutByLayoutType { get; private set; }

        private void OnEnable()
        {
            LayoutByLayoutType = layoutByLayoutType.ToDictionary(
                entry => entry.LayoutType,
                entry => entry.Layout
                );
        }
    }
}