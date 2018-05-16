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
using System.IO;

namespace New_Sin_Hypocrisy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public enum GameState
        {
            MainMenu,
            InGame,
            Inventory,
            Pause,
            Conversation,
            Options,
            FastTravel,
            End
        }

        GameState currentState;

        Matrix camera;
        Vector2 cameraPos;

        KeyboardState keyboardState;
        KeyboardState previousKS;

        GamePadState gamePadState;
        GamePadState previousGamePadState;

        Player player;
        List<Animation> playerAnimations;
        int currentRoom;
        int currentDoor;

        List<Room> rooms;

        List<Door> BobbyRoomDoors;
        List<Rectangle> BobbyRoomWalls;
        List<Item> BobbyRoomItems;
        List<Character> BobbyRoomCharacters;

        List<Door> BobbyOutsideDoors;
        List<Rectangle> BobbyOutsideWalls;

        List<Door> Road1Doors;

        List<Door> SchoolDoors;

        List<Door> Road2Doors;

        List<Door> Road3Doors;
        List<Rectangle> Road3Walls;

        List<Door> GasStationDoors;
        List<Rectangle> GasStationWalls;
        List<Character> GasStationCharacters;

        List<Door> MansionOutsideDoors;
        List<Rectangle> MansionOutsideWalls;
        List<Character> MansionCharacters;

        List<Door> MansionInsideDoors;
        List<Item> MansionInsideItems;

        List<Door> FarmDoors;
        List<Rectangle> FarmWalls;
        List<Character> FarmCharacters;

        List<Door> HotelDoors;
        List<Character> HotelCharacters;

        List<Door> ApartmentDoors;
        List<Rectangle> ApartmentWalls;

        List<Door> StoreDoors;

        List<Door> TownSquareDoors;
        List<Rectangle> TownSquareWalls;
        List<Character> TownSquareCharacters;
        List<Item> TownSquareItems;

        List<Door> CityHallDoors;
        List<Rectangle> CityHallWalls;
        List<Character> CityHallCharacters;

        List<Character> StoreCharacters;

        float transparent;
        bool transparentBool;
        bool changeRoom;

        bool showRoomName;
        float showRoomNameTimer;

        float textFadeTimer;

        SpriteFont font;

        #region inventory
        Inventory inventory;
        List<Item> items;
        List<Rectangle> rectanglePositions;
        Rectangle mouse;
        MouseState mouseState;
        MouseState prevMouseState;
        KeyboardState prevKeyboardState;
        Rectangle buttonRectangle;
        Rectangle downArrow;
        Rectangle upArrow;
        Texture2D rect;
        float keyTimer;
        bool itemClicked;
        #endregion

        int pauseSelection;
        int optionsSelection;

        Vector2 inventorySelection;
        Rectangle inventorySelectionRectangle;

        Dialogue dialogue;
        bool conversationStarted;

        int newRoom;

        List<Item> allItems;
        string[] savedItemIndexes;

        float volume;

        int menuSelection;

        int doorInt;

      

        Conversation easonPhone;
        Conversation rangerPhone;
        #region Songs
        SoundEffect murder;
        SoundEffect peaks;
        SoundEffect diner;
        SoundEffect storm;
       
        #endregion

        #region SoundEffects
        List<SoundEffectInstance> soundEffects;
        SoundEffect doorTalk;
        SoundEffect doorReverb;
        SoundEffect door;
        SoundEffect ether;
        SoundEffect footstepSticky;
        SoundEffect footstep;
        SoundEffect footstepReverb;
        SoundEffect weird;
        SoundEffect stairstep;
        SoundEffect doorOutside;
        SoundEffect pickUp;
        SoundEffect wind;
        SoundEffect pen;
        SoundEffect pistol;
        SoundEffect phone;
        #endregion 

        bool newGame;

        bool easonCalled;

        bool rangerCalled;

        bool changed;

        bool anTalked;

        bool easonTalked;

        bool poem;
        int callTimer;

        string[] savedConversationStates;
        string[] saveCalls;


        Texture2D easonEnd;
        Texture2D augustEnd;
        Texture2D catherineEnd;
        Texture2D janeEnd;
        Texture2D donnaEnd;
        Texture2D gameOver;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = true;

            IsMouseVisible = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            currentState = GameState.MainMenu;

            camera = new Matrix();
            cameraPos = Vector2.Zero;

            playerAnimations = new List<Animation>();
            currentRoom = int.Parse(File.ReadAllText(Content.RootDirectory + "//SaveRoom.txt"));
            currentDoor = 0;

            rooms = new List<Room>();

            BobbyRoomDoors = new List<Door>();
            BobbyRoomItems = new List<Item>();
            BobbyRoomWalls = new List<Rectangle>();
            BobbyRoomCharacters = new List<Character>();

            BobbyOutsideDoors = new List<Door>();
            BobbyOutsideWalls = new List<Rectangle>();

            Road1Doors = new List<Door>();

            SchoolDoors = new List<Door>();

            Road2Doors = new List<Door>();

            Road3Doors = new List<Door>();
            Road3Walls = new List<Rectangle>();

            GasStationDoors = new List<Door>();
            GasStationWalls = new List<Rectangle>();
            GasStationCharacters = new List<Character>();

            MansionOutsideDoors = new List<Door>();
            MansionOutsideWalls = new List<Rectangle>();

            MansionInsideDoors = new List<Door>();
            MansionCharacters = new List<Character>();
            MansionInsideItems = new List<Item>();

            FarmDoors = new List<Door>();
            FarmWalls = new List<Rectangle>();
            FarmCharacters = new List<Character>();

            HotelDoors = new List<Door>();
            HotelCharacters = new List<Character>();

            ApartmentDoors = new List<Door>();
            ApartmentWalls = new List<Rectangle>();

            StoreDoors = new List<Door>();

            TownSquareDoors = new List<Door>();
            TownSquareWalls = new List<Rectangle>();
            TownSquareCharacters = new List<Character>();
            TownSquareItems = new List<Item>();

            CityHallDoors = new List<Door>();
            CityHallWalls = new List<Rectangle>();
            CityHallCharacters = new List<Character>();

            StoreCharacters = new List<Character>();

            transparent = 0;
            transparentBool = false;

            showRoomName = true;
            showRoomNameTimer = 0f;
            textFadeTimer = 0f;

            camera = Matrix.CreateScale(1) * Matrix.CreateTranslation(0, 0, 0);

            #region initializeItems
            keyTimer = 0;
            rect = Content.Load<Texture2D>("Rect");
            rectanglePositions = new List<Rectangle>();

            //Page should always be 1 in initialize

            mouse = new Rectangle(0, 0, 0, 0);
            mouseState = new MouseState();
            #endregion

            inventorySelection = Vector2.Zero;

            itemClicked = false;

            pauseSelection = 1;
            optionsSelection = 1;
            menuSelection = 1;

            newRoom = 0;

            allItems = new List<Item>();
            savedItemIndexes = new string[allItems.Count];
            savedConversationStates = new string[8];
            saveCalls = new string[2];

            volume = float.Parse(File.ReadAllText(Content.RootDirectory + "//Options.txt"));

            doorInt = 0;

            soundEffects = new List<SoundEffectInstance>();
            saveCalls = File.ReadAllLines(Content.RootDirectory + "//SaveCall.txt");
            newGame = false;
            easonCalled = bool.Parse(saveCalls[0]);
            rangerCalled = bool.Parse(saveCalls[1]);
            changed = false;
            anTalked = false;
            easonTalked = false;
            poem = false;
            callTimer = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            savedConversationStates = File.ReadAllLines(Content.RootDirectory + "//SaveConversation.txt");
            //allItems.Add(new Item("Best detective in town", 0, new Animation(Content.Load<Texture2D>("Inventory/Dale"), 1, 1, 100, 1), Content.Load<Texture2D>("Inventory/Dale"), new Vector2(600,600) , false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), true, 0));
            //allItems.Add(new Item("sw88t snax m9", 1, new Animation(Content.Load<Texture2D>("Inventory/Dorito"), 1, 1, 100, 1), Content.Load<Texture2D>("Inventory/Dorito"), new Vector2(1200, 600), false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), true, 0));
            allItems.Add(new Item("Commie", 0, new Animation(Content.Load<Texture2D>("Inventory/Stalin"), 1, 1, 100, 1), Content.Load<Texture2D>("Inventory/Stalin"), Vector2.Zero, false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), true, 0));
            allItems.Add(new Item(@"A poem signed by August Fresh, 
but it is clearly a Roderick Atkins lyric.", 1, new Animation(Content.Load<Texture2D>("Items/Poem"), 1, 1, 100, 1), Content.Load<Texture2D>("Items/Poem"), new Vector2(4000, 700), false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), true, 0));
            allItems.Add(new Item("Watch", 2, new Animation(Content.Load<Texture2D>("Items/Watch"), 1, 1, 100, 1), Content.Load<Texture2D>("Items/Watch"), new Vector2(4300, 720), false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), true, 0));
            allItems.Add(new Item("Drugs", 3, new Animation(Content.Load<Texture2D>("Items/Drugs"), 1, 1, 100, 1), Content.Load<Texture2D>("Items/Drugs"), new Vector2(5162, 557), false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), true, 0));
            BobbyRoomDoors.Add(new Door(1, new Rectangle(200, 300, 140, 500), 0, 0, 1));
            //BobbyRoomItems.Add(allItems[0]);
            //BobbyRoomItems.Add(allItems[1]);
            BobbyRoomWalls.Add(new Rectangle(3200, 720, 800, 140));
            BobbyRoomCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/AnPhone.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[0]), 1, 0, new Rectangle(3000, 580, 100, 100), new Vector2(3000, 580), new Animation(Content.Load<Texture2D>("Characters/phoneAnimation"), 2, 1, 200, 1f), false,0));

            BobbyOutsideWalls.Add(new Rectangle(1600, 500, 1000, 180));
            BobbyOutsideWalls.Add(new Rectangle(2900, 500, 310, 180));
            BobbyOutsideWalls.Add(new Rectangle(3120, 600, 900, 130));
            BobbyOutsideDoors.Add(new Door(0, new Rectangle(2680, 300, 150, 320), 0, 0,1));
            BobbyOutsideDoors.Add(new Door(2, new Rectangle(3800, 600, 200, 300), 1, 0,0));

            Road1Doors.Add(new Door(1, new Rectangle(0, 150, 200, 900), 0, 1, 0));
            Road1Doors.Add(new Door(3, new Rectangle(5300, 150, 200, 900), 1, 0, 0));
            Road1Doors.Add(new Door(10, new Rectangle(4900, 800, 300, 100), 2, 0, 0));

            SchoolDoors.Add(new Door(2, new Rectangle(0, 100, 400, 900), 0, 1, 0));
            SchoolDoors.Add(new Door(4, new Rectangle(4600, 100, 400, 900), 1, 0, 0));

            Road2Doors.Add(new Door(3, new Rectangle(0, 150, 400, 900), 0, 1, 0));
            Road2Doors.Add(new Door(5, new Rectangle(3600, 150, 400, 800), 1, 0, 0));

            Road3Doors.Add(new Door(4, new Rectangle(0, 200, 200, 550), 0, 1, 0));
            Road3Doors.Add(new Door(6, new Rectangle(3600, 150, 400, 900), 1, 0, 0));
            Road3Doors.Add(new Door(9, new Rectangle(200, 800, 400, 100), 2, 0, 0));
            Road3Walls.Add(new Rectangle(2400, 400, 900, 300));
            Road3Walls.Add(new Rectangle(1650, 500, 750, 150));

            GasStationDoors.Add(new Door(5, new Rectangle(0, 150, 300, 900), 0, 1, 0));
            GasStationDoors.Add(new Door(7, new Rectangle(3300, 750, 500, 100), 1, 0, 0));
            GasStationWalls.Add(new Rectangle(850, 650, 250, 100));
            GasStationCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/August.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[1]), 1, 0, new Rectangle(1550, 420, 350, 350), new Vector2(1550, 420), new Animation(Content.Load<Texture2D>("Characters/AugustAnimation"), 2, 1, 800, 1f), false, 0));

            MansionOutsideDoors.Add(new Door(6, new Rectangle(5600, 0, 400, 900), 0, 1, 0));
            MansionOutsideDoors.Add(new Door(8, new Rectangle(2300, 0, 350, 700), 1, 0, 3));
            MansionOutsideWalls.Add(new Rectangle(1700, 450, 300, 250));
            MansionOutsideWalls.Add(new Rectangle(3000, 450, 300, 250));

            MansionInsideDoors.Add(new Door(7, new Rectangle(5600, 50, 400, 900), 0, 1, 1));
            MansionCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/Catherine.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[2]), 1, 0, new Rectangle(800, 400, 200, 300), new Vector2(800, 400), new Animation(Content.Load<Texture2D>("Characters/AtkinsAnimation"), 2, 1, 800, 1f), false, 0));
            MansionInsideItems.Add(allItems[3]);

            FarmDoors.Add(new Door(5, new Rectangle(0, 500, 400, 200), 0, 2, 0));
            FarmCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/Ricky.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[3]), 1, 0, new Rectangle(3100, 400, 200, 300), new Vector2(3100, 400), new Animation(Content.Load<Texture2D>("Characters/rickyAnimation"), 2, 1, 800, 1f), false, 0));

            HotelDoors.Add(new Door(2, new Rectangle(2400, 350, 500, 300), 0, 2, 0));
            HotelDoors.Add(new Door(11, new Rectangle(5900, 300, 100, 500), 1, 0, 0));
            HotelDoors.Add(new Door(12, new Rectangle(5600, 810, 300, 50), 2, 0, 0));
            HotelCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/Kristoff.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[4]), 1, 0, new Rectangle(3700, 420, 200, 300), new Vector2(3700, 420), new Animation(Content.Load<Texture2D>("Characters/ristoffAnimation"), 2, 1, 400, 1f), false, 0));

            ApartmentDoors.Add(new Door(10, new Rectangle(0, 100, 400, 900), 0, 1, 0));
            ApartmentWalls.Add(new Rectangle(4000, 650, 150, 50));

            StoreDoors.Add(new Door(10, new Rectangle(5600, 300, 100, 350), 0, 2, 0));
            StoreDoors.Add(new Door(13, new Rectangle(5850, 100, 150, 900), 1, 0, 0));
            StoreDoors.Add(new Door(14, new Rectangle(5500, 850, 250, 50), 2, 0, 0));
            StoreCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/Donna.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[5]), 1, 0, new Rectangle(2200, 400, 200, 300), new Vector2(2200, 400), new Animation(Content.Load<Texture2D>("Characters/DonnaAnimation"), 2, 1, 800, 1f), false, 0));
            

            TownSquareDoors.Add(new Door(12, new Rectangle(0, 100, 400, 900), 0, 1, 0));
            TownSquareWalls.Add(new Rectangle(3950, 600, 100, 80));
            TownSquareCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/An.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[6]), 1, 0, new Rectangle(4600, 400, 200, 300), new Vector2(4600, 400), new Animation(Content.Load<Texture2D>("Characters/AnAnimation"), 2, 1, 300, 1f), false, 0));
            TownSquareItems.Add(allItems[1]);
            TownSquareItems.Add(allItems[2]);

            CityHallDoors.Add(new Door(12, new Rectangle(5400, 300, 400, 400), 0, 2, 0));
            CityHallCharacters.Add(new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/Eason.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, int.Parse(savedConversationStates[7]), 1, 0, new Rectangle(3700, 420, 200, 300), new Vector2(3700, 420), new Animation(Content.Load<Texture2D>("Characters/MayorAnimation"), 2, 1, 800, 1f), false, 0));
            CityHallWalls.Add(new Rectangle(1000, 700, 850, 100));
            
            easonPhone = new Conversation(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/EasonPhone.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, 1, 1, 0, false, 0);
            rangerPhone = new Conversation(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/Phone.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, 1, 1, 0, false, 0);
            #region Load Items
            items = new List<Item>();
            //allItems.Add(new Item("THE WU", 3, new Animation(Content.Load<Texture2D>("Items/thewuAni"), 2, 1, 150, 1f), Content.Load<Texture2D>("Items/wutang"), new Vector2(500, 500), false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), false, 0));
            //allItems.Add(new Item("THE WU", new Animation(Content.Load<Texture2D>("Items/thewuAni"), 2, 1, 200, 1f), Content.Load<Texture2D>("Items/wutang"), Vector2.Zero, false, Content.Load<SpriteFont>("font"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), Vector2.Zero, new Rectangle(0, 0, 0, 0), false, 0));

            savedItemIndexes = File.ReadAllLines(Content.RootDirectory + "//SaveItems.txt");

            for (int i = 0; i < savedItemIndexes.Length; i++)
            {
                items.Add(allItems[int.Parse(savedItemIndexes[i])]);
            }

            #endregion

            inventory = new Inventory(items, Content.Load<Texture2D>("Inventory/Inventory"), Content.Load<Texture2D>("Inventory/Inventory2"), new Vector2(1550, 0), rectanglePositions, false, 8, buttonRectangle, downArrow, upArrow, 7, 1);

            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < BobbyRoomItems.Count; j++)
                {
                    if (BobbyRoomItems[j].ItemIndex == items[i].ItemIndex)
                    {
                        BobbyRoomItems.RemoveAt(j);
                    }
                }
            }

            rooms.Add(new Room(0, Content.Load<Texture2D>("Backgrounds/BobbyRoom/1"), Content.Load<Texture2D>("Backgrounds/BobbyRoom/2"), BobbyRoomDoors, BobbyRoomWalls, BobbyRoomItems, BobbyRoomCharacters, new Rectangle(200, 720, 3800, 180), "Bobby's House", 1.3f, 1, false));
            rooms.Add(new Room(1, Content.Load<Texture2D>("Backgrounds/BobbyOutside/1"), Content.Load<Texture2D>("Backgrounds/BobbyOutside/2"), BobbyOutsideDoors, BobbyOutsideWalls, new List<Item>(), new List<Character>(), new Rectangle(1600, 580, 2400, 300), "Outside Bobby's House", 1f, 1, true));
            rooms.Add(new Room(2, Content.Load<Texture2D>("Backgrounds/Road1/1"), Content.Load<Texture2D>("Backgrounds/Road1/2"), Content.Load<Texture2D>("Backgrounds/Road1/3"), Road1Doors, new List<Rectangle>(), new List<Item>(), new List<Character>(), new Rectangle(0, 750, 5500, 150), "Road", 1f, 1, true));
            rooms.Add(new Room(3, Content.Load<Texture2D>("Backgrounds/Kindergarten/1"), Content.Load<Texture2D>("Backgrounds/Kindergarten/2"), Content.Load<Texture2D>("Backgrounds/Kindergarten/3"), SchoolDoors, new List<Rectangle>(), new List<Item>(), new List<Character>(), new Rectangle(0, 700, 5000, 200), "School", 1f, 1, true));
            rooms.Add(new Room(4, Content.Load<Texture2D>("Backgrounds/Road2/1"), Content.Load<Texture2D>("Backgrounds/Road2/2"), Road2Doors, new List<Rectangle>(), new List<Item>(), new List<Character>(), new Rectangle(0, 550, 4000, 350), "Road", 1f, 1, true));
            rooms.Add(new Room(5, Content.Load<Texture2D>("Backgrounds/Road3/1"), Content.Load<Texture2D>("Backgrounds/Road3/2"), Road3Doors, Road3Walls, new List<Item>(), new List<Character>(), new Rectangle(0, 550, 4000, 350), "Road", 1f, 1, true));
            rooms.Add(new Room(6, Content.Load<Texture2D>("Backgrounds/GasStation/1"), Content.Load<Texture2D>("Backgrounds/GasStation/2"), GasStationDoors, GasStationWalls, new List<Item>(), GasStationCharacters, new Rectangle(0, 720, 4000, 180), "August's Gas Station", 1f, 1, false));
            rooms.Add(new Room(7, Content.Load<Texture2D>("Backgrounds/MansionOutside/1"), Content.Load<Texture2D>("Backgrounds/MansionOutside/2"), Content.Load<Texture2D>("Backgrounds/MansionOutside/3"), MansionOutsideDoors, MansionOutsideWalls, new List<Item>(), new List<Character>(), new Rectangle(0, 550, 6000, 350), "Outside the mansion", 1.2f, 1, true));
            rooms.Add(new Room(8, Content.Load<Texture2D>("Backgrounds/MansionInside/1"), Content.Load<Texture2D>("Backgrounds/MansionInside/2"), Content.Load<Texture2D>("Backgrounds/MansionInside/3"), MansionInsideDoors, new List<Rectangle>(), MansionInsideItems, MansionCharacters, new Rectangle(900, 700, 5100, 200), "Inside the mansion", 1.2f, 3, false));
            rooms.Add(new Room(9, Content.Load<Texture2D>("Backgrounds/Farm/1"), Content.Load<Texture2D>("Backgrounds/Farm/2"), Content.Load<Texture2D>("Backgrounds/Farm/3"), FarmDoors, FarmWalls, new List<Item>(), FarmCharacters, new Rectangle(0, 650, 3200, 250), "The farm", 1.2f, 1, true));
            rooms.Add(new Room(10, Content.Load<Texture2D>("Backgrounds/Hotel/1"), Content.Load<Texture2D>("Backgrounds/Hotel/2"), Content.Load<Texture2D>("Backgrounds/Hotel/3"), HotelDoors, new List<Rectangle>(), new List<Item>(), HotelCharacters, new Rectangle(0, 700, 6000, 200), "The Hotel", 1f, 1, true));
            rooms.Add(new Room(11, Content.Load<Texture2D>("Backgrounds/Apartment/1"), Content.Load<Texture2D>("Backgrounds/Apartment/2"), Content.Load<Texture2D>("Backgrounds/Apartment/3"), ApartmentDoors, ApartmentWalls, new List<Item>(), new List<Character>(), new Rectangle(0, 650, 4600, 250), "Old Apartments", 1f, 1, true));
            rooms.Add(new Room(12, Content.Load<Texture2D>("Backgrounds/Store/1"), Content.Load<Texture2D>("Backgrounds/Store/2"), Content.Load<Texture2D>("Backgrounds/Store/3"), StoreDoors, new List<Rectangle>(), new List<Item>(), StoreCharacters, new Rectangle(400, 700, 5600, 200), "The Store", 1.2f, 1, true));
            rooms.Add(new Room(13, Content.Load<Texture2D>("Backgrounds/TownSquare/1"), Content.Load<Texture2D>("Backgrounds/TownSquare/2"), Content.Load<Texture2D>("Backgrounds/TownSquare/3"), TownSquareDoors, TownSquareWalls, TownSquareItems, TownSquareCharacters, new Rectangle(0, 650, 5000, 250), "Town Square", 1.2f, 1, true));
            rooms.Add(new Room(14, Content.Load<Texture2D>("Backgrounds/CityHall/1"), Content.Load<Texture2D>("Backgrounds/CityHall/2"), Content.Load<Texture2D>("Backgrounds/CityHall/3"), CityHallDoors, CityHallWalls, new List<Item>(), CityHallCharacters, new Rectangle(1100, 700, 4900, 200), "City Hall", 1f, 1, true)); 
            

            font = Content.Load<SpriteFont>("roomFont");

            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation1"), 7, 1, 100, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation2"), 4, 1, 150, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation3"), 7, 1, 100, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation4"), 4, 1, 150, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation5"), 7, 2, 150, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation6"), 7, 2, 150, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation7"), 4, 2, 200, 1f));
            playerAnimations.Add(new Animation(Content.Load<Texture2D>("PlayerAnimations/Animation8"), 4, 2, 200, 1f));

            string pX = File.ReadAllLines(Content.RootDirectory + "//SavePosition.txt")[0];
            string pY = File.ReadAllLines(Content.RootDirectory + "//SavePosition.txt")[1];
            player = new Player(new Vector2(float.Parse(pX),float.Parse(pY)), playerAnimations, rooms[currentRoom]);
            //player = new Player(new Vector2(rooms[currentRoom].Doors[0].DoorRect.X + 25, rooms[currentRoom].Doors[0].DoorRect.Y + rooms[currentRoom].Doors[0].DoorRect.Height - 80), playerAnimations, rooms[currentRoom]);
            //player = new Player(new Vector2(, 800), playerAnimations, rooms[currentRoom]);

            dialogue = new Dialogue(Content.Load<Texture2D>("Dialogue"), new Vector2(100, 400), false);
            conversationStarted = false;
            transparent = 1;



            #region LoadSoundEffects
            door = Content.Load<SoundEffect>("Sounds/door");
            doorOutside = Content.Load<SoundEffect>("Sounds/doorOutside");
            doorReverb = Content.Load<SoundEffect>("Sounds/doorReverb");
            doorTalk = Content.Load<SoundEffect>("Sounds/doorTalk");
            stairstep = Content.Load<SoundEffect>("Sounds/stairstep");
            footstep = Content.Load<SoundEffect>("Sounds/footstep");
            footstepReverb = Content.Load<SoundEffect>("Sounds/footstepReverb");
            footstepSticky = Content.Load<SoundEffect>("Sounds/footstepSticky");
            ether = Content.Load<SoundEffect>("Sounds/ether");
            weird = Content.Load<SoundEffect>("Sounds/weird");
            pickUp = Content.Load<SoundEffect>("Sounds/pickUp");
            wind = Content.Load<SoundEffect>("Sounds/wind");
            pen = Content.Load<SoundEffect>("Sounds/pen");
            pistol = Content.Load<SoundEffect>("Sounds/pistol");
            murder = Content.Load<SoundEffect>("Songs/murder");
            storm = Content.Load<SoundEffect>("Songs/storm");
            peaks = Content.Load<SoundEffect>("Songs/peaks");
            diner = Content.Load<SoundEffect>("Songs/diner");
            phone = Content.Load<SoundEffect>("Sounds/phone");

            
            soundEffects.Add(door.CreateInstance());
            soundEffects.Add(doorOutside.CreateInstance());
            soundEffects.Add(doorReverb.CreateInstance());
            soundEffects.Add(doorTalk.CreateInstance());
            soundEffects.Add(stairstep.CreateInstance());
            soundEffects.Add(footstep.CreateInstance());
            soundEffects.Add(footstepReverb.CreateInstance());
            soundEffects.Add(footstepSticky.CreateInstance());
            soundEffects.Add(ether.CreateInstance());
            soundEffects.Add(weird.CreateInstance());
            soundEffects.Add(pickUp.CreateInstance());
            soundEffects.Add(wind.CreateInstance());
            soundEffects.Add(pen.CreateInstance());
            soundEffects.Add(pistol.CreateInstance());
            soundEffects.Add(murder.CreateInstance());
            soundEffects.Add(storm.CreateInstance());
            soundEffects.Add(peaks.CreateInstance());
            soundEffects.Add(diner.CreateInstance());
            soundEffects.Add(phone.CreateInstance());

            soundEffects[5].IsLooped = true;
            soundEffects[7].IsLooped = true;
            soundEffects[6].IsLooped = true;
            soundEffects[4].IsLooped = true;
            soundEffects[8].IsLooped = true;
            soundEffects[9].IsLooped = true;
            soundEffects[11].IsLooped = true;
            soundEffects[12].IsLooped = true;

            foreach (SoundEffectInstance s in soundEffects) { s.Stop(); }
            
            #endregion

            easonEnd = Content.Load<Texture2D>("EasonEnding");
            augustEnd = Content.Load<Texture2D>("AugustEnd");
            catherineEnd = Content.Load<Texture2D>("CatherineEnd");
            janeEnd = Content.Load<Texture2D>("JaneEnding");
            donnaEnd = Content.Load<Texture2D>("DonnaEnding");
            gameOver = Content.Load<Texture2D>("GameOver");


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            previousGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);

            #region Inventory updates
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouse = new Rectangle((int)(mouseState.X + cameraPos.X), (int)(mouseState.Y + cameraPos.Y), 1, 1);

            buttonRectangle = new Rectangle((int)(inventory.Position.X + cameraPos.X), (int)(inventory.Position.Y), 50, 50);
            downArrow = new Rectangle((int)(inventory.Position.X + cameraPos.X), (int)(inventory.Position.Y + cameraPos.Y) + inventory.Texture.Height - 40, 50, 50);
            upArrow = new Rectangle((int)(inventory.Position.X + cameraPos.X), (int)(inventory.Position.Y + cameraPos.Y) + buttonRectangle.Height, 50, 50);

            if (inventory.Show == true)
            {
                keyTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            if (mouse.Intersects(buttonRectangle) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                inventory.Show = true;

            }
            if (inventory.Position.X >= 1300 && inventory.Show == true)
            {
                inventory.Move();
                currentState = GameState.Inventory;
            }
            if (inventory.Position.X >= 1300 && inventory.Show == false && currentState == GameState.Inventory)
            {
                inventory.Close();
                if (inventory.Position.X >= 1550)
                    currentState = GameState.InGame;
            }

            if (mouse.Intersects(buttonRectangle) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && keyTimer >= 600)
            {
                inventory.Show = false;

                keyTimer = 0;

            }

            if (mouse.Intersects(downArrow) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                inventory.Down();
            }
            if (mouse.Intersects(upArrow) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                inventory.Up();
            }
            #endregion
            #region Item updates
            foreach (Item i in items)
            {
                i.TextureCenter = new Vector2(i.Texture.Width / 2, i.Texture.Height / 2);
            }
            for (int i = 0; i < items.Count(); i++)
            {
                if (i <= 7)
                {
                    items[i].Page = 1;
                }
                if (i > 7)
                {
                    items[i].Page = 2;
                }
            }
            for (int i = 0; i < items.Count(); i++)
            {
                if (i <= inventory.ItemsPerPage)
                {
                    if (i % 2 == 0)
                    {
                        items[i].Position = new Vector2(inventory.Position.X + 88, inventory.Position.Y + 40);
                    }
                    if (i % 2 != 0)
                    {
                        items[i].Position = new Vector2(inventory.Position.X + 212, inventory.Position.Y + 40);
                    }
                    if (i > 1)
                    {
                        items[i].Position = new Vector2(items[i].Position.X, inventory.Position.Y + 162);
                    }
                    if (i > 3)
                    {
                        items[i].Position = new Vector2(items[i].Position.X, inventory.Position.Y + 285);
                    }
                    if (i > 5)
                    {
                        items[i].Position = new Vector2(items[i].Position.X, inventory.Position.Y + 407);
                    }
                }
                if (i > inventory.ItemsPerPage)
                {
                    if (i % 2 == 0)
                    {
                        items[i].Position = new Vector2(inventory.Position.X + 88, inventory.Position.Y + 40);
                    }
                    if (i % 2 != 0)
                    {
                        items[i].Position = new Vector2(inventory.Position.X + 212, inventory.Position.Y + 40);
                    }
                    if (i > 9)
                    {
                        items[i].Position = new Vector2(items[i].Position.X, inventory.Position.Y + 162);
                    }
                    if (i > 11)
                    {
                        items[i].Position = new Vector2(items[i].Position.X, inventory.Position.Y + 285);
                    }
                    if (i > 13)
                    {
                        items[i].Position = new Vector2(items[i].Position.X, inventory.Position.Y + 407);
                    }
                }

            }

            for (int i = 0; i < items.Count(); i++)
            {
                rectanglePositions.Add(new Rectangle((int)(items[i].Position.X + cameraPos.X), (int)(items[i].Position.Y + cameraPos.Y), 50, 50));
            }
            

            for (int i = 0; i < items.Count(); i++)
            {
                if (items[i].PickUp == true && inventorySelectionRectangle.Intersects(rectanglePositions[i]) && keyboardState.IsKeyDown(Keys.Space) && prevMouseState.LeftButton == ButtonState.Released && currentState == GameState.Inventory || items[i].PickUp == true && inventorySelectionRectangle.Intersects(rectanglePositions[i]) && gamePadState.IsButtonDown(Buttons.A) && currentState == GameState.Inventory)
                {
                    items[i].Clicked = true;
                    itemClicked = true;
                }

                //if (keyboardState.IsKeyDown(Keys.Space) && inventorySelectionRectangle.Intersects(rectanglePositions[i]) && items[i].PickUp == true)
                //{
                //    items[i].Clicked = true;
                //}

                if (keyboardState.IsKeyDown(Keys.Escape) || keyboardState.IsKeyDown(Keys.I) || gamePadState.IsButtonDown(Buttons.B) || gamePadState.IsButtonDown(Buttons.Back) || gamePadState.IsButtonDown(Buttons.Y))
                {
                    items[i].Clicked = false;
                    itemClicked = false;
                }
            }
            #endregion

            if (keyboardState.IsKeyDown(Keys.P) || gamePadState.IsButtonDown(Buttons.Start))
                currentState = GameState.Pause;

            switch (currentState)
            {
                case GameState.MainMenu:
                    if (currentState == GameState.MainMenu)
                    {
                        
                    }
                    if (int.Parse(File.ReadAllText(Content.RootDirectory + "//Menu.txt")) == 0)
                    {
                        if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState.IsKeyUp(Keys.S) && menuSelection < 2 || gamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released && menuSelection < 2 || gamePadState.ThumbSticks.Left.Y < -0.1f && previousGamePadState.ThumbSticks.Left.Y > -0.1f && menuSelection < 3)
                            menuSelection++;

                        if (keyboardState.IsKeyDown(Keys.W) && prevKeyboardState.IsKeyUp(Keys.W) && menuSelection > 1 || gamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released && menuSelection > 1 || gamePadState.ThumbSticks.Left.Y > 0.1f && previousGamePadState.ThumbSticks.Left.Y < 0.1f && menuSelection > 1)
                            menuSelection--;

                        if (keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                        {
                            if (menuSelection == 1)
                            {
                               
                                File.WriteAllLines(Content.RootDirectory + "//SaveItems", new string[1]); 
                                File.WriteAllText(Content.RootDirectory + "//SaveRoom.txt", "0");
                                File.WriteAllText(Content.RootDirectory + "//Menu.txt", "0");
                                File.WriteAllLines(Content.RootDirectory + "//SaveConversation.txt", new string[8] { "1", "0", "0", "0", "0", "0", "1", "0" });
                                File.WriteAllLines(Content.RootDirectory + "//SaveCall.txt", new string[2] { "false", "false" });
                                this.Initialize();
                                items = new List<Item>();
                                inventory = new Inventory(new List<Item>(), Content.Load<Texture2D>("Inventory/Inventory"), Content.Load<Texture2D>("Inventory/Inventory2"), new Vector2(1550, 0), rectanglePositions, false, 8, buttonRectangle, downArrow, upArrow, 7, 1);
                                currentRoom = 0;
                                player = new Player(new Vector2(2700, 520), playerAnimations, rooms[currentRoom]);
                                transparentBool = true; 
                                newGame = true;
                                
                                currentState = GameState.InGame;
                                
                            }

                            if (menuSelection == 2)
                            {
                                Exit();
                            }
                        }
                    }

                    if (int.Parse(File.ReadAllText(Content.RootDirectory + "//Menu.txt")) != 0)
                    {
                        if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState.IsKeyUp(Keys.S) && menuSelection < 3 || gamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released && menuSelection < 3 || gamePadState.ThumbSticks.Left.Y < -0.1f && previousGamePadState.ThumbSticks.Left.Y > -0.1f && menuSelection < 3)
                            menuSelection++;

                        if (keyboardState.IsKeyDown(Keys.W) && prevKeyboardState.IsKeyUp(Keys.W) && menuSelection > 1 || gamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released && menuSelection > 1 || gamePadState.ThumbSticks.Left.Y > 0.1f && previousGamePadState.ThumbSticks.Left.Y < 0.1f && menuSelection > 1)
                            menuSelection--;

                        if (keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                        {
                            if (menuSelection == 1)
                            {
                                this.Initialize();
                                transparentBool = true;
                                currentState = GameState.InGame;
                                newGame = false;
                               
                            }

                            if (menuSelection == 2)
                            {
                            
                                File.WriteAllLines(Content.RootDirectory + "//SaveItems", new string[1]);
                                File.WriteAllText(Content.RootDirectory + "//SaveRoom.txt", "0");
                                File.WriteAllText(Content.RootDirectory + "//Menu.txt", "0");
                                File.WriteAllLines(Content.RootDirectory + "//SaveConversation.txt", new string[8] { "1", "0", "0", "0", "0", "0", "1", "0" });
                                File.WriteAllLines(Content.RootDirectory + "//SaveCall.txt", new string[2] { "false", "false" });
                                this.Initialize();
                                items = new List<Item>();
                                inventory = new Inventory(items, Content.Load<Texture2D>("Inventory/Inventory"), Content.Load<Texture2D>("Inventory/Inventory2"), new Vector2(1550, 0), rectanglePositions, false, 8, buttonRectangle, downArrow, upArrow, 7, 1);
                                currentRoom = 0;
                                player = new Player(new Vector2(2700, 520), playerAnimations, rooms[currentRoom]);
                                transparentBool = true;
                               
                                currentState = GameState.InGame;
                                newGame = true;
                               
                            }

                            if (menuSelection == 3)
                            {
                                Exit();
                            }
                        }
                    }
                    break;

                #region inGame
                case GameState.InGame:

               
                

                     #region Sound
                 
                   
#region Volume
                    foreach (SoundEffectInstance s in soundEffects)
                    {
                        s.Volume = volume;

                       
            
                    }
                    soundEffects[8].Volume = volume / 4;
                    soundEffects[0].Volume = volume / 4;
                    soundEffects[1].Volume = volume / 4;
                    soundEffects[2].Volume  = volume / 4;
#endregion
                    if (newGame == true) { soundEffects[13].Play(); soundEffects[14].Play(); newGame = false; }

                    if (currentRoom == 6 && soundEffects[14].State == SoundState.Stopped && soundEffects[16].State == SoundState.Stopped && soundEffects[15].State == SoundState.Stopped) { soundEffects[17].Play(); soundEffects[11].Play(); }
                    else { soundEffects[17].Stop(); }
                    if (rooms[currentRoom].Outside == true)
                    {
                        soundEffects[11].Play();
                        if (soundEffects[14].State == SoundState.Stopped && soundEffects[16].State == SoundState.Stopped && soundEffects[15].State == SoundState.Stopped) { soundEffects[8].Play(); }
                        else { soundEffects[8].Pause(); }
                    }
                    else
                    {
                        soundEffects[11].Pause();
                    }
                 
                    if (player.moving)
                    {
                        if (rooms[currentRoom].WalkSound == 1)
                        {
                            soundEffects[5].Play();
                        }
                        if (rooms[currentRoom].WalkSound == 2)
                        {
                            soundEffects[7].Play();
                        }
                        if (rooms[currentRoom].WalkSound == 3)
                        {
                            soundEffects[6].Play();
                        }
                    }
                    else
                    {
                        soundEffects[5].Stop();
                        soundEffects[6].Stop();
                        soundEffects[7].Stop();
                    }
                    if (changeRoom)
                    {
                        if (rooms[currentRoom].Doors[doorInt].DoorSound == 1)
                        {
                            soundEffects[0].Play();
                        }
                        if (rooms[currentRoom].Doors[doorInt].DoorSound == 2)
                        {
                            soundEffects[1].Play();
                        }
                        if (rooms[currentRoom].Doors[doorInt].DoorSound == 3)
                        {
                            soundEffects[2].Play();
                        }
                       
                        
                    }
                    

#endregion
#region Events
                    
                //OPENS UP ABILITY TO TALK TO AN ABOUT WHO YOU THOUGHT IT WERE AFTER YOU TALKED TO EASON
                    if (CityHallCharacters[0].ConversationIndexNumber > 1 && easonTalked == false) { TownSquareCharacters[0] = new Character(File.ReadAllLines(Content.RootDirectory + "//Characters/Conversations/End.txt"), false, Content.Load<SpriteFont>("font"), Vector2.Zero, false, false, false, 5, 1, 0, new Rectangle(4600, 400, 200, 300), new Vector2(4600, 400), new Animation(Content.Load<Texture2D>("Characters/AnAnimation"), 2, 1, 300, 1f), false, 1); easonTalked = true; }
                   
                //GAME OVER IN PHONE SCENE
                    if (rangerPhone.ConversationIndexNumber == 4) { currentState = GameState.End; }
                    
                //GIVES MORE OPPERTUNITIES IF YOU HAVE POEM
                    foreach (Item i in inventory.Items)
                    {
                        if (i.Name==@"A poem signed by August Fresh, 
but it is clearly a Roderick Atkins lyric.")
                        {
                            poem = true;
                            
                        }
                    }
                    if (poem == true)
                    {
                        GasStationCharacters[0].ConversationIndexNumber = 5;
                        GasStationCharacters[0].ConversationIndexNumber2 = 1;
                        GasStationCharacters[0].LastKey = 1;
                    }
                    //EASON CALLS
                    easonPhone.TextPosition = new Vector2(dialogue.Position.X + 40, dialogue.Position.Y + 50);
                    if ((((MansionCharacters[0].ConversationIndexNumber > 1 && GasStationCharacters[0].ConversationIndexNumber > 1) && ((!(player.TalkRectangle.Intersects(GasStationCharacters[0].TalkRectangle))) || (!(player.TalkRectangle.Intersects(MansionCharacters[0].TalkRectangle))))) && easonPhone.ConversationIndexNumber == 1) && easonCalled == false)
                    {
                        callTimer += gameTime.ElapsedGameTime.Milliseconds;
                        soundEffects[18].Play();
                        if (callTimer > 5000)
                        {
                            soundEffects[18].Stop();
                            dialogue.Show = true;
                            easonPhone.MakeChars();
                            easonPhone.PrintText = true;
                            conversationStarted = true;

                            currentState = GameState.Conversation;
                        }
                        
                        if (dialogue.Show == false && conversationStarted == true)
                        {
                            easonPhone.Reset();
                            if (easonPhone.PrintText == true)
                            {
                                easonPhone.IsReset = true;
                                easonPhone.PrintText = false;
                            }
                        }
                    }

                    if (easonPhone.ConversationIndexNumber > 1) 
                    { 
                        soundEffects[16].Play();
                        easonPhone.ConversationIndexNumber = 0; 
                        CityHallCharacters[0].ConversationIndexNumber = 1;
                        if (soundEffects[17].State == SoundState.Playing) { soundEffects[17].Stop(); }
                    }

                    //IF YOU HAVE TALKED TO AN AND SWITCH ROOMS PHONE.TXT PLAYS
                    if (currentRoom == 13 && soundEffects[18].State != SoundState.Playing) { changed = false; }
                    if (changeRoom && changed == false && soundEffects[18].State != SoundState.Playing) { changed = true; }
                    
                     rangerPhone.TextPosition = new Vector2(dialogue.Position.X + 40, dialogue.Position.Y + 50);
                    if (((TownSquareCharacters[0].ConversationIndexNumber > 1 && changed == true) && rangerPhone.ConversationIndexNumber == 1) && rangerCalled == false)
                    {
                        callTimer += gameTime.ElapsedGameTime.Milliseconds;
                        soundEffects[18].Play();
                        if (callTimer > 5000)
                        {
                            changed = false;
                            soundEffects[18].Stop();
                            dialogue.Show = true;
                            rangerPhone.MakeChars();
                            rangerPhone.PrintText = true;
                            conversationStarted = true;

                            currentState = GameState.Conversation;
                        }
                        
                        if (dialogue.Show == false && conversationStarted == true)
                        {
                            rangerPhone.Reset();
                            if (rangerPhone.PrintText == true)
                            {
                                rangerPhone.IsReset = true;
                                rangerPhone.PrintText = false;
                            }
                        }
                    }

                    if (rangerPhone.ConversationIndexNumber > 1 && rangerPhone.ConversationIndexNumber != 4) { rangerPhone.ConversationIndexNumber = 0; }

                    //TALKING TO AN UNLOCKS ALL THE CHARACTERS EXCEPT EASON WHO UNLOCKS BY HIS PHONECALL
                    if (TownSquareCharacters[0].ConversationIndexNumber > 1 && anTalked == false && rangerCalled == true)
                    {
                        foreach (Item i in inventory.Items)
                        {
                            if (i.Name == @"A poem signed by August Fresh, 
but it is clearly a Roderick Atkins lyric.")
                            {
                                poem = true;

                            }
                        }
                        if (poem)
                        {
                            GasStationCharacters[0].ConversationIndexNumber = 5;
                            GasStationCharacters[0].ConversationIndexNumber2 = 1;
                            GasStationCharacters[0].LastKey = 1;
                        }
                        if(!poem)
                        {
                            GasStationCharacters[0].ConversationIndexNumber = 1;
                        }
                        MansionCharacters[0].ConversationIndexNumber = 1;
                        
                        FarmCharacters[0].ConversationIndexNumber = 1;
                        HotelCharacters[0].ConversationIndexNumber = 1;
                        StoreCharacters[0].ConversationIndexNumber = 1;
                        anTalked = true;
                        soundEffects[15].Play();
                    }

                    //ENDINGS
                    if (TownSquareCharacters[0].ConversationIndexNumber == 51 || TownSquareCharacters[0].ConversationIndexNumber == 52 || TownSquareCharacters[0].ConversationIndexNumber == 53 || TownSquareCharacters[0].ConversationIndexNumber == 54 || TownSquareCharacters[0].ConversationIndexNumber == 55) { currentState = GameState.End; }


#endregion
                    for (int i = 0; i < rooms[currentRoom].Characters.Count; i++)
                    {
                        if (player.TalkRectangle.Intersects(rooms[currentRoom].Characters[i].TalkRectangle) && keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space) && rooms[currentRoom].Characters[i].ConversationIndexNumber != 0)
                        {
                            
                            dialogue.Show = true;

                            rooms[currentRoom].Characters[i].MakeChars();
                            rooms[currentRoom].Characters[i].PrintText = true;

                            conversationStarted = true;
                            currentState = GameState.Conversation;

                            if (dialogue.Show == false && conversationStarted == true)
                            {
                                rooms[currentRoom].Characters[i].Reset();
                                if (rooms[currentRoom].Characters[i].PrintText == true)
                                {
                                    rooms[currentRoom].Characters[i].IsReset = true;
                                    rooms[currentRoom].Characters[i].PrintText = false;
                                }
                            }

                            rooms[currentRoom].Characters[i].TextPosition = new Vector2((dialogue.Position.X) + 40, (dialogue.Position.Y) + 50);
                        }
                    }
                    

                    player.Update(keyboardState, gamePadState,Vector2.Zero, gameTime);

                    rooms[currentRoom].Update(gameTime);

                    if (keyboardState.IsKeyDown(Keys.I) && previousKS.IsKeyUp(Keys.I) && !inventory.Show && inventory.Position.X >= 1500 || gamePadState.IsButtonDown(Buttons.Y) && previousGamePadState.IsButtonUp(Buttons.Y) && !inventory.Show && inventory.Position.X >= 1500)
                    {
                        inventorySelection = Vector2.Zero;
                        inventory.Show = true;
                    }

                    for (int i = 0; i < rooms[currentRoom].Items.Count; i++)
                    {
                        if (Math.Abs(player.TalkRectangle.Center.X - rooms[currentRoom].Items[i].Position.X) < 100 && Math.Abs(player.TalkRectangle.Center.Y - rooms[currentRoom].Items[i].Position.Y) < 300 && keyboardState.IsKeyDown(Keys.Space) || Math.Abs(player.TalkRectangle.Center.X - rooms[currentRoom].Items[i].Position.X) < 100 && Math.Abs(player.TalkRectangle.Center.Y - rooms[currentRoom].Items[i].Position.Y) < 100 && gamePadState.IsButtonDown(Buttons.A))
                        {
                            items.Add(rooms[currentRoom].Items[i]);
                            soundEffects[10].Play();
                            rooms[currentRoom].Items[i].PickUp = true;
                            inventory.Items.Add(rooms[currentRoom].Items[i]);
                            //inventory = new Inventory(items, Content.Load<Texture2D>("Inventory/InventoryTEST"), Content.Load<Texture2D>("Inventory/InventoryTEST2"), new Vector2(1550, 0), rectanglePositions, false, 8, buttonRectangle, downArrow, upArrow, 7, 1);
                            rooms[currentRoom].Items.RemoveAt(i);
                            break;
                        }
                    }

                    for (int i = 0; i < rooms[currentRoom].Doors.Count; i++)
                    {

                        if (player.DoorRectangle.Intersects(rooms[currentRoom].Doors[i].DoorRect) && keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space) || player.DoorRectangle.Intersects(rooms[currentRoom].Doors[i].DoorRect) && gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                        {
                            changeRoom = true;
                            currentDoor = rooms[currentRoom].Doors[i].ConnectedDoor;
                            newRoom = rooms[currentRoom].Doors[i].ConnectedRoom;
                            doorInt = i;
                        }

                        if (changeRoom)
                        {
                            transparent += (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.005f;
                            player.inputAllowed = false;
                        }

                        if (transparent >= 1 && changeRoom)
                        {
                            //currentDoor = rooms[currentRoom].Doors[i].ConnectedDoor;
                            currentRoom = newRoom;
                            //player = new Player(new Vector2(rooms[currentRoom].Doors[currentDoor].DoorRect.X - 50, rooms[currentRoom].Doors[currentDoor].DoorRect.Y + rooms[currentRoom].Doors[currentDoor].DoorRect.Height - 180), playerAnimations, rooms[currentRoom]);

                            player = new Player(new Vector2(rooms[currentRoom].Doors[currentDoor].DoorRect.X - 50, rooms[currentRoom].Doors[currentDoor].DoorRect.Y + (rooms[currentRoom].Doors[currentDoor].DoorRect.Height / 2)), playerAnimations, rooms[currentRoom]);
                           

                            if (rooms[currentRoom].Doors[currentDoor].DoorRect.X > GraphicsDevice.Viewport.Width / 2)
                                cameraPos = new Vector2(-rooms[currentRoom].Doors[currentDoor].DoorRect.X + GraphicsDevice.Viewport.Width / 2, 0);

                            if (rooms[currentRoom].Doors[currentDoor].DoorRect.X < GraphicsDevice.Viewport.Width / 2)
                                cameraPos = new Vector2(0, 0);

                            if (rooms[currentRoom].Doors[currentDoor].DoorRect.X > rooms[currentRoom].roomWidth - GraphicsDevice.Viewport.Width / 2)
                                cameraPos = new Vector2(-rooms[currentRoom].roomWidth + GraphicsDevice.Viewport.Width, 0);

                            changeRoom = false;
                            transparentBool = true;
                        }

                        if (showRoomName)
                        {
                            showRoomNameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }

                        if (showRoomNameTimer > 2)
                        {
                            showRoomName = false;
                            showRoomNameTimer = 0;
                        }

                        if (transparentBool)
                        {
                            transparent -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.005f;

                            if (transparent <= 0)
                            {
                                showRoomName = true;
                                player.inputAllowed = true;
                                transparent = 0;
                                transparentBool = false;
                            }
                        }
                    }

                    if (showRoomName && showRoomNameTimer < 0.5f)
                    {
                        textFadeTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }

                    if (showRoomName && showRoomNameTimer > 1.5f)
                    {
                        textFadeTimer -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }

                    if (player.Position.X > GraphicsDevice.Viewport.Width / 2 && player.Position.X < rooms[currentRoom].roomWidth - GraphicsDevice.Viewport.Width / 2)
                        cameraPos = new Vector2(-player.Position.X + GraphicsDevice.Viewport.Width / 2, 0);

                    //if (player.Position.X <= GraphicsDevice.Viewport.Width / 2)
                    //    cameraPos = new Vector2(0, 0);

                    //if (player.Position.X >= rooms[currentRoom].roomWidth - GraphicsDevice.Viewport.Width / 2)
                    //    cameraPos = new Vector2(rooms[currentRoom].roomWidth - GraphicsDevice.Viewport.Width / 2, 0);

                    camera = Matrix.CreateScale(1) * Matrix.CreateTranslation(cameraPos.X, cameraPos.Y, 0);

                    #region items


                    #endregion

                    break;
                #endregion

                #region inventory
                case GameState.Inventory:
                    if (keyboardState.IsKeyDown(Keys.P))
                        currentState = GameState.Pause;

                    for (int i = 0; i < items.Count(); i++)
                    {
                        rectanglePositions[i] = new Rectangle((int)(items[i].Position.X), (int)(items[i].Position.Y + cameraPos.Y), 50, 50);
                    }

                    if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyUp(Keys.Space) && !itemClicked || gamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released && gamePadState.IsButtonUp(Buttons.A) && !itemClicked || gamePadState.ThumbSticks.Left.Y < -0.1f && previousGamePadState.ThumbSticks.Left.Y > -0.1f && gamePadState.IsButtonUp(Buttons.A) && !itemClicked)
                    {
                        if (inventorySelection.Y < 4)
                            inventorySelection = new Vector2(inventorySelection.X, inventorySelection.Y + 1);

                        if (inventorySelection.Y >= 4)
                        {
                            inventory.Down();
                            inventorySelection = new Vector2(inventorySelection.X, 0);
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.W) && prevKeyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyUp(Keys.Space) && !itemClicked || gamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released && gamePadState.IsButtonUp(Buttons.A) && !itemClicked || gamePadState.ThumbSticks.Left.Y > 0.1f && previousGamePadState.ThumbSticks.Left.Y < 0.1f && gamePadState.IsButtonUp(Buttons.A) && !itemClicked)
                    {
                        if (inventory.Page == 1 && inventorySelection.Y > 0 || inventory.Page != 1)
                            inventorySelection = new Vector2(inventorySelection.X, inventorySelection.Y - 1);

                        if (inventorySelection.Y == -1 && inventory.Page != 1)
                        {
                            inventory.Up();
                            inventorySelection = new Vector2(inventorySelection.X, 3);
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.D) && prevKeyboardState.IsKeyUp(Keys.D) && inventorySelection.X < 1 && keyboardState.IsKeyUp(Keys.Space) && !itemClicked || gamePadState.DPad.Right == ButtonState.Pressed && previousGamePadState.DPad.Right == ButtonState.Released && inventorySelection.X < 1 && gamePadState.IsButtonUp(Buttons.A) && !itemClicked || gamePadState.ThumbSticks.Left.X > 0.1f && previousGamePadState.ThumbSticks.Left.X < 0.1f && inventorySelection.X < 1 && gamePadState.IsButtonUp(Buttons.A) && !itemClicked)
                        inventorySelection = new Vector2(inventorySelection.X + 1, inventorySelection.Y);

                    if (keyboardState.IsKeyDown(Keys.A) && prevKeyboardState.IsKeyUp(Keys.A) && inventorySelection.X > 0 && keyboardState.IsKeyUp(Keys.Space) && !itemClicked || gamePadState.DPad.Left == ButtonState.Pressed && previousGamePadState.DPad.Left == ButtonState.Released && inventorySelection.X > 0 && gamePadState.IsButtonUp(Buttons.A) && !itemClicked || gamePadState.ThumbSticks.Left.X < -0.1f && previousGamePadState.ThumbSticks.Left.X > -0.1f && inventorySelection.X > 0 && gamePadState.IsButtonUp(Buttons.A) && !itemClicked)
                        inventorySelection = new Vector2(inventorySelection.X - 1, inventorySelection.Y);


                    inventorySelectionRectangle = new Rectangle((int)(inventorySelection.X * 200), (int)(inventorySelection.Y * 200), 200, 200);

                    if (keyboardState.IsKeyDown(Keys.I) && previousKS.IsKeyUp(Keys.I) && inventory.Show && keyTimer >= 600 && inventory.Position.X >= 1200 || gamePadState.IsButtonDown(Buttons.Y) && previousGamePadState.IsButtonUp(Buttons.Y) && inventory.Show && keyTimer >= 600 && inventory.Position.X >= 1200 || gamePadState.IsButtonDown(Buttons.Y) && previousGamePadState.IsButtonUp(Buttons.Y) && inventory.Show && keyTimer >= 600 && inventory.Position.X >= 1200)
                    {
                        inventory.Show = false;
                        keyTimer = 0;
                    }

                    inventorySelectionRectangle = new Rectangle((int)((GraphicsDevice.Viewport.Width - 242) + inventorySelection.X * 120), (int)(inventorySelection.Y * 122 + 12), 110, 110);

                    break;
                #endregion

                #region pause
                case GameState.Pause:
                    
                    foreach (SoundEffectInstance s in soundEffects)
                    {
                        if (s.State == SoundState.Playing)
                        {
                            s.Pause();
                        }
                    }
                    if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState.IsKeyUp(Keys.S) && pauseSelection < 4 || gamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released && pauseSelection < 4 || gamePadState.ThumbSticks.Left.Y < -0.1f && previousGamePadState.ThumbSticks.Left.Y > -0.1f && pauseSelection < 4)
                        pauseSelection++;

                    if (keyboardState.IsKeyDown(Keys.W) && prevKeyboardState.IsKeyUp(Keys.W) && pauseSelection > 1 || gamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released && pauseSelection > 1 || gamePadState.ThumbSticks.Left.Y > 0.1f && previousGamePadState.ThumbSticks.Left.Y < 0.1f && pauseSelection > 1)
                            pauseSelection--;

                    if (keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        if (pauseSelection == 1)
                        {
                            foreach (SoundEffectInstance s in soundEffects)
                            {
                                if (s.State == SoundState.Paused)
                                {
                                    s.Resume();
                                }
                            }
                            currentState = GameState.InGame;

                        }

                        if (pauseSelection == 2)
                        {
                            string[] itemIndexes = new string[items.Count];
                            for (int i = 0; i < items.Count; i++)
			                {
                                itemIndexes[i] = items[i].ItemIndex.ToString();
			                }
                            string[] pPos = new string[2];
                            pPos[0] = player.Position.X.ToString();
                            pPos[1] = player.Position.Y.ToString();
                            savedConversationStates[0] = BobbyRoomCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[1] = GasStationCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[2] = MansionCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[3] = FarmCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[4] = HotelCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[5] = StoreCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[6] = TownSquareCharacters[0].ConversationIndexNumber.ToString();
                            savedConversationStates[7] = CityHallCharacters[0].ConversationIndexNumber.ToString();
                            saveCalls[0] = easonCalled.ToString();
                            saveCalls[1] = rangerCalled.ToString();
                            File.WriteAllText(Content.RootDirectory + "//Options.txt", volume.ToString());
                            File.WriteAllLines(Content.RootDirectory + "//SavePosition.txt", pPos);
                            File.WriteAllLines(Content.RootDirectory + "//SaveItems.txt", itemIndexes);
                            File.WriteAllText(Content.RootDirectory + "//SaveRoom.txt", currentRoom.ToString());
                            File.WriteAllText(Content.RootDirectory + "//Menu.txt", "1");
                            File.WriteAllLines(Content.RootDirectory + "//SaveConversation.txt", savedConversationStates);
                            File.WriteAllLines(Content.RootDirectory + "//SaveCall.txt", saveCalls);
                          
                        }

                        if (pauseSelection == 3)
                            currentState = GameState.Options;

                        if (pauseSelection == 4)
                            currentState = GameState.MainMenu;
                    }

                    break;
                #endregion

                case GameState.Conversation:
                    soundEffects[5].Stop();
                    soundEffects[6].Stop();
                    soundEffects[7].Stop();
                    foreach (Character c in rooms[currentRoom].Characters)
                    {
                        if (c.Typing ||easonPhone.Typing || rangerPhone.Typing == true) { soundEffects[12].Play(); }
                        else { soundEffects[12].Pause(); }
                    }
                 
                    for (int i = 0; i < rooms[currentRoom].Characters.Count; i++)
                    {
                        if (player.TalkRectangle.Intersects(rooms[currentRoom].Characters[i].TalkRectangle))
                        {
                            if (rooms[currentRoom].Characters[i].Close)
                            {
                                dialogue.Show = false;
                                currentState = GameState.InGame;
                            }
                        }
                    }
                    if (easonPhone.Close && easonCalled == false) { dialogue.Show = false; currentState = GameState.InGame; easonCalled = true; callTimer = 0; }
                    if (rangerPhone.Close && rangerCalled == false) { dialogue.Show = false; currentState = GameState.InGame; rangerCalled = true; callTimer = 0; }
                    break;

                case GameState.Options:
                    if (keyboardState.IsKeyDown(Keys.S) && prevKeyboardState.IsKeyUp(Keys.S) && optionsSelection < 2 || gamePadState.DPad.Down == ButtonState.Pressed && previousGamePadState.DPad.Down == ButtonState.Released && optionsSelection < 2 || gamePadState.ThumbSticks.Left.Y < -0.1f && previousGamePadState.ThumbSticks.Left.Y > -0.1f && optionsSelection < 2)
                        optionsSelection++;

                    if (keyboardState.IsKeyDown(Keys.W) && prevKeyboardState.IsKeyUp(Keys.W) && optionsSelection > 1 || gamePadState.DPad.Up == ButtonState.Pressed && previousGamePadState.DPad.Up == ButtonState.Released && optionsSelection > 1 || gamePadState.ThumbSticks.Left.Y > 0.1f && previousGamePadState.ThumbSticks.Left.Y < 0.1f && optionsSelection > 1)
                        optionsSelection--;

                    if (keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space) && optionsSelection == 2 || keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter) && optionsSelection == 2 || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && optionsSelection == 2)
                    {
                        optionsSelection = 1;
                        currentState = GameState.Pause;
                    }

                    if (optionsSelection == 1)
                    {
                        if (keyboardState.IsKeyDown(Keys.A) && prevKeyboardState.IsKeyUp(Keys.A) && volume > 0 || gamePadState.DPad.Left == ButtonState.Pressed && previousGamePadState.DPad.Left == ButtonState.Released && volume > 0 || gamePadState.ThumbSticks.Left.X < -0.1f && previousGamePadState.ThumbSticks.Left.X > -0.1f && volume > 0)
                        {
                            volume -= 0.1f;
                        }

                        if (keyboardState.IsKeyDown(Keys.D) && prevKeyboardState.IsKeyUp(Keys.D) && volume < 1 || gamePadState.DPad.Right == ButtonState.Pressed && previousGamePadState.DPad.Right == ButtonState.Released && volume < 1 || gamePadState.ThumbSticks.Left.X < 0.1f && previousGamePadState.ThumbSticks.Left.X > 0.1f && volume < 1)
                        {
                            volume += 0.1f;
                        }
                    }

                    break;

                case GameState.End:
                    foreach (SoundEffectInstance s in soundEffects)
                    {
                        s.Stop();
                    }
                    soundEffects[16].Play();
                    if ((keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space)) || (gamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released)) { this.Exit(); }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera);

            rooms[currentRoom].Draw(spriteBatch);

            //for (int i = 0; i < rooms[currentRoom].Doors.Count; i++)
            //    spriteBatch.Draw(Content.Load<Texture2D>("rect"), rooms[currentRoom].Doors[i].DoorRect, Color.Magenta);

            //for (int i = 0; i < rooms[currentRoom].Walls.Count; i++)
            //{
            //    spriteBatch.Draw(Content.Load<Texture2D>("rect"), rooms[currentRoom].Walls[i], Color.Green);
            //}

            //for (int i = 0; i < rooms[currentRoom].Walls.Count; i++)
            //{
            //    spriteBatch.Draw(Content.Load<Texture2D>("rect"), rooms[currentRoom].Walls[i], Color.Red);
            //}
            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.TalkRectangle, Color.Black);
            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.DoorRectangle, Color.White);

            for (int i = 0; i < rooms[currentRoom].Characters.Count; i++)
            {
                rooms[currentRoom].Characters[i].Print(spriteBatch, gameTime, keyboardState, prevKeyboardState);
                //spriteBatch.Draw(Content.Load<Texture2D>("rect"), rooms[currentRoom].Characters[i].TalkRectangle, Color.Red);
            }
          
            player.Draw(spriteBatch);
            //spriteBatch.DrawString(font, BobbyRoomCharacters[0].ConversationIndexNumber.ToString(), new Vector2(player.Position.X, player.Position.Y), Color.Red);
            //spriteBatch.DrawString(font, allCharacters[0].ConversationIndexNumber.ToString(), new Vector2(player.Position.X, player.Position.Y + 50), Color.Red);
            //spriteBatch.DrawString(font, TownSquareCharacters[0].ConversationIndexNumber.ToString(), new Vector2(player.Position.X - 100, player.Position.Y), Color.Red);
            //spriteBatch.DrawString(font, allCharacters[1].ConversationIndexNumber.ToString(), new Vector2(player.Position.X - 100, player.Position.Y + 50), Color.Red);
            //spriteBatch.DrawString(font, rooms[currentRoom].Outside.ToString(), new Vector2(player.Position.X - 100, player.Position.Y + 50), Color.Red);
            //spriteBatch.DrawString(font, TownSquareCharacters[0].ConversationIndexNumber.ToString(), new Vector2(player.Position.X - 100, player.Position.Y + 50), Color.Red);
            //spriteBatch.DrawString(font, rangerPhone.Typing.ToString(), new Vector2(player.Position.X - 100, player.Position.Y + 50), Color.Red);
            //spriteBatch.DrawString(font, player.Position.X.ToString(), new Vector2(player.Position.X - 100, player.Position.Y + 50), Color.Red);
            //spriteBatch.DrawString(font, GasStationCharacters[0].ConversationIndexNumber.ToString(), new Vector2(player.Position.X - 100, player.Position.Y + 50), Color.Red);
            dialogue.print(spriteBatch);
            if (easonPhone.PrintText)
            {
                easonPhone.Print(spriteBatch, gameTime, keyboardState, prevKeyboardState);
            }
            if (rangerPhone.PrintText)
            {
                rangerPhone.Print(spriteBatch, gameTime, keyboardState, prevKeyboardState);
            }
            dialogue.Position = new Vector2(-cameraPos.X + 350, cameraPos.Y + 500);

            for (int i = 0; i < rooms[currentRoom].Characters.Count; i++)
            {
                rooms[currentRoom].Characters[i].PrintConv(spriteBatch, gameTime, keyboardState, prevKeyboardState);
                //spriteBatch.Draw(Content.Load<Texture2D>("rect"), rooms[currentRoom].Characters[i].TalkRectangle, Color.Red);
            }

            
            spriteBatch.Draw(Content.Load<Texture2D>("rect"), new Rectangle(0, 0, 50000, GraphicsDevice.Viewport.Height), Color.Black * transparent);

            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.PlayerRectangle, Color.Magenta);
            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.rectBot, Color.Green);
            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.rectTop, Color.Green);
            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.rectL, Color.Green);
            //spriteBatch.Draw(Content.Load<Texture2D>("rect"), player.rectR, Color.Green);

            spriteBatch.End();

            spriteBatch.Begin();
            if (showRoomName)
            {
                spriteBatch.DrawString(font, "" + rooms[currentRoom].Name, new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(rooms[currentRoom].Name).X / 2 + 2, GraphicsDevice.Viewport.Height / 2 - font.MeasureString(rooms[currentRoom].Name).Y / 2 + 2 - 150), Color.Black * ((textFadeTimer * 2) / 1000));
                spriteBatch.DrawString(font, "" + rooms[currentRoom].Name, new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(rooms[currentRoom].Name).X / 2, GraphicsDevice.Viewport.Height / 2 - font.MeasureString(rooms[currentRoom].Name).Y / 2 - 150), Color.White * ((textFadeTimer * 2) / 1000));
                spriteBatch.Draw(Content.Load<Texture2D>("TextBorder"), new Vector2(GraphicsDevice.Viewport.Width / 2 - Content.Load<Texture2D>("TextBorder").Width / 2, GraphicsDevice.Viewport.Height / 2 - Content.Load<Texture2D>("TextBorder").Height / 2 - 60 - 150), Color.Black * ((textFadeTimer * 2) / 1000));
                spriteBatch.Draw(Content.Load<Texture2D>("TextBorder"), new Vector2(GraphicsDevice.Viewport.Width / 2 - Content.Load<Texture2D>("TextBorder").Width / 2, GraphicsDevice.Viewport.Height / 2 - Content.Load<Texture2D>("TextBorder").Height / 2 + 60 - 150), Color.Black * ((textFadeTimer * 2) / 1000));
            }

            #region inventory
            if (inventory.Position.X <= 1550)
            {
                inventory.Print(spriteBatch);
            }

            foreach (Item i in items)
            {
                if (inventory.Page == 1)
                {
                    for (int j = 0; j < items.Count(); j++)
                    {
                        if (items[j].Page == 1)
                        {
                            items[j].Print(spriteBatch);
                        }
                    }
                }
                if (inventory.Page == 2)
                {
                    for (int k = 0; k < items.Count(); k++)
                    {
                        if (items[k].Page == 2)
                        {
                            items[k].Print(spriteBatch);
                        }
                    }
                }
            }
            #endregion
            
            switch (currentState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("rect"), new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Black);
                    if (int.Parse(File.ReadAllText(Content.RootDirectory + "//Menu.txt")) == 0)
                    {
                        spriteBatch.DrawString(font, "New Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 350), Color.White);
                        spriteBatch.DrawString(font, "Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 450), Color.White);

                        if (menuSelection == 1)
                            spriteBatch.DrawString(font, "New Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 350), Color.Yellow);

                        if (menuSelection == 2)
                            spriteBatch.DrawString(font, "Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 450), Color.Yellow);
                    }

                    if (int.Parse(File.ReadAllText(Content.RootDirectory + "//Menu.txt")) != 0)
                    {
                        spriteBatch.DrawString(font, "Continue", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 320), Color.White);
                        spriteBatch.DrawString(font, "New Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 420), Color.White);
                        spriteBatch.DrawString(font, "Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 520), Color.White);

                        if (menuSelection == 1)
                            spriteBatch.DrawString(font, "Continue", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 320), Color.Yellow);

                        if (menuSelection == 2)
                            spriteBatch.DrawString(font, "New Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 420), Color.Yellow);

                        if (menuSelection == 3)
                            spriteBatch.DrawString(font, "Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 220, 520), Color.Yellow);
                    }

                    break;

                case GameState.Inventory:
                    if (inventory.Position.X <= 1300)
                        spriteBatch.Draw(Content.Load<Texture2D>("inventorySelection"), inventorySelectionRectangle, Color.White);
                    break;

                case GameState.Pause:
                    spriteBatch.Draw(Content.Load<Texture2D>("rect"), new Rectangle((int)cameraPos.X, (int)cameraPos.Y, 10000, GraphicsDevice.Viewport.Height), Color.Black * 0.6f);
                    spriteBatch.DrawString(font, "Resume", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 250), Color.White);
                    spriteBatch.DrawString(font, "Save", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 350), Color.White);
                    spriteBatch.DrawString(font, "Options", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 450), Color.White);
                    spriteBatch.DrawString(font, "Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 550), Color.White);

                    if (pauseSelection == 1)
                        spriteBatch.DrawString(font, "Resume", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 250), Color.Yellow);

                    if (pauseSelection == 2)
                        spriteBatch.DrawString(font, "Save", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 350), Color.Yellow);

                    if (pauseSelection == 3)
                        spriteBatch.DrawString(font, "Options", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 450), Color.Yellow);

                    if (pauseSelection == 4)
                        spriteBatch.DrawString(font, "Exit", new Vector2(GraphicsDevice.Viewport.Width / 2 - 180, 550), Color.Yellow);

                    break;

                case GameState.Options:
                    spriteBatch.Draw(Content.Load<Texture2D>("rect"), new Rectangle((int)cameraPos.X, (int)cameraPos.Y, 10000, GraphicsDevice.Viewport.Height), Color.Black * 0.6f);
                    spriteBatch.Draw(Content.Load<Texture2D>("rect"), new Rectangle((int)(GraphicsDevice.Viewport.Width / 2 + 100), 365, (int)(200 * volume), 25), Color.Red);
                    spriteBatch.DrawString(font, "Volume: ", new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, 350), Color.White);
                    spriteBatch.DrawString(font, "Back", new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, 450), Color.White);

                    if (optionsSelection == 1)
                        spriteBatch.DrawString(font, "Volume: ", new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, 350), Color.Yellow);

                    if (optionsSelection == 2)
                        spriteBatch.DrawString(font, "Back", new Vector2(GraphicsDevice.Viewport.Width / 2 - 300, 450), Color.Yellow);

                    break;

                case GameState.End:
                    if (TownSquareCharacters[0].ConversationIndexNumber == 51) { spriteBatch.Draw(easonEnd, Vector2.Zero, Color.White); }
                    if (TownSquareCharacters[0].ConversationIndexNumber == 52) { spriteBatch.Draw(augustEnd, Vector2.Zero, Color.White); }
                    if (TownSquareCharacters[0].ConversationIndexNumber == 53) { spriteBatch.Draw(catherineEnd, Vector2.Zero, Color.White); }
                    if (TownSquareCharacters[0].ConversationIndexNumber == 54) { spriteBatch.Draw(janeEnd, Vector2.Zero, Color.White); }
                    if (TownSquareCharacters[0].ConversationIndexNumber == 55) { spriteBatch.Draw(donnaEnd, Vector2.Zero, Color.White); }
                    if (rangerPhone.ConversationIndexNumber == 4) { spriteBatch.Draw(gameOver, Vector2.Zero, Color.White); }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
