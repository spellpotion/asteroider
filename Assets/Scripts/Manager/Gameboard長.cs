using System.Collections.Generic;
using UnityEngine;

namespace Asteroider
{
    public class Gameboard長 : 抽象Manager<Gameboard長, Gameboard設定>
    {
        public static Transform Gameboard => Instance.gameBoard;
        public static Vector3 ScreenMin => Instance.screenMin;
        public static Vector3 ScreenMax => Instance.screenMax;

        private Vector3 screenMin;
        private Vector3 screenMax;

        private new Camera camera;
        private float width;
        private float height;

        private Transform gameBoard;

        private readonly List<GameboardObject> gameboardObjects = new();
        private readonly List<SpriteRenderer> spriteRenderers = new();

        private readonly Dictionary<GameboardObject, SpriteRenderer[]> spriteRenderers辞典 = new();
        private readonly Dictionary<SpriteRenderer, GameObject[]> duplicates辞典 = new();

        public static void Clear() => Instance.Clear_Implementation();

        public static void Add(GameboardObject gameboardObject)
            => Instance.Add_Implementation(gameboardObject);

        public static void Remove(GameboardObject gameboardObject)
            => Instance?.Remove_Implementation(gameboardObject);

        public static Vector3 GetRandomPositionInside()
            => Instance.GetRandomPositionInside_Implementation();

        public static Vector3 GetRandomPositionOnTheEdge()
            => Instance.GetRandomPositionOnEdge_Implementation();

        private void Awake()
        {
            camera = Camera.main;
        
            screenMin = camera.ViewportToWorldPoint(Vector3.zero);
            screenMax = camera.ViewportToWorldPoint(Vector3.one);
            width = screenMax.x - screenMin.x;
            height = screenMax.y - screenMin.y;

            gameBoard = (new GameObject("Game Board")).transform;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private void LateUpdate()
        {
            foreach (var gameboardObject in gameboardObjects)
            {
                WrapPosition(gameboardObject.transform);
            }

            foreach (var spriteRenderer in spriteRenderers)
            {
                SetDuplicatesPositionAndRotation(spriteRenderer);
            }
        }

        private void Clear_Implementation()
        {
            for (var i = gameboardObjects.Count - 1; i >= 0; i--)
            {
                Remove_Implementation(gameboardObjects[i]);
            }
        }

        private void Add_Implementation(GameboardObject gameboardObject)
        {
            gameboardObject.transform.SetParent(gameBoard);

            gameboardObjects.Add(gameboardObject);

            var renderers = gameboardObject.GetComponentsInChildren<SpriteRenderer>(true);

            spriteRenderers辞典.Add(gameboardObject, renderers);
            spriteRenderers.AddRange(renderers);

            foreach (var renderer in renderers)
            {
                var duplicates = CreateSpriteDuplicates(renderer);

                duplicates辞典.Add(renderer, duplicates);
            }

            GameObject[] CreateSpriteDuplicates(SpriteRenderer renderer)
            {
                GameObject[] duplicates = new GameObject[gameboardObject is Anomaly ? 2 : 8];

                duplicates[0] = new GameObject($"{gameboardObject.name} {renderer.gameObject.name} Duplicate");
                duplicates[0].transform.SetParent(gameBoard);
                duplicates[0].transform.localScale = renderer.transform.localScale;

                var component = duplicates[0].AddComponent<SpriteRenderer>();
                component.sprite = renderer.sprite;
                component.color = renderer.color;
                component.sortingOrder = renderer.sortingOrder;

                for (var i = 1; i < duplicates.Length; i++)
                {
                    duplicates[i] = Instantiate(duplicates[0]);
                    duplicates[i].name = $"{gameboardObject.name} {renderer.gameObject.name} Duplicate ({i + 1})";
                    duplicates[i].transform.SetParent(gameBoard);
                }

                return duplicates;
            }
        }

        private void Remove_Implementation(GameboardObject gameboardObject)
        {
            gameboardObjects.Remove(gameboardObject);

            foreach (var renderer in spriteRenderers辞典[gameboardObject])
            {
                var duplicates = duplicates辞典[renderer];

                for (var i = duplicates.Length - 1; i >= 0; i--)
                {
                    Destroy(duplicates[i]);
                }

                duplicates辞典.Remove(renderer);
            }
            spriteRenderers辞典.Remove(gameboardObject);

            Destroy(gameboardObject.gameObject);
        }

        private static readonly (int, int)[] offsets = { (0, -1), (0, 1), (-1, -1), (-1, 0), (-1, 1),  (1, -1), (1, 0), (1, 1) };

        private void SetDuplicatesPositionAndRotation(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == null) return;

            var duplicates = duplicates辞典[spriteRenderer];

            spriteRenderer.transform.GetPositionAndRotation(out var positionBase, out var rotation);

            for (var i = 0; i < duplicates.Length; i++)
            {
                var offset = offsets[i];
                var position = new Vector3(
                    positionBase.x + offset.Item1 * width,
                    positionBase.y + offset.Item2 * height,
                    positionBase.z);
                duplicates[i].transform.SetPositionAndRotation(position, rotation);

                duplicates[i].SetActive(spriteRenderer.enabled);
            }
        }

        private void WrapPosition(Transform transform)
        {
            var position = transform.position;

            if (transform.position.x < screenMin.x)
            {
                position += Vector3.right * width;
            }
            if (transform.position.x > screenMax.x)
            {
                position += Vector3.left * width;
            }

            if (transform.position.y < screenMin.y)
            {
                position += Vector3.up * height;
            }
            if (transform.position.y > screenMax.y)
            {
                position += Vector3.down * height;
            }

            transform.position = position;
        }

        private Vector3 GetRandomPositionInside_Implementation()
        {
            Vector3 position;

            do
            {
                position = new Vector3(
                Random.Range(screenMin.x, screenMax.x),
                Random.Range(screenMin.y, screenMax.y),
                0);
            } while (Vector3.Distance(position, Vector3.zero) < 設定.SafeDistance);

            return position;
        }

        private Vector3 GetRandomPositionOnEdge_Implementation()
        {
            return new Vector3(
                Random.Range(0, 2) == 0 ? screenMin.x : screenMax.x,
                Random.Range(screenMin.y, screenMax.y),
                0f);
        }
    }
}