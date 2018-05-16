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
    class Dialogue
    {
        public Texture2D DialogueSquareTexture { get; set; }


        public bool Show { get; set; }
        public Vector2 Position { get; set; }

        public Dialogue(Texture2D dialogueSquareTexture, Vector2 position, bool show)
        {
            DialogueSquareTexture = dialogueSquareTexture;

            Position = position;
            Show = show;
        }

        public void print(SpriteBatch spriteBatch)
        {
            if (Show)
            {
                spriteBatch.Draw(DialogueSquareTexture, Position, Color.White);
            }
        }
    }
}
