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
    class Player
    {
        public Vector2 Position { get; set; }
        public List<Animation> Animations { get; set; }
        public Room Room { get; set; }
        public Rectangle PlayerRectangle;
        public bool inputAllowed;
        public Rectangle DoorRectangle;
        public Rectangle TalkRectangle;

        public bool moving;
        private int direction;
        private Vector2 velocity;
        public Rectangle rectL, rectR, rectTop, rectBot;
        public  float idleTimer;

        public Player(Vector2 position, List<Animation> animations, Room room)
        {
            this.Position = position;
            this.Animations = animations;
            this.Room = room;

            PlayerRectangle = new Rectangle((int)Position.X + 5, (int)Position.Y + 20, 90, 50);
            inputAllowed = true;

            moving = false;
            velocity = new Vector2(11, 9);
            //velocity = new Vector2(14, 8);
            direction = 0;
            for (int i = 0; i < Animations.Count; i++)
            {
                Animations[i].Scale = room.PlayerScale;
            }
            idleTimer = 0;
        }

        public void Update(KeyboardState keyboardState, GamePadState gamePadState,Vector2 cameraPos, GameTime gameTime)
        {

            if (inputAllowed)
            {
                //if (keyboardState.IsKeyDown(Keys.W) && Rectangle.Y - velocity.Y > Room.RoomSize.Y)
                //{
                //    Position = new Vector2(Position.X, Position.Y - velocity.Y);
                //    moving = true;
                //}

                //if (keyboardState.IsKeyDown(Keys.S) && Rectangle.Y + Rectangle.Height + velocity.Y < Room.RoomSize.Y + Room.RoomSize.Height)
                //{
                //    Position = new Vector2(Position.X, Position.Y + velocity.Y);
                //    moving = true;
                //}

                //if (keyboardState.IsKeyDown(Keys.A) && Rectangle.X - velocity.X > Room.RoomSize.X)
                //{
                //    Position = new Vector2(Position.X - velocity.X, Position.Y);
                //    moving = true;
                //}

                //if (keyboardState.IsKeyDown(Keys.D) && Rectangle.X + Rectangle.Width + velocity.X < Room.RoomSize.X + Room.RoomSize.Width)
                //{
                //    Position = new Vector2(Position.X + velocity.X, Position.Y);
                //    moving = true;
                //}
                

                for (int i = 0; i < Room.Walls.Count; i++)
                {
                    if (rectTop.Intersects(Room.Walls[i]))
                        Position = new Vector2(Position.X, Room.Walls[i].Bottom - 20 - 150 * Animations[direction].Scale);

                    if (rectBot.Intersects(Room.Walls[i]))
                        Position = new Vector2(Position.X, Room.Walls[i].Top);

                    if (rectR.Intersects(Room.Walls[i]))
                        Position = new Vector2(Room.Walls[i].Left - 65 - 150 * Animations[direction].Scale, Position.Y);

                    if (rectL.Intersects(Room.Walls[i]))
                        Position = new Vector2(Room.Walls[i].Right + 30 - 150 * Animations[direction].Scale, Position.Y);
                }

                if (keyboardState.IsKeyDown(Keys.W) && rectBot.Y - velocity.Y > Room.RoomSize.Y || gamePadState.ThumbSticks.Left.Y > 0.1f && rectBot.Y - velocity.Y > Room.RoomSize.Y)
                {
                    direction = 3;
                    Position = new Vector2(Position.X, Position.Y - velocity.Y);

                    if(keyboardState.IsKeyUp(Keys.A))
                        Position = new Vector2(Position.X, Position.Y);

                    moving = true;
                    idleTimer = 0;
                }

                if (keyboardState.IsKeyDown(Keys.S) && rectBot.Y + rectBot.Height + velocity.Y < Room.RoomSize.Y + Room.RoomSize.Height || gamePadState.ThumbSticks.Left.Y < -0.1f && PlayerRectangle.Y + PlayerRectangle.Height + velocity.Y < Room.RoomSize.Y + Room.RoomSize.Height)
                {
                    direction = 1;
                    Position = new Vector2(Position.X, Position.Y + velocity.Y);

                    if (keyboardState.IsKeyUp(Keys.D))
                        Position = new Vector2(Position.X, Position.Y);

                    moving = true;
                    idleTimer = 0;
                }

                if (keyboardState.IsKeyDown(Keys.A) && rectBot.X - velocity.X > Room.RoomSize.X || gamePadState.ThumbSticks.Left.X < -0.1f && PlayerRectangle.X - velocity.X > Room.RoomSize.X)
                {
                    direction = 2;
                    Position = new Vector2(Position.X - velocity.X, Position.Y);
                    moving = true;
                    idleTimer = 0;
                }

                if (keyboardState.IsKeyDown(Keys.D) && rectBot.X + rectBot.Width + velocity.X < Room.RoomSize.X + Room.RoomSize.Width || gamePadState.ThumbSticks.Left.X > 0.1f && PlayerRectangle.X + PlayerRectangle.Width + velocity.X < Room.RoomSize.X + Room.RoomSize.Width)
                {
                    direction = 0;
                    Position = new Vector2(Position.X + velocity.X, Position.Y);
                    moving = true;
                    idleTimer = 0;
                }
            }

            //if (moving)
            //    Animations[direction].Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.S))
                moving = false;

            if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.D))
                moving = false;

            if (!moving)
            {
                idleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (direction == 0 || direction == 1)
                    direction = 4;

                if (direction == 2 || direction == 3)
                    direction = 5;

                //if (idleTimer <= 2)
                //    direction = 4;

                if (idleTimer > 5 && direction == 4)
                {
                    direction = 6;
                    Animations[direction].Reset();
                }

                if (idleTimer > 5 && direction == 5)
                {
                    direction = 7;
                    Animations[direction].Reset();
                }

                if (Animations[direction].currentFrameX + 1 == Animations[direction].FramesX && direction == 6 && Animations[direction].currentFrameY + 1 == Animations[direction].FramesY)
                {
                    direction = 4;
                    idleTimer = 0;
                }

                if (Animations[direction].currentFrameX + 1 == Animations[direction].FramesX && direction == 7 && Animations[direction].currentFrameY + 1 == Animations[direction].FramesY)
                {
                    direction = 5;
                    idleTimer = 0;
                }
            }

            Animations[direction].Update(gameTime);

            if (keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.S) && gamePadState.ThumbSticks.Left.X == 0 && gamePadState.ThumbSticks.Left.Y == 0)
                moving = false;

            rectL = new Rectangle((int)(Position.X - 30 + 150 * Animations[direction].Scale), (int)(Position.Y + 35 + 150 * Animations[direction].Scale), 20, 30);
            rectR = new Rectangle((int)(Position.X + 45 + 150 * Animations[direction].Scale), (int)(Position.Y + 35 + 150 * Animations[direction].Scale), 20, 30);
            rectTop = new Rectangle((int)(Position.X - 10 + 150 * Animations[direction].Scale), (int)(Position.Y + 20 + 150 * Animations[direction].Scale), 55, 20);
            rectBot = new Rectangle((int)(Position.X - 10 + 150 * Animations[direction].Scale), (int)(Position.Y + 60 + 150 * Animations[direction].Scale), 55, 20);
            PlayerRectangle = new Rectangle((int)(Position.X + 120 * Animations[direction].Scale), (int)Position.Y, 100, 50);
            DoorRectangle = new Rectangle((int)(Position.X + 120 * Animations[direction].Scale), (int)Position.Y, 100, 200);
            TalkRectangle = new Rectangle((int)(Position.X + 90 * Animations[direction].Scale), (int)Position.Y - 120, 150, 250);

            Animations[direction].Scale = Room.PlayerScale - (900 - Position.Y) * 0.001f;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Animations[direction].Draw(spriteBatch, new Vector2(Position.X - 100 * Animations[direction].Scale, Position.Y - 200 * Animations[direction].Scale));
        }
    }


}
