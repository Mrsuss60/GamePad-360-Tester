using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GamepadTester
{
    public class InputManager
    {
        public GamePadState PreviousState { get; private set; }
        public GamePadState CurrentState { get; private set; }

        public InputManager()
        {
            PreviousState = GamePad.GetState(PlayerIndex.One);
            CurrentState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update(GameTime gameTime)
        {
            // Update the previous and current state
            PreviousState = CurrentState;
            CurrentState = GamePad.GetState(PlayerIndex.One);
        }
    }
}
