using System.Collections;
using UnityEngine;

namespace Asteroider
{
    public class General長 : 抽象Manager<General長>
    {
        enum State { Initial, Menu, Game }
        private State state;

        public static void StartGame() => Instance.StartGame_Implementation();
        public static void EndGame() => Instance.EndGame_Implementation();
        public static void RestartGame() => Instance.RestartGame_Implementation();
        public static void Quit() => Instance.Quit_Implementation();

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
            if (state前 == State.Game)
            {
                Game長.StopGame();
                Screen長.ClearScreen();
                return;
            }

            Debug.LogError($"[{GetType().Name}] timeBeg {state前} → {state次} UNDEFINED");
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

            Debug.LogError($"[{GetType().Name}] timeEnd {state前} → {state次} UNDEFINED");
        }

        
        private void StartGame_Implementation()
        {
            if (state != State.Menu) return;

            RunChangeToState(State.Game);
        }


        private void EndGame_Implementation()
        {
            if (state != State.Game) return;

            RunChangeToState(State.Menu);
        }

        private void RestartGame_Implementation()
        {
            Debug.Log("Restart");

            if (state != State.Game) return;

            RunChangeToState(State.Game);
        }

        private void Quit_Implementation()
        {
            Application.Quit();
        }
    }
}
