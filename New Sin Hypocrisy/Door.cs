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
    class Door
    {
        public int ConnectedRoom;
        public Rectangle DoorRect;
        public int DoorNumber;
        public int ConnectedDoor;
        public int DoorSound;

        public Door(int connectedRoom, Rectangle doorRect, int doorNumber, int connectedDoor, int doorSound)
        {
            this.ConnectedRoom = connectedRoom;
            this.DoorRect = doorRect;
            this.DoorNumber = doorNumber;
            this.ConnectedDoor = connectedDoor;
            this.DoorSound = doorSound;
        }
    }
}
