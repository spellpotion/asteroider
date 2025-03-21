using System;
using UnityEngine;

namespace Asteroider
{
    public abstract class ’ŠÛGameboardObject<T, U> : ’ŠÛGameboardObject<T> where T : ’ŠÛGameboardObject<T, U> where U : ’ŠÛGameboardObjectConfig<T>
    {
        [SerializeField] protected U İ’è = null;

        protected virtual void OnValidate()
        {
            Debug.Assert(İ’è != null, $"[{typeof(T).Name}] {name}: İ’è");
        }
    }
    public abstract class ’ŠÛGameboardObject<T> : GameboardObject where T : ’ŠÛGameboardObject<T> {}

    public class GameboardObject : MonoBehaviour
    {
        public EventProxy<GameboardObject> OnDisabled;
        private Action<GameboardObject> onDisabled;

        protected virtual void Awake()
        {
            OnDisabled = new(out onDisabled);
        }

        protected virtual void Start()
        {
            Gameboard’·.Add(this);
        }

        protected virtual void OnDisable() => onDisabled?.Invoke(this);
    }
}
