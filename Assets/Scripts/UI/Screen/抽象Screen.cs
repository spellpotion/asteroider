using UnityEngine;

namespace Asteroider
{
    public abstract class ’ŠÛScreen<T, U> : ’ŠÛScreen<T> where T : ’ŠÛScreen<T, U> where U : ’ŠÛScreenConfig<T>
    {
        [SerializeField] protected U İ’è;

        protected virtual void OnValidate()
        {
            Debug.Assert(İ’è != null, $"[{typeof(T).Name}] {name}: İ’è");
        }
    }

    public abstract class ’ŠÛScreen<T> : ’ŠÛScreen where T : ’ŠÛScreen<T> { }

    public abstract class ’ŠÛScreen : MonoBehaviour { }
}