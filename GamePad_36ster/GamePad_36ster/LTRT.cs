using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamepadTester;

public class LTRT
{
    private GameContent gameContent;
    private Rectangle leftTriggerFillRect;
    private Rectangle rightTriggerFillRect;
    private int PositionX = 0;
    private int PositionY = 138;


    public LTRT(GameContent content, Vector2 gamepadPosition)
    {
        gameContent = content;

        leftTriggerFillRect = new Rectangle(PositionX, PositionY, content.LeftTriggerFillTexture.Width, content.LeftTriggerFillTexture.Height);

        rightTriggerFillRect = new Rectangle(PositionX, PositionY, content.RightTriggerFillTexture.Width, content.RightTriggerFillTexture.Height);

        UpdateFillPositions();
    }

    public void UpdateFillPositions()
    {
        leftTriggerFillRect = new Rectangle(
            (int)PositionX,
            (int)PositionY,
            gameContent.LeftTriggerFillTexture.Width,
            gameContent.LeftTriggerFillTexture.Height
        );

        rightTriggerFillRect = new Rectangle(
            (int)PositionX,
            (int)PositionY,
            gameContent.RightTriggerFillTexture.Width,
            gameContent.RightTriggerFillTexture.Height
        );
    }

    public void Draw(SpriteBatch spriteBatch, GamePadState currentState)
    {
        DrawTriggerFill(spriteBatch, leftTriggerFillRect, currentState.Triggers.Left, gameContent.LeftTriggerFillTexture);
        DrawTriggerFill(spriteBatch, rightTriggerFillRect, currentState.Triggers.Right, gameContent.RightTriggerFillTexture);
    }

    private void DrawTriggerFill(SpriteBatch spriteBatch, Rectangle fillRect, float value, Texture2D fillTexture)
    {
        float triggerValue = MathHelper.Clamp(value, 0, 1);
        int fillHeight = (int)(triggerValue * fillRect.Height);

        if (fillHeight > 0)
        {
            Rectangle sourceRect = new Rectangle(
                0,
                fillTexture.Height - fillHeight,
                fillTexture.Width,
                fillHeight
            );

            Rectangle destRect = new Rectangle(
                fillRect.X,
                fillRect.Y + (fillRect.Height - fillHeight),
                fillRect.Width,
                fillHeight
            );

            spriteBatch.Draw(fillTexture, destRect, sourceRect, Color.White);
        }
    }
}
