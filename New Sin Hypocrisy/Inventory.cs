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
    class Inventory
    {
        //Public properties
        public List<Item> Items { get; set; }
        public List<Vector2> positionInInventory;

        public List<Rectangle> InventoryRectangles { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D SecondTexture { get; set; }
        public Vector2 Position { get; set; }
        public bool Show { get; set; }
        public int OpenSpeed { get; set; }
        public Rectangle ButtonRectangle { get; set; }

        public Rectangle DownArrow { get; set; }
        public Rectangle UpArrow { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }
        //private stuff keep out
        private int pages = 2;
        private Rectangle DrawOnlyButton;

        public Inventory(List<Item> items, Texture2D texture, Texture2D secondTexture, Vector2 position, List<Rectangle> inventoryRectangles, bool show, int openSpeed, Rectangle buttonRectangle, Rectangle downArrow, Rectangle upArrow, int itemsPerPage, int page)
        {
            Items = items;
            ItemsPerPage = itemsPerPage;
            Texture = texture;
            Position = position;
            InventoryRectangles = inventoryRectangles;
            Show = show;
            OpenSpeed = openSpeed;
            ButtonRectangle = buttonRectangle;
            SecondTexture = secondTexture;
            DownArrow = downArrow;
            UpArrow = upArrow;
            Page = page;

        }

        public Inventory(List<Item> items)
        {
            positionInInventory = new List<Vector2>();
            Items = items;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i % 2 == 0)
                    positionInInventory.Add(new Vector2(1, i / 2));

                if (i % 2 != 0)
                    positionInInventory.Add(new Vector2(0, i / 2));
            }
        }

        //Move method to open inventory
        public void Move()
        {
            Position = new Vector2(Position.X - OpenSpeed, Position.Y);
            if (Position.X < 1300)
            {
                Position = new Vector2(1300, 0);
            }
        }

        //Close method to close inventory
        public void Close()
        {
            Position = new Vector2(Position.X + OpenSpeed, Position.Y);
            if (Position.X > 1550)
            {
                Position = new Vector2(1550, 0);
            }
        }

        //Scroll down a page
        public void Down()
        {
            Page++;
            if (Page >= pages)
            {
                Page = pages;
            }
        }


        //Scroll up a page
        public void Up()
        {
            Page--;
            if (Page <= pages)
            {
                Page = 1;
            }
        }

        //Draws inventory


        public void Print(SpriteBatch spritebatch)
        {
            if (Position.X < 1550)
            {
                if (Page == 1)
                {
                    spritebatch.Draw(Texture, Position, Color.White);
                }
                if (Page == 2)
                {
                    spritebatch.Draw(SecondTexture, Position, Color.White);
                }
            }
            if (Position.X >= 1550)
            {
                spritebatch.Draw(Texture, Position, DrawOnlyButton = new Rectangle(0, 0, 50, 50), Color.White);
            }
        }
    }
}
