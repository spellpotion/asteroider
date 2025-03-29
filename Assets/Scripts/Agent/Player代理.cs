using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroider
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player代理 : 抽象Agent
    {
        private Gun機器 gun;
        private Propulsion機器 propulsion;

        private void Awake()
        {
            gun = GetComponentInChildren<Gun機器>();
            propulsion = GetComponentInChildren<Propulsion機器>();
        }

        private void Start()
        {
            GetComponent<PlayerInput>().
                SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
        }

        void OnThrust(InputValue value) => propulsion.Active = value.isPressed;

        void OnRotate(InputValue value) => propulsion.Rotate = value.Get<float>();

        void OnShoot() => gun.Fire();
    }
}