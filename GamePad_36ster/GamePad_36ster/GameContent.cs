using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamepadTester
{
    public class GameContent
    {
        public Texture2D GamepadFrameTexture { get; private set; }
        public Texture2D ButtonAPressedTexture { get; private set; }
        public Texture2D ButtonBPressedTexture { get; private set; }
        public Texture2D ButtonXPressedTexture { get; private set; }
        public Texture2D ButtonYPressedTexture { get; private set; }
        public Texture2D StartButtonTexture { get; private set; }
        public Texture2D BackButtonTexture { get; private set; }
        public Texture2D DpadUpTexture { get; private set; }
        public Texture2D DpadDownTexture { get; private set; }
        public Texture2D DpadLeftTexture { get; private set; }
        public Texture2D DpadRightTexture { get; private set; }
        public Texture2D LbTexture { get; private set; }
        public Texture2D RbTexture { get; private set; }
        public Texture2D LeftStickTexture { get; private set; }
        public Texture2D RightStickTexture { get; private set; }
        public Texture2D LeftTriggerBarTexture { get; private set; }
        public Texture2D LeftTriggerFillTexture { get; private set; }
        public Texture2D RightTriggerBarTexture { get; private set; }
        public Texture2D RightTriggerFillTexture { get; private set; }
        public Texture2D GuideB1Texture { get; private set; }
        public Texture2D GuideB2Texture { get; private set; }
        public Texture2D GuideB3Texture { get; private set; }
        public Texture2D GuideB4Texture { get; private set; }
        public Texture2D RStexture { get; private set; }
        public Texture2D LStexture { get; private set; }
        public SpriteFont Font { get; private set; }
        public Texture2D Splashscreen { get; private set; }

        public Texture2D Credits { get; private set; }
        public void LoadContent(ContentManager content)
        {
            GamepadFrameTexture = content.Load<Texture2D>("gamepadframe");
            ButtonAPressedTexture = content.Load<Texture2D>("A");
            ButtonBPressedTexture = content.Load<Texture2D>("B");
            ButtonXPressedTexture = content.Load<Texture2D>("X");
            ButtonYPressedTexture = content.Load<Texture2D>("Y");
            StartButtonTexture = content.Load<Texture2D>("start");
            BackButtonTexture = content.Load<Texture2D>("back");
            DpadUpTexture = content.Load<Texture2D>("dpad_up");
            DpadDownTexture = content.Load<Texture2D>("dpad_down");
            DpadLeftTexture = content.Load<Texture2D>("dpad_left");
            DpadRightTexture = content.Load<Texture2D>("dpad_right");
            LbTexture = content.Load<Texture2D>("LB");
            RbTexture = content.Load<Texture2D>("RB");
            LeftStickTexture = content.Load<Texture2D>("LeftStick");
            RightStickTexture = content.Load<Texture2D>("RightStick");
            RStexture = content.Load<Texture2D>("RS");
            LStexture = content.Load<Texture2D>("LS");

            LeftTriggerFillTexture = content.Load<Texture2D>("LeftTFill");
            RightTriggerFillTexture = content.Load<Texture2D>("RightTFill");

            GuideB1Texture = content.Load<Texture2D>("guide_b1");
            GuideB2Texture = content.Load<Texture2D>("guide_b2");
            GuideB3Texture = content.Load<Texture2D>("guide_b3");
            GuideB4Texture = content.Load<Texture2D>("guide_b4");
            Font = content.Load<SpriteFont>("font");
            Credits = content.Load<Texture2D>("credits");

            Splashscreen = content.Load<Texture2D>("splashscreen");
        }

        public Texture2D GetButtonTexture(Buttons button)
        {
            switch (button)
            {
                case Buttons.A: return ButtonAPressedTexture;
                case Buttons.B: return ButtonBPressedTexture;
                case Buttons.X: return ButtonXPressedTexture;
                case Buttons.Y: return ButtonYPressedTexture;
                case Buttons.Start: return StartButtonTexture;
                case Buttons.Back: return BackButtonTexture;
                case Buttons.DPadUp: return DpadUpTexture;
                case Buttons.DPadDown: return DpadDownTexture;
                case Buttons.DPadLeft: return DpadLeftTexture;
                case Buttons.DPadRight: return DpadRightTexture;
                case Buttons.LeftShoulder: return LbTexture;
                case Buttons.RightShoulder: return RbTexture;
                default: return null;
            }
        }
    }
}