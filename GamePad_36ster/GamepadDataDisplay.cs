using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamepadTester
{
    public class GamepadDataDisplay
    {
        private SpriteFont font;
        private GamePadState currentState;

        public GamepadDataDisplay(SpriteFont font)
        {
            this.font = font;
        }

        public void Update(GamePadState state)
        {
            currentState = state;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            DrawButtonState(spriteBatch, "A", currentState.Buttons.A, new Vector2(screenWidth - 150, 100));
            DrawButtonState(spriteBatch, "B", currentState.Buttons.B, new Vector2(screenWidth - 150, 130));
            DrawButtonState(spriteBatch, "X", currentState.Buttons.X, new Vector2(screenWidth - 150, 160));
            DrawButtonState(spriteBatch, "Y", currentState.Buttons.Y, new Vector2(screenWidth - 150, 190));
            DrawButtonState(spriteBatch, "RB", currentState.Buttons.RightShoulder, new Vector2(screenWidth - 150, 220));
            DrawButtonState(spriteBatch, "START", currentState.Buttons.Start, new Vector2(screenWidth - 150, 250));
            DrawButtonState(spriteBatch, "RS", currentState.Buttons.RightStick, new Vector2(screenWidth - 150, 280));

            DrawButtonState(spriteBatch, "D_UP", currentState.DPad.Up, new Vector2(50, 100));
            DrawButtonState(spriteBatch, "D_DOWN", currentState.DPad.Down, new Vector2(50, 130));
            DrawButtonState(spriteBatch, "D_LEFT", currentState.DPad.Left, new Vector2(50, 160));
            DrawButtonState(spriteBatch, "D_RIGHT", currentState.DPad.Right, new Vector2(50, 190));
            DrawButtonState(spriteBatch, "LB", currentState.Buttons.RightShoulder, new Vector2(50, 220));
            DrawButtonState(spriteBatch, "BACK", currentState.Buttons.Back, new Vector2(50, 250));
            DrawButtonState(spriteBatch, "LS", currentState.Buttons.LeftStick, new Vector2(50, 280));

            DrawTriggerState(spriteBatch, "RT", currentState.Triggers.Right, new Vector2(screenWidth - 150, 310));
            DrawTriggerState(spriteBatch, "LT", currentState.Triggers.Left, new Vector2(50, 310));

            DrawStickPosition(spriteBatch, "Left Stick", currentState.ThumbSticks.Left, new Vector2(50, screenHeight - 140), 20);
            DrawStickPosition(spriteBatch, "Right Stick", currentState.ThumbSticks.Right, new Vector2(1280 - 150, screenHeight - 140), 20);
        }

        private void DrawButtonState(SpriteBatch spriteBatch, string buttonName, ButtonState state, Vector2 position)
        {
            float value = state == ButtonState.Pressed ? 1.0f : 0.0f;
            string text = buttonName + ": " + value.ToString("F1");
            spriteBatch.DrawString(font, text, position, Color.Black);
        }

        private void DrawTriggerState(SpriteBatch spriteBatch, string triggerName, float value, Vector2 position)
        {
            int scaledValue = (int)(value * 255);
            string text = triggerName + ": " + scaledValue.ToString();
            spriteBatch.DrawString(font, text, position, Color.Black);
        }

        private void DrawStickPosition(SpriteBatch spriteBatch, string stickName, Vector2 position, Vector2 screenPosition, float verticalOffset)
        {
            int xValue = (int)(position.X * 32768);
            int yValue = (int)(position.Y * 32768);

            string title = stickName + ":";
            string textX = "X=" + xValue.ToString();
            string textY = "Y=" + yValue.ToString();

            spriteBatch.DrawString(font, title, screenPosition, Color.Black);

            spriteBatch.DrawString(font, textX, new Vector2(screenPosition.X, screenPosition.Y + verticalOffset), Color.Black);

            spriteBatch.DrawString(font, textY, new Vector2(screenPosition.X, screenPosition.Y + 2 * verticalOffset), Color.Black);
        }
    }
}
