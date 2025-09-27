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
            float leftX = screenWidth * 0.05f;
            float rightX = screenWidth * 0.87f;
            float yStart = screenHeight * 0.25f;
            float yStep = 38.3f;


            DrawButtonState(spriteBatch, "A", currentState.Buttons.A, new Vector2(rightX, yStart + yStep * 0));
            DrawButtonState(spriteBatch, "B", currentState.Buttons.B, new Vector2(rightX, yStart + yStep * 1));
            DrawButtonState(spriteBatch, "X", currentState.Buttons.X, new Vector2(rightX, yStart + yStep * 2));
            DrawButtonState(spriteBatch, "Y", currentState.Buttons.Y, new Vector2(rightX, yStart + yStep * 3));
            DrawButtonState(spriteBatch, "RB", currentState.Buttons.RightShoulder, new Vector2(rightX, yStart + yStep * 4));
            DrawButtonState(spriteBatch, "START", currentState.Buttons.Start, new Vector2(rightX, yStart + yStep * 5));
            DrawButtonState(spriteBatch, "RS", currentState.Buttons.RightStick, new Vector2(rightX, yStart + yStep * 6));


            DrawButtonState(spriteBatch, "D-UP", currentState.DPad.Up, new Vector2(leftX, yStart + yStep * 0));
            DrawButtonState(spriteBatch, "D-DOWN", currentState.DPad.Down, new Vector2(leftX, yStart + yStep * 1));
            DrawButtonState(spriteBatch, "D-LEFT", currentState.DPad.Left, new Vector2(leftX, yStart + yStep * 2));
            DrawButtonState(spriteBatch, "D-RIGHT", currentState.DPad.Right, new Vector2(leftX, yStart + yStep * 3));
            DrawButtonState(spriteBatch, "LB", currentState.Buttons.LeftShoulder, new Vector2(leftX, yStart + yStep * 4));
            DrawButtonState(spriteBatch, "BACK", currentState.Buttons.Back, new Vector2(leftX, yStart + yStep * 5));
            DrawButtonState(spriteBatch, "LS", currentState.Buttons.LeftStick, new Vector2(leftX, yStart + yStep * 6));


            DrawTriggerState(spriteBatch, "RT", currentState.Triggers.Right, new Vector2(rightX, yStart + yStep * 7));
            DrawTriggerState(spriteBatch, "LT", currentState.Triggers.Left, new Vector2(leftX, yStart + yStep * 7));


            float stickYOffset = 28f;
            float stickY = screenHeight * 0.85f;
            DrawStickPosition(spriteBatch, "Left Stick", currentState.ThumbSticks.Left, new Vector2(leftX, stickY), stickYOffset);
            DrawStickPosition(spriteBatch, "Right Stick", currentState.ThumbSticks.Right, new Vector2(rightX, stickY), stickYOffset);

            DrawVibrationIndicators(spriteBatch, screenWidth, screenHeight);
        }

        private void DrawButtonState(SpriteBatch spriteBatch, string buttonName, ButtonState state, Vector2 position)
        {
            int value = state == ButtonState.Pressed ? 1 : 0;
            string text = string.Format("{0}: {1}", buttonName, value);
            spriteBatch.DrawString(font, text, position, Color.Black);
        }

        private void DrawTriggerState(SpriteBatch spriteBatch, string triggerName, float value, Vector2 position)
        {
            int scaledValue = (int)(value * 255);
            string text = string.Format("{0}: {1}", triggerName, scaledValue);
            spriteBatch.DrawString(font, text, position, Color.Black);
        }

        private void DrawStickPosition(SpriteBatch spriteBatch, string stickName, Vector2 position, Vector2 screenPosition, float verticalOffset)
        {
            int xValue = (int)(position.X * 32768);
            int yValue = (int)(position.Y * 32768);

            spriteBatch.DrawString(font, stickName + ":", screenPosition, Color.Black);

            Vector2 textXPos = new Vector2(screenPosition.X, screenPosition.Y + verticalOffset);
            spriteBatch.DrawString(font, "X=", textXPos, Color.Black);

            Vector2 valXPos = new Vector2(textXPos.X + font.MeasureString("X=").X + 5, textXPos.Y);
            spriteBatch.DrawString(font, xValue.ToString(), valXPos, Color.Black);

            Vector2 textYPos = new Vector2(screenPosition.X, screenPosition.Y + verticalOffset * 2);
            spriteBatch.DrawString(font, "Y=", textYPos, Color.Black);

            Vector2 valYPos = new Vector2(textYPos.X + font.MeasureString("Y=").X + 5, textYPos.Y);
            spriteBatch.DrawString(font, yValue.ToString(), valYPos, Color.Black);
        }

        private void DrawVibrationIndicators(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            int OffsetFromCenter = 650;
            int OffsetY = 335;
            int centerX = screenWidth / 2;

            Vector2 LeftMotorText = new Vector2(centerX - OffsetFromCenter, screenHeight - OffsetY);
            Vector2 RightMotorText = new Vector2(centerX + OffsetFromCenter, screenHeight - OffsetY);

            float leftVibration = currentState.Triggers.Left;
            float rightVibration = currentState.Triggers.Right;

            if (leftVibration > 0.1f)
            {
                DrawVibrationText(spriteBatch, "Left Motor", LeftMotorText, Color.Black);
            }

            if (rightVibration > 0.1f)
            {
                DrawVibrationText(spriteBatch, "Right Motor", RightMotorText, Color.Black);
            }
        }

        private void DrawVibrationText(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            Vector2 textSize = font.MeasureString(text);
            spriteBatch.DrawString(font, text, position - textSize / 2, color);
        }
    }
}