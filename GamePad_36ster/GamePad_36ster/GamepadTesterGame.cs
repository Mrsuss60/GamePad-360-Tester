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

        private const int SCREEN_WIDTH = 1920;
        private const int SCREEN_HEIGHT = 1080;
        private const float SlideSpeed = 1550f;
        private const float SplashAnimationSpeed = 1.7f;
        private const float SplashHdistance = 35f;

        private bool isSplashActive = true;
        private bool isSlidingOut = false;
        private float slideOffset = 0f;
        private float splashInputDelay = 1.2f;
        private float splashAnimationTime = 0f;
        private float splashElapsedTime = 0f;
        private float pressAnimationDelay = 1.2f;

        private bool PressAnyButton(GamePadState state)
        {
            Buttons[] buttonsToCheck = new Buttons[]
            {
                Buttons.A, Buttons.B, Buttons.X, Buttons.Y,
                Buttons.Start, Buttons.Back,
                Buttons.LeftShoulder, Buttons.RightShoulder,
                Buttons.LeftStick, Buttons.RightStick
            };

            foreach (var button in buttonsToCheck)
            {
                if (state.IsButtonDown(button))
                    return true;
            }

            if (state.DPad.Up == ButtonState.Pressed ||
                state.DPad.Down == ButtonState.Pressed ||
                state.DPad.Left == ButtonState.Pressed ||
                state.DPad.Right == ButtonState.Pressed)
            {
                return true;
            }

            const float stickThreshold = 0.1f;
            if (Math.Abs(state.ThumbSticks.Left.X) > stickThreshold ||
                Math.Abs(state.ThumbSticks.Left.Y) > stickThreshold ||
                Math.Abs(state.ThumbSticks.Right.X) > stickThreshold ||
                Math.Abs(state.ThumbSticks.Right.Y) > stickThreshold)
            {
                return true;
            }

            if (state.Triggers.Left > 0.1f || state.Triggers.Right > 0.1f)
            {
                return true;
            }

            return false;
        }


        public GamepadTesterGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();
            vibrationTester = new VibrationTester();
            inputManager = new InputManager();
        }


        protected override void Initialize()
        {
            gameContent = new GameContent();
            gamepadRenderer = new GamepadRenderer();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent.LoadContent(Content);
            gamepadRenderer.Initialize(gameContent);
            gamepadDataDisplay = new GamepadDataDisplay(gameContent.Font);
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

                    if (splashElapsedTime >= splashInputDelay && PressAnyButton(currentState))
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
            }
            vibrationTester.Update(currentState, PlayerIndex.One);
            gamepadRenderer.Update(currentState, gameTime);
            gamepadDataDisplay.Update(currentState);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            spriteBatch.Draw(gameContent.GamepadFrameTexture, Vector2.Zero, Color.White);

            gamepadRenderer.Draw(spriteBatch);
            gamepadDataDisplay.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            float PositionX = -10f;
            float PositionY = -5f;
            spriteBatch.Draw(gameContent.Credits, new Vector2(PositionX, PositionY), Color.White);

#if true
            if (isSplashActive)
            {
                float baseX = (graphics.PreferredBackBufferWidth - gameContent.Splashscreen.Width) / 2;
                float baseY = (graphics.PreferredBackBufferHeight - gameContent.Splashscreen.Height) / 2;

                float xPosition = baseX + slideOffset;

                Vector2 logoPosition = new Vector2(xPosition, baseY);
                spriteBatch.Draw(gameContent.Splashscreen, logoPosition, Color.White);
            }
#endif

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
