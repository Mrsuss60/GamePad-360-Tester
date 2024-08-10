using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GlowingGuideButton
{
    private Texture2D darkTexture;
    private Texture2D mediumTexture;
    private Texture2D brightTexture;
    private float glowTimer;
    private const float GlowDuration = 4f; 

    public GlowingGuideButton(Texture2D dark, Texture2D medium, Texture2D bright)
    {
        darkTexture = dark;
        mediumTexture = medium;
        brightTexture = bright;
        glowTimer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        glowTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (glowTimer > GlowDuration)
        {
            glowTimer -= GlowDuration;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        float glowPhase = (glowTimer / GlowDuration) * 4f; 

        float opacityDark = 1f;
        float opacityMedium = 0f;
        float opacityBright = 0f;

        if (glowPhase < 1f) 
        {
            opacityMedium = MathHelper.SmoothStep(0f, 1f, glowPhase);
        }
        else if (glowPhase < 2f) 
        {
            opacityMedium = 1f;
            opacityBright = MathHelper.SmoothStep(0f, 1f, glowPhase - 1f);
        }
        else if (glowPhase < 3f) 
        {
            opacityMedium = 1f;
            opacityBright = MathHelper.SmoothStep(1f, 0f, glowPhase - 2f);
        }
        else 
        {
            opacityMedium = MathHelper.SmoothStep(1f, 0f, glowPhase - 3f);
        }

        spriteBatch.Draw(darkTexture, position, Color.White * opacityDark);
        spriteBatch.Draw(mediumTexture, position, Color.White * opacityMedium);
        spriteBatch.Draw(brightTexture, position, Color.White * opacityBright);
    }
}