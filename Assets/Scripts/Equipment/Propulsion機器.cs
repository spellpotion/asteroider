using UnityEngine;

namespace Asteroider
{
    public class Propulsion機器 : 抽象Equipment<Propulsion機器, Propulsion設定>
    {
        private const float CorrectionConstant = 6f;

        public bool Active { get; set; }
        public float Rotate { get; set; }

        private bool active;

        private SpriteRenderer rendererShip;
        private new SpriteRenderer renderer;

        protected override void Awake()
        {
            base.Awake();

            audioSource.clip = Config.SoundActive;
            renderer = GetComponent<SpriteRenderer>();
            rendererShip = transform.parent.GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            renderer.enabled = (active && rendererShip.isVisible);
        }

        private void FixedUpdate()
        {
            if (Active != active)
            {
                active = Active;

                if (active) audioSource.Play();
            }
            if (active)
            {
                bodyParent.AddRelativeForce(Vector2.up * CorrectionConstant);
            }

            if (Rotate != 0)
            {
                bodyParent.freezeRotation = true;
                bodyParent.transform.Rotate(Vector3.back, Rotate * CorrectionConstant);
                bodyParent.freezeRotation = false;
            }
        }
    }
}
