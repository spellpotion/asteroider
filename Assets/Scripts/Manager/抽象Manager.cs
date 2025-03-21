using UnityEngine;

namespace Asteroider
{
    public abstract class ����Manager<T, U> : ����Manager<T> where T : ����Manager<T, U> where U : ����ManagerConfig<T>
    {
        [SerializeField] protected U �ݒ� = null;

        protected virtual void OnValidate()
        {
            Debug.Assert(�ݒ� != null, $"[{typeof(T).Name}] {name}: �ݒ�");
        }
    }
    public abstract class ����Manager<T> : MonoBehaviour where T : ����Manager<T>
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