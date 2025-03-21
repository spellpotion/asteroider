using System;
using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Collider2D))]
    public class Reward能 : 抽象Ability<Reward能, Reward設定>
    {
        public static EventProxy<int> OnReward = new(out onReward);
        private static Action<int> onReward;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ship"))
            {
                onReward?.Invoke(設定.Reward);
            }
        }
    }
}
