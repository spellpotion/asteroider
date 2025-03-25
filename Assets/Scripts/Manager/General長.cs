using System.Collections;
using UnityEngine;

namespace Asteroider
{
    public class General長 : 抽象Manager<General長>
    {
        enum State { Initial, Menu, Game }
        private State state;

        protected void Start()
        {
            RunChangeToState(State.Menu);
        }

        private void RunChangeToState(State state)
        {
            StartCoroutine(ChangeToState(state));
        }

        private IEnumerator ChangeToState(State state)
        {
            ChangeState始(this.state, state);

            yield return new WaitForEndOfFrame(); // necessary

            ChangeState終(this.state, state);

            Debug.Log($"[{GetType().Name}] {this.state} → {state}");

            this.state = state;
        }

        private void ChangeState始(State state前, State state次)
        {
            if (state前 == State.Initial && state次 == State.Menu)
            {
                return;
            }
            if (state前 == State.Menu && state次 == State.Game)
            {
                Game長.StopDemo();
                Screen長.ClearScreen();
                return;
            }
            if (state前 == State.Game && state次 == State.Menu)
            {
                Game長.StopGame();
                Screen長.ClearScreen();
                return;
            }

            Debug.LogError($"[{GetType().Name}] 始 {state前} → {state次} UNDEFINED");
        }

        private void ChangeState終(State state前, State state次)
        {
            if (state次 == State.Menu)
            {
                Screen長.SetScreen(LayoutType.Menu);
                Game長.StartDemo();
                return;
            }
            if (state次 == State.Game)
            {
                Screen長.SetScreen(LayoutType.Game);
                Game長.StartGame();
                return;
            }

            Debug.LogError($"[{GetType().Name}] 終 {state前} → {state次} UNDEFINED");
        }

        public static void StartGame() => Instance.StartGame_Implementation();
        private void StartGame_Implementation()
        {
            if (state != State.Menu) return;

            RunChangeToState(State.Game);
        }

        public static void EndGame() => Instance.EndGame_Implementation();
        private void EndGame_Implementation()
        {
            if (state != State.Game) return;

            RunChangeToState(State.Menu);
        }

        public static void Quit() => Instance.Quit_Implementation();
        private void Quit_Implementation()
        {
            Application.Quit();
        }
    }
}
