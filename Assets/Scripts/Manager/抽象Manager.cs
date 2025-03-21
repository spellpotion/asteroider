using UnityEngine;

namespace Asteroider
{
    public abstract class ’ŠÛManager<T, U> : ’ŠÛManager<T> where T : ’ŠÛManager<T, U> where U : ’ŠÛManagerConfig<T>
    {
        [SerializeField] protected U İ’è = null;

        protected virtual void OnValidate()
        {
            Debug.Assert(İ’è != null, $"[{typeof(T).Name}] {name}: İ’è");
        }
    }
    public abstract class ’ŠÛManager<T> : MonoBehaviour where T : ’ŠÛManager<T>
    {
        protected static T Instance { get; private set; } = null;

        virtual protected void OnEnable()
        {
            if (Instance != null) return;

            Instance = (T)this;
        }

        virtual protected void OnDisable()
        {
            if (Instance == this) Instance = null;
        }
    }  
}