using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GamepadTester;

namespace GamepadTester
{
    public class GlowingGuideButton
    {
        private Texture2D TextureB4;
        private Texture2D TextureB3;
        private Texture2D TextureB2;
        private Texture2D TextureB1;
        private float glowTimer;
        private const float GlowDuration = 3.5f;

        public GlowingGuideButton(GameContent content)
        {
            TextureB4 = content.GuideB4Texture;
            TextureB3 = content.GuideB3Texture;
            TextureB2 = content.GuideB2Texture;
            TextureB1 = content.GuideB1Texture;
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

            float glowPhase = (glowTimer / GlowDuration) * 6f;

            float opacity1 = 0f;
            float opacity2 = 0f;
            float opacity3 = 0f;
            float opacity4 = 0f;

            if (glowPhase < 1f)
            {
                opacity1 = MathHelper.Lerp(1f, 0f, glowPhase);
                opacity2 = MathHelper.Lerp(0f, 1f, glowPhase);
            }
            else if (glowPhase < 2f)
            {
                opacity2 = MathHelper.Lerp(1f, 0f, glowPhase - 1f);
                opacity3 = MathHelper.Lerp(0f, 1f, glowPhase - 1f);
            }
            else if (glowPhase < 3f)
            {
                opacity3 = MathHelper.Lerp(1f, 0f, glowPhase - 2f);
                opacity4 = MathHelper.Lerp(0f, 1f, glowPhase - 2f);
            }
            else if (glowPhase < 4f)
            {
                opacity4 = MathHelper.Lerp(1f, 0f, glowPhase - 3f);
                opacity3 = MathHelper.Lerp(0f, 1f, glowPhase - 3f);
            }
            else if (glowPhase < 5f)
            {
                opacity3 = MathHelper.Lerp(1f, 0f, glowPhase - 4f);
                opacity2 = MathHelper.Lerp(0f, 1f, glowPhase - 4f);
            }
            else
            {
                opacity2 = MathHelper.Lerp(1f, 0f, glowPhase - 5f);
                opacity1 = MathHelper.Lerp(0f, 1f, glowPhase - 5f);
            }

            spriteBatch.Draw(TextureB1, position, Color.White * opacity1);
            spriteBatch.Draw(TextureB2, position, Color.White * opacity2);
            spriteBatch.Draw(TextureB3, position, Color.White * opacity3);
            spriteBatch.Draw(TextureB4, position, Color.White * opacity4);
        }
    }
}