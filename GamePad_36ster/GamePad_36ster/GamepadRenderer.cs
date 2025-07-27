using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GamepadTester
{
    public class GamepadRenderer
    {
        private GameContent gameContent;
        private GamePadState currentState;
        private Dictionary<Buttons, Texture2D> buttonTextures;
        private GlowingGuideButton glowingGuideButton;
        private LTRT ltrt;

        private Vector2 smoothedLeftStickOffset = Vector2.Zero;
        private Vector2 smoothedRightStickOffset = Vector2.Zero;

        private const float StickMovementScale = 40f;
        private const float SmoothingFactor = 0.20f;



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

            ltrt = new LTRT(gameContent, new Vector2(0, 138));
            glowingGuideButton = new GlowingGuideButton(gameContent);
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
            foreach (var buttonTexture in buttonTextures)
            {
                if (currentState.IsButtonDown(buttonTexture.Key))
                {
                    spriteBatch.Draw(buttonTexture.Value, Vector2.Zero, Color.White);
                }
            }

            DrawSticks(spriteBatch);
            ltrt.Draw(spriteBatch, currentState);
            DrawGlowingGuideButton(spriteBatch);
        }

        private void DrawSticks(SpriteBatch spriteBatch)
        {

            Vector2 leftStickBase = Vector2.Zero;
            Vector2 rightStickBase = Vector2.Zero;

            DrawStick(spriteBatch, gameContent.LeftStickTexture, leftStickBase, smoothedLeftStickOffset);
            DrawStick(spriteBatch, gameContent.RightStickTexture, rightStickBase, smoothedRightStickOffset);

            const float StickDrawScale = 1.2f;

            if (currentState.Buttons.LeftStick == ButtonState.Pressed)
            {
                spriteBatch.Draw(
                    gameContent.LStexture,
                    leftStickBase + smoothedLeftStickOffset * StickDrawScale,
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }

            if (currentState.Buttons.RightStick == ButtonState.Pressed)
            {
                spriteBatch.Draw(
                    gameContent.RStexture,
                    rightStickBase + smoothedRightStickOffset * StickDrawScale,
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
            }
        }

        private void DrawStick(SpriteBatch spriteBatch, Texture2D texture, Vector2 center, Vector2 offset)
        {
            Vector2 position = center + offset * 1.2f;
            spriteBatch.Draw(texture, position, null, Color.White, 0f,
                Vector2.Zero,
                Vector2.One, SpriteEffects.None, 0f);
        }

        private void DrawGlowingGuideButton(SpriteBatch spriteBatch)
        {
            glowingGuideButton.Draw(spriteBatch, Vector2.Zero);
        }
    }
}