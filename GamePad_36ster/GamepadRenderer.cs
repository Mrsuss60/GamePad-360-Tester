using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GamepadTester
{
    public class GamepadRenderer
    {
        private GameContent gameContent;
        private Vector2 gamepadPosition;
        private GamePadState currentState;
        private Vector2 leftVibrationIndicatorPosition;
        private Vector2 rightVibrationIndicatorPosition;
        private Dictionary<Buttons, Texture2D> buttonTextures;
        private GlowingGuideButton glowingGuideButton;
        private LTRT ltrt;

        private Vector2 smoothedLeftStickOffset = Vector2.Zero;
        private Vector2 smoothedRightStickOffset = Vector2.Zero;


        private const float StickMovementScale = 30f;

        private const float LeftStickCenterX = 362f;
        private const float LeftStickCenterY = 263f;
        private const float RightStickCenterX = 362f;
        private const float RightStickCenterY = 263f;

        private const float SmoothingFactor = 0.20f;


        public GamepadRenderer(int screenWidth, int screenHeight)
        {
            gamepadPosition = new Vector2(
                (screenWidth - 724) / 2,
                (screenHeight - 529) / 2);
            leftVibrationIndicatorPosition = new Vector2(screenWidth / 4, screenHeight - 50);
            rightVibrationIndicatorPosition = new Vector2(3 * screenWidth / 4, screenHeight - 50);
        }

        public void Initialize(GameContent content)
        {
            gameContent = content;

            buttonTextures = new Dictionary<Buttons, Texture2D>
            {
                { Buttons.A, gameContent.ButtonAPressedTexture },
                { Buttons.B, gameContent.ButtonBPressedTexture },
                { Buttons.X, gameContent.ButtonXPressedTexture },
                { Buttons.Y, gameContent.ButtonYPressedTexture },
                { Buttons.Start, gameContent.StartButtonTexture },
                { Buttons.Back, gameContent.BackButtonTexture },
                { Buttons.DPadUp, gameContent.DpadUpTexture },
                { Buttons.DPadDown, gameContent.DpadDownTexture },
                { Buttons.DPadLeft, gameContent.DpadLeftTexture },
                { Buttons.DPadRight, gameContent.DpadRightTexture },
                { Buttons.LeftShoulder, gameContent.LbTexture },
                { Buttons.RightShoulder, gameContent.RbTexture }
            };

            glowingGuideButton = new GlowingGuideButton(
                gameContent.GuideButtonDarkTexture,
                gameContent.GuideButtonMediumTexture,
                gameContent.GuideButtonBrightTexture
            );

            ltrt = new LTRT(content, gamepadPosition);
        }

        public void Update(GamePadState state, GameTime gameTime)
        {
            currentState = state;

            Vector2 leftStickInput = new Vector2(
                state.ThumbSticks.Left.X * StickMovementScale,
                -state.ThumbSticks.Left.Y * StickMovementScale);
            Vector2 rightStickInput = new Vector2(
                state.ThumbSticks.Right.X * StickMovementScale,
                -state.ThumbSticks.Right.Y * StickMovementScale);

            smoothedLeftStickOffset = Vector2.Lerp(smoothedLeftStickOffset, leftStickInput, SmoothingFactor);
            smoothedRightStickOffset = Vector2.Lerp(smoothedRightStickOffset, rightStickInput, SmoothingFactor);

            glowingGuideButton.Update(gameTime);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameContent.GamepadFrameTexture, gamepadPosition, Color.White);

            foreach (var buttonTexture in buttonTextures)
            {
                if (currentState.IsButtonDown(buttonTexture.Key))
                {
                    spriteBatch.Draw(buttonTexture.Value, gamepadPosition, Color.White);
                }
            }

            DrawSticks(spriteBatch);
            ltrt.Draw(spriteBatch, currentState);
            DrawGlowingGuideButton(spriteBatch);
            DrawVibrationIndicators(spriteBatch);
        }


        private void DrawSticks(SpriteBatch spriteBatch)
        {
            Vector2 leftStickCenter = new Vector2(gamepadPosition.X + LeftStickCenterX, gamepadPosition.Y + LeftStickCenterY);
            Vector2 rightStickCenter = new Vector2(gamepadPosition.X + RightStickCenterX, gamepadPosition.Y + RightStickCenterY);

            DrawStick(spriteBatch, gameContent.LeftStickTexture, leftStickCenter, smoothedLeftStickOffset);
            DrawStick(spriteBatch, gameContent.RightStickTexture, rightStickCenter, smoothedRightStickOffset);
        }


        private void DrawStick(SpriteBatch spriteBatch, Texture2D texture, Vector2 center, Vector2 offset)
        {
            Vector2 position = center + offset;
            spriteBatch.Draw(texture, position, null, Color.White, 0f,
                new Vector2(texture.Width / 2, texture.Height / 2),
                Vector2.One, SpriteEffects.None, 0f);
        }

        private void DrawGlowingGuideButton(SpriteBatch spriteBatch)
        {
            Vector2 gamepadCenter = new Vector2(
                gamepadPosition.X + 0 / 2, 
                gamepadPosition.Y + 35 / 2);

            Vector2 guideButtonOffset = new Vector2(0, -20);

            Vector2 guideButtonPosition = gamepadCenter + guideButtonOffset;
            glowingGuideButton.Draw(spriteBatch, guideButtonPosition);
        }

        private void DrawVibrationIndicators(SpriteBatch spriteBatch)
        {
            float leftVibration = currentState.Triggers.Left;
            float rightVibration = currentState.Triggers.Right;

            if (leftVibration > 0.1f)
            {
                DrawVibrationText(spriteBatch, "Left Motor", leftVibrationIndicatorPosition, Color.Black);
            }

            if (rightVibration > 0.1f)
            {
                DrawVibrationText(spriteBatch, "Right Motor", rightVibrationIndicatorPosition, Color.Black);
            }
        }

        private void DrawVibrationText(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            Vector2 textSize = gameContent.Font.MeasureString(text);
            spriteBatch.DrawString(gameContent.Font, text, position - textSize / 2, color);
        }
    }
}