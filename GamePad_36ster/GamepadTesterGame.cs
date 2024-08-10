using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent.LoadContent(Content);
            gamepadRenderer.Initialize(gameContent);
            gamepadDataDisplay = new GamepadDataDisplay(gameContent.Font);

            Vector2 gamepadPosition = new Vector2(
                (graphics.PreferredBackBufferWidth - 724) / 2,
                (graphics.PreferredBackBufferHeight - 529) / 2);
            ltrt = new LTRT(gameContent, gamepadPosition);

        }

        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);


            vibrationTester.Update(inputManager.CurrentState, PlayerIndex.One);

            gamepadRenderer.Update(inputManager.CurrentState, gameTime);
            gamepadDataDisplay.Update(inputManager.CurrentState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            spriteBatch.Begin();


            // title
            string title = "GamePad 36ster";
            Vector2 titleSize = gameContent.TitleFont.MeasureString(title);
            Vector2 titlePosition = new Vector2(
                (graphics.PreferredBackBufferWidth - titleSize.X) / 2,
                30);
            spriteBatch.DrawString(gameContent.TitleFont, title, titlePosition, Color.MediumSlateBlue);

       
            string byPart = "By";
            string namePart = "Mr.SuS.60";

            Vector2 bySize = gameContent.By.MeasureString(byPart);
            Vector2 nameSize = gameContent.name.MeasureString(namePart);

            Vector2 byPosition = new Vector2(
                1280 - bySize.X - nameSize.X - 20,  
                10);  

            Vector2 namePosition = new Vector2(
                byPosition.X + bySize.X + 5,  
                10);  


            spriteBatch.DrawString(gameContent.By, byPart, byPosition, Color.Black); // 
            spriteBatch.DrawString(gameContent.name, namePart, namePosition, Color.MediumSlateBlue); 


            gamepadRenderer.Draw(spriteBatch);
            gamepadDataDisplay.Draw(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            ltrt.Draw(spriteBatch, inputManager.CurrentState);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
