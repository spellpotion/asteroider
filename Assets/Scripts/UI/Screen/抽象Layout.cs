using UnityEngine;

namespace Asteroider
{
    public abstract class ����Layout<T, U> : ����Layout<T> where T : ����Layout<T, U> where U : ����LayoutConfig<T>
    {
        [SerializeField] protected U �ݒ�;

        protected virtual void OnValidate()
        {
            Debug.Assert(�ݒ� != null, $"[{typeof(T).Name}] {name}: �ݒ�");
        }
    }

    public abstract class ����Layout<T> : ����Layout where T : ����Layout<T> { }

    public abstract class ����Layout : MonoBehaviour { }
}