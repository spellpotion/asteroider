using System.Collections;
using UnityEngine;

namespace Asteroider
{
    public class General長 : 抽象Manager<General長>
    {
        private enum State { Initial, Menu, Game }
        private State state;

        public static void StartGame() => Instance.StartGame_Implementation();
        public static void Settings() => Instance.Settings_Implementation();
        public static void Back() => Instance.Back_Implementation();
        public static void EndGame() => Instance.EndGame_Implementation();
        public static void RestartGame() => Instance.RestartGame_Implementation();
        public static void Quit() => Instance.Quit_Implementation();

        protected void Start()
        {
            RunChangeToState(State.Menu);
        }

        #region State

        private void RunChangeToState(State state)
            => StartCoroutine(ChangeToState(state));

        private IEnumerator ChangeToState(State state)
        {
            ChangeState前();

            yield return new WaitForEndOfFrame();

            ChangeState後();

            Debug.Log($"[{GetType().Name}] state changed {this.state} → {state}");

            this.state = state;

            void ChangeState前()
            {
                if (this.state == State.Menu)
                {
                    Game長.StopDemo();
                }
                else if (this.state == State.Game)
                {
                    Game長.StopGame();
                }

                Screen長.Clear();
                Audio長.StopMusic();
            }

            void ChangeState後()
            {
                if (state == State.Menu)
                {
                    Screen長.OpenNew(ScreenType.Menu);
                    Audio長.PlayMusic(MusicType.Menu);

                    Game長.StartDemo();
                }
                else if (state == State.Game)
                {
                    Screen長.OpenNew(ScreenType.Game);
                    Audio長.PlayMusic(MusicType.Game);

                    Game長.StartGame();
                }
            }
        }

        #endregion State
        #region Screen

        private void RunChangeScreen(ScreenType screenType)
            => StartCoroutine(ChangeScreen(screenType));
        private IEnumerator ChangeScreen(ScreenType screenType)
        {
            ChangeScreen前();

            yield return new WaitForEndOfFrame();

            ChangeScreen後(screenType);
        }

        private void RunChangeScreenBack()
            => StartCoroutine(ChangeScreenBack());
        private IEnumerator ChangeScreenBack()
        {
            ChangeScreen前();

            yield return new WaitForEndOfFrame();

            Screen長.Back();
        }

        void ChangeScreen前()
        {
            Screen長.Clear();
        }

        void ChangeScreen後(ScreenType screenType)
        {
            Screen長.OpenAdd(screenType);
        }

        #endregion Screen
        #region Implementation

        private void StartGame_Implementation()
        {
            if (state != State.Menu) return;

            RunChangeToState(State.Game);
        }

        private void Settings_Implementation()
        {
            if (state != State.Menu) return;

            RunChangeScreen(ScreenType.Settings);
        }

        private void EndGame_Implementation()
        {
            if (state != State.Game) return;

            RunChangeToState(State.Menu);
        }

        private void RestartGame_Implementation()
        {
            if (state != State.Game) return;

            RunChangeToState(State.Game);
        }

        private void Back_Implementation()
        {
            if (state != State.Menu) return;

            RunChangeScreenBack();
        }

        private void Quit_Implementation()
        {
            if (state != State.Menu) return;

            Application.Quit();
        }

        #endregion Implementation
    }
}
