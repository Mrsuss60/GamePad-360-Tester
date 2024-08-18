using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamepadTester;

public class LTRT
{
    private GameContent gameContent;
    private Rectangle leftTriggerBarRect;
    private Rectangle rightTriggerBarRect;
    private Rectangle leftTriggerFillRect;
    private Rectangle rightTriggerFillRect;

    public Vector2 LeftFillOffset { get; set; }
    public Vector2 RightFillOffset { get; set; }

    public LTRT(GameContent content, Vector2 gamepadPosition)
    {
        gameContent = content;

        leftTriggerBarRect = new Rectangle(
            (int)gamepadPosition.X,
            (int)gamepadPosition.Y,
            content.LeftTriggerBarTexture.Width,
            content.LeftTriggerBarTexture.Height);

        rightTriggerBarRect = new Rectangle(
            (int)gamepadPosition.X,
            (int)gamepadPosition.Y,
            content.RightTriggerBarTexture.Width,
            content.RightTriggerBarTexture.Height);

        LeftFillOffset = new Vector2(87, 30);
        RightFillOffset = new Vector2(598, 30); 

        UpdateFillPositions();
    }

    public void UpdateFillPositions()
    {
        leftTriggerFillRect = new Rectangle(
            leftTriggerBarRect.X + (int)LeftFillOffset.X,
            leftTriggerBarRect.Y + (int)LeftFillOffset.Y,
            gameContent.LeftTriggerFillTexture.Width,
            gameContent.LeftTriggerFillTexture.Height
        );

        rightTriggerFillRect = new Rectangle(
            leftTriggerBarRect.X + (int)RightFillOffset.X,
            leftTriggerBarRect.Y + (int)RightFillOffset.Y,
            gameContent.RightTriggerFillTexture.Width,
            gameContent.RightTriggerFillTexture.Height
        );
    }

    public void Draw(SpriteBatch spriteBatch, GamePadState currentState)
    {
        spriteBatch.Draw(gameContent.LeftTriggerBarTexture, leftTriggerBarRect, Color.White);
        spriteBatch.Draw(gameContent.RightTriggerBarTexture, rightTriggerBarRect, Color.White);

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