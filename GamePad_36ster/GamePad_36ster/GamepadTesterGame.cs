using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GamepadTester
{
    public class GamepadTesterGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameContent gameContent;
        private GamepadRenderer gamepadRenderer;
        private VibrationTester vibrationTester;
        private GamepadDataDisplay gamepadDataDisplay;
        private InputManager inputManager;
        private LTRT ltrt;
        private TimeSpan lbRbHoldTime = TimeSpan.Zero;
        private const float MenuActivationTime = 1.0f;

        private VertexPositionColor[] gradientVertices;
        private BasicEffect gradientEffect;

        private Backcolors backcolors;
        private GamePadState previousState;

        private bool isSplashActive = true;

        private bool isSlidingOut = false;
        private float slideOffset = 0f;
        private const float SlideSpeed = 840f;
        private const float SplashAnimationSpeed = 1.7f;
        private const float SplashHdistance = 35f;
        private float splashInputDelay = 1.2f;
        private float splashAnimationTime = 0f;
        private float splashElapsedTime = 0f;
        private float pressAnimationDelay = 1.2f;


        public GamepadTesterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            vibrationTester = new VibrationTester();
            inputManager = new InputManager();
        }

        protected override void Initialize()
        {
            gameContent = new GameContent();
            gamepadRenderer = new GamepadRenderer(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            gradientVertices = new VertexPositionColor[4];
            gradientEffect = new BasicEffect(GraphicsDevice);
            gradientEffect.VertexColorEnabled = true;
            gradientEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0, GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height, 0,
                0, 1);

            previousState = GamePad.GetState(PlayerIndex.One);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent.LoadContent(Content);
            gamepadRenderer.Initialize(gameContent);
            gamepadDataDisplay = new GamepadDataDisplay(gameContent.Font);

            Vector2 gamepadPos = new Vector2(
                (graphics.PreferredBackBufferWidth - 724) / 2,
                (graphics.PreferredBackBufferHeight - 529) / 2);
            ltrt = new LTRT(gameContent, gamepadPos);

            backcolors = new Backcolors(gameContent.Font);

            SaveLoad.LoadColors(backcolors);
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            GamePadState currentState = inputManager.CurrentState;

            if (isSplashActive)
            {
                if (!isSlidingOut)
                {
                    splashElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (pressAnimationDelay > 0)
                    {
                        pressAnimationDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        splashAnimationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        slideOffset = -Math.Abs((float)Math.Sin(splashAnimationTime * SplashAnimationSpeed)) * SplashHdistance;
                    }

                    if (splashElapsedTime >= splashInputDelay &&
                        (currentState.Buttons.A == ButtonState.Pressed ||
                        currentState.Buttons.B == ButtonState.Pressed ||
                        currentState.Buttons.X == ButtonState.Pressed ||
                        currentState.Buttons.Y == ButtonState.Pressed ||
                        currentState.Buttons.Start == ButtonState.Pressed ||
                        currentState.Buttons.Back == ButtonState.Pressed ||
                        currentState.Buttons.LeftShoulder == ButtonState.Pressed ||
                        currentState.Buttons.RightShoulder == ButtonState.Pressed ||
                        currentState.Buttons.LeftStick == ButtonState.Pressed ||
                        currentState.Buttons.RightStick == ButtonState.Pressed ||
                        currentState.DPad.Up == ButtonState.Pressed ||
                        currentState.DPad.Down == ButtonState.Pressed ||
                        currentState.DPad.Left == ButtonState.Pressed ||
                        currentState.DPad.Right == ButtonState.Pressed ||
                        Math.Abs(currentState.ThumbSticks.Left.X) > 0.1f ||
                        Math.Abs(currentState.ThumbSticks.Left.Y) > 0.1f ||
                        Math.Abs(currentState.ThumbSticks.Right.X) > 0.1f ||
                        Math.Abs(currentState.ThumbSticks.Right.Y) > 0.1f ||
                        currentState.Triggers.Left > 0.1f ||
                        currentState.Triggers.Right > 0.1f))
                    {
                        isSlidingOut = true;
                    }
                }
                else
                {
                    slideOffset -= SlideSpeed * 2.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (slideOffset <= -gameContent.Splashscreen.Width)
                    {
                        isSplashActive = false;
                    }
                }

                // if (isSplashActive)
                // {
                //     base.Update(gameTime);
                //     return;
                // }
            }

            if (!backcolors.IsActive &&
                currentState.Buttons.LeftShoulder == ButtonState.Pressed &&
                currentState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                lbRbHoldTime += gameTime.ElapsedGameTime;
                if (lbRbHoldTime.TotalSeconds >= MenuActivationTime)
                {
                    backcolors.ToggleActive();
                    lbRbHoldTime = TimeSpan.Zero;
                }
            }
            else
            {
                lbRbHoldTime = TimeSpan.Zero;
            }


            if (backcolors.IsActive)
            {
                backcolors.Update(currentState, previousState, (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                vibrationTester.Update(currentState, PlayerIndex.One);
                gamepadRenderer.Update(currentState, gameTime);
                gamepadDataDisplay.Update(currentState);
            }

            previousState = currentState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Color topLeft, topRight, bottom;
            backcolors.SetupGradient(out topLeft, out topRight, out bottom);
            SetupGradient(topLeft, topRight, bottom);

            foreach (var pass in gradientEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, gradientVertices, 0, 2);
            }

            if (backcolors.IsActive)
            {
                backcolors.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            }
            else
            {
                gamepadRenderer.Draw(spriteBatch);
                gamepadDataDisplay.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                ltrt.Draw(spriteBatch, inputManager.CurrentState);
            }

            //string textLine1 = "By Mr.SuS.60";
            //string textLine2 = "github.com/Mrsuss60";

            //Vector2 textSize1 = gameContent.Font.MeasureString(textLine1);
            //Vector2 textSize2 = gameContent.Font.MeasureString(textLine2);

            //float paddingX = 20f;
            //float paddingY = 8f;
            //float lineSpacing = gameContent.Font.LineSpacing + 4f;


            //Vector2 textPosition1 = new Vector2(paddingX, paddingY);
            //Vector2 textPosition2 = new Vector2(paddingX, paddingY + lineSpacing);

            //spriteBatch.DrawString(gameContent.Font, textLine1, textPosition1, Color.Black);
            //spriteBatch.DrawString(gameContent.Font, textLine2, textPosition2, Color.Black);

            if (!backcolors.IsActive)
            {

                string message = "Hold LB+RB for a second to access background colors menu";

                Vector2 textSize = gameContent.Font.MeasureString(message);

                float LbRbpaddingX = 20f;
                float LbRbpaddingY = 15f;

                Vector2 textPosition = new Vector2(GraphicsDevice.Viewport.Width - textSize.X - LbRbpaddingX, LbRbpaddingY);

                spriteBatch.DrawString(gameContent.Font, message, textPosition, Color.Black);

                float PositionX = -10f;
                float PositionY = -5f;
                spriteBatch.Draw(gameContent.Credits, new Vector2(PositionX, PositionY), Color.White);

            }




            if (isSplashActive)
            {
                float baseX = (graphics.PreferredBackBufferWidth - gameContent.Splashscreen.Width) / 2;
                float baseY = (graphics.PreferredBackBufferHeight - gameContent.Splashscreen.Height) / 2;

                float xPosition = baseX + slideOffset;

                Vector2 logoPosition = new Vector2(xPosition, baseY);
                spriteBatch.Draw(gameContent.Splashscreen, logoPosition, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SetupGradient(Color topLeft, Color topRight, Color bottom)
        {
            gradientVertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), topLeft);
            gradientVertices[1] = new VertexPositionColor(new Vector3(GraphicsDevice.Viewport.Width, 0, 0), topRight);
            gradientVertices[2] = new VertexPositionColor(new Vector3(0, GraphicsDevice.Viewport.Height, 0), bottom);
            gradientVertices[3] = new VertexPositionColor(new Vector3(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0), bottom);
        }
    }
}
