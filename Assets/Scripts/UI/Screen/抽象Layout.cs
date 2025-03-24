using UnityEngine;

namespace Asteroider
{
    public abstract class ’ŠÛLayout<T, U> : ’ŠÛLayout<T> where T : ’ŠÛLayout<T, U> where U : ’ŠÛLayoutConfig<T>
    {
        [SerializeField] protected U İ’è;

        protected virtual void OnValidate()
        {
            Debug.Assert(İ’è != null, $"[{typeof(T).Name}] {name}: İ’è");
        }
    }

    public abstract class ’ŠÛLayout<T> : ’ŠÛLayout where T : ’ŠÛLayout<T> { }

    public abstract class ’ŠÛLayout : MonoBehaviour { }
}