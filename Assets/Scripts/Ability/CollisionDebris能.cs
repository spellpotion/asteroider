using UnityEngine;

namespace Asteroider
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionDebris能 : 抽象Ability<CollisionDebris能, CollisionDebris設定>
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            for (var i = 0; i < 設定.Count; i++)
            {
                var debris = Instantiate(設定.DebrisPrefab,
                    transform.position, transform.rotation);

                debris.OnDisabled.AddListener(x => Gameboard長.Remove(x));
            }

            Audio長.Play(設定.Sound);

            gameObject.SetActive(false);
        }
    }
}
