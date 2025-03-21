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

        void OnThrust(InputValue value) => propulsion.Active = value.isPressed;

        void OnRotate(InputValue value) => propulsion.Rotate = value.Get<float>();

        void OnShoot() => gun.Fire();
    }
}