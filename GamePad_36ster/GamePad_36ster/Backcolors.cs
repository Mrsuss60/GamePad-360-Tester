using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Backcolors
{
    private Color topLeft = new Color(52, 152, 219);
    private Color topRight = new Color(41, 128, 185);
    private Color bottom = new Color(236, 240, 241);
    private bool isActive = false;
    private int currentColorIndex = 0;
    private int currentComponentIndex = 0;
    private SpriteFont font;
    private const float AdjustmentSpeed = 120f; 
    public Backcolors(SpriteFont font)
    {
        this.font = font;
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }

    public bool IsActive
    {
        get { return isActive; }
    }

    public void Update(GamePadState currentState, GamePadState previousState, float deltaTime)
    {
        if (!isActive) return;

        if (currentState.DPad.Up == ButtonState.Pressed)
            AdjustCurrentComponent(1, deltaTime);
        else if (currentState.DPad.Down == ButtonState.Pressed)
            AdjustCurrentComponent(-1, deltaTime);

        if (currentState.DPad.Right == ButtonState.Pressed && previousState.DPad.Right == ButtonState.Released)
            currentComponentIndex = (currentComponentIndex + 1) % 3;
        if (currentState.DPad.Left == ButtonState.Pressed && previousState.DPad.Left == ButtonState.Released)
            currentComponentIndex = (currentComponentIndex + 2) % 3;
        if (currentState.Buttons.A == ButtonState.Pressed && previousState.Buttons.A == ButtonState.Released)
            currentColorIndex = (currentColorIndex + 1) % 3;
        if (currentState.Buttons.B == ButtonState.Pressed && previousState.Buttons.B == ButtonState.Released)
            isActive = false;
    }

    private void AdjustCurrentComponent(int direction, float deltaTime)
    {
        Color colorToAdjust = GetCurrentColor();
        byte[] colorComponents = new byte[] { colorToAdjust.R, colorToAdjust.G, colorToAdjust.B };

        float adjustment = direction * AdjustmentSpeed * deltaTime;
        colorComponents[currentComponentIndex] = (byte)MathHelper.Clamp(colorComponents[currentComponentIndex] + adjustment, 0, 255);

        SetCurrentColor(new Color(colorComponents[0], colorComponents[1], colorComponents[2]));
    }

    private Color GetCurrentColor()
    {
        if (currentColorIndex == 0) return topLeft;
        if (currentColorIndex == 1) return topRight;
        return bottom;
    }

    private void SetCurrentColor(Color color)
    {
        if (currentColorIndex == 0) topLeft = color;
        else if (currentColorIndex == 1) topRight = color;
        else bottom = color;
    }

    public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
    {
        if (!isActive) return;

        string[] colorNames = { "Top Left", "Top Right", "Bottom" };

        Vector2 centerScreen = new Vector2(screenWidth / 2f, screenHeight / 2f);
        float lineHeight = font.LineSpacing * 1.5f;

        for (int i = 0; i < 3; i++)
        {
            Color color = i == 0 ? topLeft : (i == 1 ? topRight : bottom);
            string text = string.Format("{0}: Red:{1} Green:{2} Blue:{3}", colorNames[i], color.R, color.G, color.B);
            Vector2 size = font.MeasureString(text);
            Vector2 position = centerScreen + new Vector2(-size.X / 2, -lineHeight * 2 + i * lineHeight);
            Color textColor = i == currentColorIndex ? Color.Yellow : Color.Black;

            if (i == currentColorIndex)
            {
                spriteBatch.DrawString(font, ">", position - new Vector2(20, 0), Color.Yellow);
            }

            spriteBatch.DrawString(font, text, position, textColor);

            if (i == currentColorIndex)
            {
                Vector2 componentPos = position + new Vector2(size.X / 2 + 10, 0);
            }
        }

        string instructions = "D-Pad: change color values | A: Switch up or down | B: Exit";
        Vector2 instructionsSize = font.MeasureString(instructions);
        Vector2 instructionsPosition = new Vector2(screenWidth / 2 - instructionsSize.X / 2, screenHeight - 30);
        spriteBatch.DrawString(font, instructions, instructionsPosition, Color.Black);
    }

    public void SetupGradient(out Color topLeft, out Color topRight, out Color bottom)
    {
        topLeft = this.topLeft;
        topRight = this.topRight;
        bottom = this.bottom;
    }
}
