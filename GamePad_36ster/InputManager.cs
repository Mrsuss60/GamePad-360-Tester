using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace GamepadTester
{
    public class InputManager
    {
        public GamePadState PreviousState { get; private set; }
        public GamePadState CurrentState { get; private set; }

        public bool IsStartButtonDown { get; private set; }
        public bool WasStartButtonPressed { get; private set; }
        public bool WasStartButtonReleased { get; private set; }
        public TimeSpan StartButtonHoldDuration { get; private set; }
        private DateTime? startButtonPressTime;

        public InputManager()
        {
            PreviousState = GamePad.GetState(PlayerIndex.One);
            CurrentState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update(GameTime gameTime)
        {
            PreviousState = CurrentState;
            CurrentState = GamePad.GetState(PlayerIndex.One);

            bool isStartButtonDownNow = CurrentState.IsButtonDown(Buttons.Start);
            bool wasStartButtonDownPreviously = PreviousState.IsButtonDown(Buttons.Start);

            WasStartButtonPressed = isStartButtonDownNow && !wasStartButtonDownPreviously;
            WasStartButtonReleased = !isStartButtonDownNow && wasStartButtonDownPreviously;
            IsStartButtonDown = isStartButtonDownNow;

            if (WasStartButtonPressed)
            {
                startButtonPressTime = DateTime.Now;
                StartButtonHoldDuration = TimeSpan.Zero;
            }
            else if (WasStartButtonReleased)
            {
                startButtonPressTime = null;
                StartButtonHoldDuration = TimeSpan.Zero;
            }
            else if (IsStartButtonDown && startButtonPressTime.HasValue)
            {
                StartButtonHoldDuration = DateTime.Now - startButtonPressTime.Value;
            }
        }
    }
}