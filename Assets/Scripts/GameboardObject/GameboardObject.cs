using System;
using UnityEngine;

namespace Asteroider
{
    public abstract class ����GameboardObject<T, U> : ����GameboardObject<T> where T : ����GameboardObject<T, U> where U : ����GameboardObjectConfig<T>
    {
        [SerializeField] protected U �ݒ� = null;

        protected virtual void OnValidate()
        {
            Debug.Assert(�ݒ� != null, $"[{typeof(T).Name}] {name}: �ݒ�");
        }
    }
    public abstract class ����GameboardObject<T> : GameboardObject where T : ����GameboardObject<T> {}

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
            Gameboard��.Add(this);
        }

        protected virtual void OnDisable() => onDisabled?.Invoke(this);
    }
}
