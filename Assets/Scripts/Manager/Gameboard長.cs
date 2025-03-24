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
        }

        private void Clear_Implementation()
        {
            for (var i = gameboardObjects.Count - 1; i >= 0; i--)
            {
                Destroy(gameboardObjects[i].gameObject);
            }

            gameboardObjects.Clear();
        }

        private void Add_Implementation(GameboardObject gameboardObject)
        {
            gameboardObject.transform.SetParent(gameBoard);

            gameboardObjects.Add(gameboardObject);
        }

        private void Remove_Implementation(GameboardObject gameboardObject)
        {
            gameboardObjects.Remove(gameboardObject);

            Destroy(gameboardObject.gameObject);
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