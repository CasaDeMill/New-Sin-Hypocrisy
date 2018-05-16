using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace New_Sin_Hypocrisy
{
    class Animation
    {
        public Texture2D Texture { get; set; }
        public int FramesX { get; set; }
        public int FramesY { get; set; }
        public int TimePerFrame { get; set; }
        public float Scale { get; set; }

        public int currentFrameX;
        public int currentFrameY;
        private float timer;
        private Rectangle sourceRect;

        public Animation(Texture2D texture, int framesX, int framesY, int timerPerFrame, float scale)
        {
            this.Texture = texture;
            this.FramesX = framesX;
            this.FramesY = framesY;
            this.TimePerFrame = timerPerFrame;
            this.Scale = scale;

            currentFrameX = 0;
            currentFrameY = 0;
        }

        public void Reset()
        {
            currentFrameX = 0;
            currentFrameY = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > TimePerFrame)
            {
                currentFrameX++;

                if (currentFrameX >= FramesX)
                {
                    currentFrameY++;

                    if (currentFrameY >= FramesY)
                        currentFrameY = 0;

                    currentFrameX = 0;
                }

                timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 followingPoint)
        {
            sourceRect = new Rectangle((Texture.Width / FramesX) * currentFrameX, (Texture.Height / FramesY) * currentFrameY, (Texture.Width / FramesX), (Texture.Height / FramesY));
            spriteBatch.Draw(this.Texture, followingPoint, sourceRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }
}
