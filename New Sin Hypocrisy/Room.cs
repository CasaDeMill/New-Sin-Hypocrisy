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
    class Room
    {
        public Rectangle RoomSize { get; set; }
        public int RoomNumber { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D Texture2 { get; set; }
        public Texture2D Texture3 { get; set; }
        public List<Door> Doors { get; set; }
        public string Name { get; set; }
        public List<Rectangle> Walls { get; set; }
        public List<Item> Items { get; set; }
        public float PlayerScale { get; set; }
        public List<Character> Characters { get; set; }
        public int WalkSound { get; set; }
        public bool Outside { get; set; }
        public float roomWidth;
        private int noOftextures;

        public Room(int roomNumber, Texture2D texture, List<Door> doors, List<Rectangle> walls, List<Item> items, List<Character> characters, Rectangle roomSize, string name, float playerScale, int walkSound, bool outside)
        {
            this.RoomNumber = roomNumber;
            this.Texture = texture;
            this.Doors = doors;
            this.Walls = walls;
            this.Items = items;
            this.Characters = characters;
            this.RoomSize = roomSize;
            this.Name = name;
            this.PlayerScale = playerScale;
            noOftextures = 1;
            roomWidth = Texture.Width;
            this.WalkSound = walkSound;
            this.Outside = outside;
        }

        public Room(int roomNumber, Texture2D texture, Texture2D texture2, List<Door> doors, List<Rectangle> walls, List<Item> items, List<Character> characters, Rectangle roomSize, string name, float playerScale, int walkSound, bool outside)
        {
            this.RoomNumber = roomNumber;
            this.Texture = texture;
            this.Texture2 = texture2;
            this.Doors = doors;
            this.Walls = walls;
            this.Items = items;
            this.Characters = characters;
            this.RoomSize = roomSize;
            this.Name = name;
            this.PlayerScale = playerScale;
            noOftextures = 2;
            roomWidth = Texture.Width + Texture2.Width;
            this.WalkSound = walkSound;
            this.Outside = outside;
        }

        public Room(int roomNumber, Texture2D texture, Texture2D texture2, Texture2D texture3, List<Door> doors, List<Rectangle> walls, List<Item> items, List<Character> characters, Rectangle roomSize, string name, float playerScale, int walkSound, bool outside)
        {
            this.RoomNumber = roomNumber;
            this.Texture = texture;
            this.Texture2 = texture2;
            this.Texture3 = texture3;
            this.Doors = doors;
            this.Walls = walls;
            this.Items = items;
            this.Characters = characters;
            this.RoomSize = roomSize;
            this.Name = name;
            this.PlayerScale = playerScale;
            noOftextures = 3;
            roomWidth = Texture.Width + Texture2.Width + Texture3.Width;
            this.WalkSound = walkSound;
            this.Outside = outside;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (noOftextures >= 1)
                spriteBatch.Draw(Texture, new Vector2(0, 0), Color.White * 1f);

            if (noOftextures >= 2)
                spriteBatch.Draw(Texture2, new Vector2(Texture.Width, 0), Color.White * 1f);

            if (noOftextures >= 3)
                spriteBatch.Draw(Texture3, new Vector2(Texture.Width + Texture2.Width, 0), Color.White * 1f);

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Print(spriteBatch);
            }
        }
    }
}
