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
    class Item
    {
        //Public properties. The pickuprectangle is a rectangle used to pick up the item. Once in the inventory this rectangle is not in use.
        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public Animation Animation { get; set; }
        public int ItemIndex { get; set; }

        public Vector2 Position { get; set; }
        public bool Clicked { get; set; }
        public SpriteFont Font { get; set; }

        public Vector2 ScreenCenter { get; set; }
        public Vector2 TextureCenter { get; set; }
        public Rectangle PickUpRectangle { get; set; }
        //the pickup bool is not ideal. This to make sure that the item is picked up and it will work as an inventory item when picked up.
        public bool PickUp { get; set; }
        public int Page { get; set; }

        //Private
        private Rectangle drawArea;

        //Constructor
        public Item(string name, int itemIndex, Animation animation, Texture2D texture, Vector2 position, bool clicked, SpriteFont font, Vector2 screenCenter, Vector2 textureCenter, Rectangle pickUpRectangle, bool pickUp, int page)
        {
            Name = name;
            Texture = texture;
            ItemIndex = itemIndex;
            this.Animation = animation;
            Position = position;
            Clicked = clicked;
            Font = font;
            ScreenCenter = screenCenter;
            TextureCenter = textureCenter;
            PickUpRectangle = pickUpRectangle;
            PickUp = pickUp;
            Page = page;
        }

        public void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
        }

        //this draws the item and when clicked in inventory draws it larger in the center of the screen
        public void Print(SpriteBatch spritebatch)
        {
            if (PickUp == false)
            {
                Animation.Draw(spritebatch, Position);
                //spritebatch.Draw(Texture, InventoryPosition, Color.White);
            }
            if (PickUp == true)
            {

                if (!Clicked)
                {
                    //spritebatch.Draw(Texture, drawArea = new Rectangle((int)Position.X, (int)Position.Y, 50, 50), Color.White);
                    spritebatch.Draw(Texture, drawArea = new Rectangle((int)Position.X, (int)Position.Y, 50, 50), Color.White);

                }
                if (Clicked)
                {
                    spritebatch.Draw(Texture, drawArea = new Rectangle((int)ScreenCenter.X, (int)ScreenCenter.Y, 400, 400), null, Color.White, 0f, TextureCenter, SpriteEffects.None, 0f);

                    spritebatch.DrawString(Font, Name, new Vector2(20, 20), Color.White);
                }
            }
        }
    }
}
