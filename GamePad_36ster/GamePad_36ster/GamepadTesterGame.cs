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
        private const float MenuActivationTime = 1f; 

        private VertexPositionColor[] gradientVertices;
        private BasicEffect gradientEffect;

        private Backcolors backcolors;
        private GamePadState previousState;

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
        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            GamePadState currentState = inputManager.CurrentState;

            if (currentState.Buttons.LeftShoulder == ButtonState.Pressed &&
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
            Color topLeft = new Color();
            Color topRight = new Color();
            Color bottom = new Color();

            backcolors.SetupGradient(out topLeft, out topRight, out bottom);
            SetupGradient(topLeft, topRight, bottom);

            foreach (var pass in gradientEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, gradientVertices, 0, 2);
            }

            spriteBatch.Begin();

            if (backcolors.IsActive)
            {
                backcolors.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            }
            else
            {
                string title = "GamePad 36ster";
                Vector2 titleSize = gameContent.TitleFont.MeasureString(title);
                Vector2 titlePos = new Vector2(
                    (graphics.PreferredBackBufferWidth - titleSize.X) / 2,
                    30);
                spriteBatch.DrawString(gameContent.TitleFont, title, titlePos, Color.Black);

                string by = "By";
                string name = "Mr.SuS.60";

                Vector2 bySize = gameContent.By.MeasureString(by);
                Vector2 nameSize = gameContent.name.MeasureString(name);

                Vector2 byPos = new Vector2(1280 - bySize.X - nameSize.X - 20, 10);
                Vector2 namePos = new Vector2(byPos.X + bySize.X + 5, 10);

                spriteBatch.DrawString(gameContent.By, by, byPos, Color.Black);
                spriteBatch.DrawString(gameContent.name, name, namePos, Color.Black);

                gamepadRenderer.Draw(spriteBatch);
                gamepadDataDisplay.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                ltrt.Draw(spriteBatch, inputManager.CurrentState);
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