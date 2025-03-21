using UnityEngine;

namespace Asteroider
{
    public abstract class ����Ability<T, U> : ����Ability<T> where T : ����Ability<T, U> where U : ����AbilityConfig<T>
    {
        [SerializeField] protected U �ݒ� = null;

        protected virtual void OnValidate()
        {
            Debug.Assert(�ݒ� != null, $"[{typeof(T).Name}] {name}: �ݒ�");
        }
    }

    public abstract class ����Ability<T> : ����Ability where T : ����Ability<T> { }

    public abstract class ����Ability : MonoBehaviour { }
}
