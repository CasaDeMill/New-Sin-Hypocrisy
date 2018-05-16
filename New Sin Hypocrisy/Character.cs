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
    class Character : Conversation
    {
        public Rectangle TalkRectangle { get; set; }
        public Vector2 Position { get; set; }
        public Animation Animation { get; set; }

        public Character(string[] talk, bool printText, SpriteFont font, Vector2 textPosition, bool speedUp, bool isReset, bool close, long conversationIndexNumber, int conversationIndexNumber2, int count, Rectangle talkRectangle, Vector2 position, Animation animation, bool typing, int lastKey)
            : base(talk, printText, font, textPosition, speedUp, isReset, close, conversationIndexNumber, conversationIndexNumber2, count, typing, lastKey)
        {
            TalkRectangle = talkRectangle;
            Position = position;
            Animation = animation;
        }

        public override void Print(SpriteBatch spriteBatch, GameTime gameTime, KeyboardState keyboardState, KeyboardState prevKeyboardState)
        {
            Animation.Update(gameTime);
            Animation.Draw(spriteBatch, Position);
            //spriteBatch.Draw(Texture, Position, Color.White);
            //if (PrintText)
            //{
            //    base.Print(spriteBatch, gameTime, keyboardState, prevKeyboardState);
            //}

        }

        public void PrintConv(SpriteBatch spriteBatch, GameTime gameTime, KeyboardState keyboardState, KeyboardState prevKeyboardState)
        {
            if (PrintText)
            {
                base.Print(spriteBatch, gameTime, keyboardState, prevKeyboardState);
            }

        }
    }
}
