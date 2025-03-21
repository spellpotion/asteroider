using UnityEngine;

namespace Asteroider
{
    public abstract class ’ŠÛEquipment<T, U> : ’ŠÛEquipment<T> where T : ’ŠÛEquipment<T, U> where U : ’ŠÛEquipmentConfig<T>
    {
        [SerializeField] protected U Config;

        protected virtual void OnValidate()
        {
            Debug.Assert(Config != null, $"[{typeof(T).Name}] {name}: İ’è");
        }
    }

    [RequireComponent(typeof(AudioSource))]
    public abstract class ’ŠÛEquipment<T> : MonoBehaviour where T : ’ŠÛEquipment<T>
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
