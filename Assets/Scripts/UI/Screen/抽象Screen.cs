using UnityEngine;

namespace Asteroider
{
    public abstract class ����Screen<T, U> : ����Screen<T> where T : ����Screen<T, U> where U : ����ScreenConfig<T>
    {
        [SerializeField] protected U �ݒ�;

        protected virtual void OnValidate()
        {
            Debug.Assert(�ݒ� != null, $"[{typeof(T).Name}] {name}: �ݒ�");
        }
    }

    public abstract class ����Screen<T> : ����Screen where T : ����Screen<T> { }

    public abstract class ����Screen : MonoBehaviour { }
}