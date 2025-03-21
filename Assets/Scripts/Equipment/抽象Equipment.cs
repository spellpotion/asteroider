using UnityEngine;

namespace Asteroider
{
    public abstract class ����Equipment<T, U> : ����Equipment<T> where T : ����Equipment<T, U> where U : ����EquipmentConfig<T>
    {
        [SerializeField] protected U Config;

        protected virtual void OnValidate()
        {
            Debug.Assert(Config != null, $"[{typeof(T).Name}] {name}: �ݒ�");
        }
    }

    [RequireComponent(typeof(AudioSource))]
    public abstract class ����Equipment<T> : MonoBehaviour where T : ����Equipment<T>
    {
        protected AudioSource audioSource;
        protected Rigidbody2D bodyParent;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            bodyParent = GetComponentInParent<Rigidbody2D>();
        }
    }
}
