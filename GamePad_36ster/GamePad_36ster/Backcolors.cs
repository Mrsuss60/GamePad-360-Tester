using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Backcolors
{
    private readonly Color defaultTopLeft = new Color(151, 179, 219);
    private readonly Color defaultTopRight = new Color(110, 128, 214);
    private readonly Color defaultBottom = new Color(233, 244, 255);

    private Color topLeft;
    private Color topRight;
    private Color bottom;

    private bool isActive = false;
    private int currentColorIndex = 0;
    private int currentComponentIndex = 0;
    private SpriteFont font;
    private const float AdjustmentSpeed = 60f; // Dont go below 60

    public Backcolors(SpriteFont font)
    {
        this.font = font;
        ResetColorsToDefault();
    }

    public void ToggleActive()
    {
        isActive = !isActive;
        if (!isActive)
        {
            SaveLoad.SaveColors(this);
        }
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
        if (currentState.Buttons.Y == ButtonState.Pressed && previousState.Buttons.Y == ButtonState.Released)
            currentColorIndex = (currentColorIndex + 2) % 3;

        if (currentState.Buttons.B == ButtonState.Pressed && previousState.Buttons.B == ButtonState.Released)
            ToggleActive();

        if (currentState.Buttons.X == ButtonState.Pressed && previousState.Buttons.X == ButtonState.Released)
        {
            ResetColorsToDefault();
        }
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


    private void ResetColorsToDefault()
    {
        topLeft = defaultTopLeft;
        topRight = defaultTopRight;
        bottom = defaultBottom;
    }

    public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
    {
        if (!isActive) return;

        string[] colorNames = { "Top Left", "Top Right", "Bottom" };

        float column1_X = screenWidth / 2f - 200;
        float column2_X = screenWidth / 2f - 103;
        float column3_X = screenWidth / 2f - 20;
        float column4_X = screenWidth / 2f + 85;
        float indicator_X_offset = -30;

        Vector2 centerScreenY = new Vector2(0, screenHeight / 2f);
        float lineHeight = font.LineSpacing * 1.5f;
        float startY = centerScreenY.Y - lineHeight;

        for (int i = 0; i < 3; i++)
        {
            Color color = i == 0 ? topLeft : (i == 1 ? topRight : bottom);
            float currentLineY = startY + i * lineHeight;
            Color textColor = Color.Black;

            if (i == currentColorIndex)
            {
                Vector2 indicatorPosition = new Vector2(column1_X + indicator_X_offset, currentLineY);
                spriteBatch.DrawString(font, ">", indicatorPosition, Color.Yellow);
                textColor = Color.Yellow;
            }


            Vector2 colorNamePosition = new Vector2(column1_X, currentLineY);
            spriteBatch.DrawString(font, colorNames[i] + ":", colorNamePosition, textColor);


            Color highlightColor = Color.Black;
            if (i == currentColorIndex)
            {
                if (currentComponentIndex == 0) highlightColor = Color.Red;
                else if (currentComponentIndex == 1) highlightColor = Color.Green;
                else if (currentComponentIndex == 2) highlightColor = Color.Blue;
            }



            string redText = string.Format("Red:{0}", color.R);
            Vector2 redPosition = new Vector2(column2_X, currentLineY);
            Color redColor = (i == currentColorIndex && currentComponentIndex == 0) ? highlightColor : Color.Black;
            spriteBatch.DrawString(font, redText, redPosition, redColor);


            string greenText = string.Format("Green:{0}", color.G);
            Vector2 greenPosition = new Vector2(column3_X, currentLineY);
            Color greenColor = (i == currentColorIndex && currentComponentIndex == 1) ? highlightColor : Color.Black;
            spriteBatch.DrawString(font, greenText, greenPosition, greenColor);


            string blueText = string.Format("Blue:{0}", color.B);
            Vector2 bluePosition = new Vector2(column4_X, currentLineY);
            Color blueColor = (i == currentColorIndex && currentComponentIndex == 2) ? highlightColor : Color.Black;
            spriteBatch.DrawString(font, blueText, bluePosition, blueColor);
        }


        string instructions = "D-Pad: change color/value | A: Next Color | Y: Previous Color | X: Reset Colors | B: Go Back/Save";
        Vector2 instructionsSize = font.MeasureString(instructions);
        Vector2 instructionsPosition = new Vector2(screenWidth / 2 - instructionsSize.X / 2, screenHeight - 55);
        spriteBatch.DrawString(font, instructions, instructionsPosition, Color.Black);
    }

    public void SetupGradient(out Color topLeft, out Color topRight, out Color bottom)
    {
        topLeft = this.topLeft;
        topRight = this.topRight;
        bottom = this.bottom;
    }

    public void SetColors(Color topLeft, Color topRight, Color bottom)
    {
        this.topLeft = topLeft;
        this.topRight = topRight;
        this.bottom = bottom;
    }
}
