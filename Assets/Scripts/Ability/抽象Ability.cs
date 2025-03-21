using UnityEngine;

namespace Asteroider
{
    public abstract class ’ŠÛAbility<T, U> : ’ŠÛAbility<T> where T : ’ŠÛAbility<T, U> where U : ’ŠÛAbilityConfig<T>
    {
        [SerializeField] protected U İ’è = null;

        protected virtual void OnValidate()
        {
            Debug.Assert(İ’è != null, $"[{typeof(T).Name}] {name}: İ’è");
        }
    }

    public abstract class ’ŠÛAbility<T> : ’ŠÛAbility where T : ’ŠÛAbility<T> { }

    public abstract class ’ŠÛAbility : MonoBehaviour { }
}
