using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

// Level Reader Class
// Neha Ghanta 
namespace ALightInTheDark
{
    class LevelReader
    {

        // sprite variables
        Texture2D platform;
        Texture2D playerSprite;
        Texture2D closeDoor;
        Texture2D openDoor;
        Texture2D beforeLever;
        Texture2D afterLever;
        Texture2D beforeButton;
        Texture2D afterButton;

        // file path variable
        private string filePath;

        // gameobject list for interactable sprites
        private List<GameObject> interactable;

        // gameobject list for levers and buttons
        private List<GameObject> inputObjects;

        // gameobject for player
        private Player player;

        // gameobject for door
        private GameObject door;

        // gameObject for lever
        private GameObject lever;

        // gameObject for button
        private GameObject button; 

        // property for file path
        public string FilePath
        {
            get
            {
                return filePath;
            }
        }

        // property for player gameobject
        public Player Player
        {
            get
            {
                return player;
            }
        }

        // property for door gameobject
        public GameObject Door
        {
            get
            {
                return door;
            }
        }


        // indexer for gameobjects
        public GameObject this[int index]
        {
            get
            {
                return interactable[index];
            }
        }
        
        public List<GameObject> Interactable
        {
            get
            {
                return interactable;
            }
        }

        public List<GameObject> InputObjects
        {
            get
            {
                return inputObjects;
            }
        }

        // constructor
        public LevelReader(Texture2D plat, Texture2D play, Texture2D openD, Texture2D closeD, Texture2D beforeL, Texture2D afterL, Texture2D beforeB, Texture2D afterB, string path)
        {
            filePath = path;
            interactable = new List<GameObject>();
            inputObjects = new List<GameObject>();
            platform = plat;
            playerSprite = play;
            closeDoor = closeD;
            openDoor = openD;
            beforeLever = beforeL;
            afterLever = afterL;
            beforeButton = beforeB;
            afterButton = afterB;
        }

        // reading file and creating gameobjects based on file
        public void ReadFile()
        {
            // read streams
            FileStream readStream = File.OpenRead(filePath);
            StreamReader reader = new StreamReader(readStream);

            // getting height and width from file
            string heightAsString = reader.ReadLine();
            int height = int.Parse(heightAsString);
            string widthAsString = reader.ReadLine();
            int width = int.Parse(widthAsString);

            // for line skip
            reader.ReadLine();

            // location of sprite placed
            int x = 0;
            int y = 0;
            int read = 0;

            // loop to read characters
            for (int i = 0; i < (width * height); i++)
            {
                char type = (char)reader.Read();
                switch (type)
                {
                    // platforms
                    case '-':
                        {
                            // creates rectangle for location
                            Rectangle location = new Rectangle((x * (platform.Width * 2)), (y * platform.Height * 7), platform.Width * 2, platform.Height * 2);

                            // adds rectangle to location
                            interactable.Add(new GameObject(location, platform));

                            read++;
                            x++;
                            break;
                        }

                    // player
                    case 'p':
                        {
                            // creates rectangles for location
                            Rectangle location = new Rectangle((x * (1000/10)), (y * (725/10)), playerSprite.Width, playerSprite.Height);

                            // making the player object
                            player = new Player(location, playerSprite);

                            read++;
                            x++;
                            break;
                        }

                    // door
                    case 'd':
                        {
                            // rectangle for location
                            Rectangle location = new Rectangle((x * (1000 / 10)), (y * (675 / 10)), closeDoor.Width, closeDoor.Height);

                            // making door object and adding it to array
                            door = new GameObject(location, closeDoor);

                            read++;
                            x++;
                            break;
                        }

                    // lever
                    case 'l':
                        {
                            // rectable for location
                            Rectangle location = new Rectangle((x * (1000 / 10)), (y * (800 / 10)), beforeLever.Width, beforeLever.Height);

                            // making lever object and adding it to array
                            lever = new GameObject(location, beforeLever);
                            inputObjects.Add(lever);

                            read++;
                            x++;
                            break;
                        }

                    // button
                    case 'b':
                        {
                            // rectangle for location
                            Rectangle location = new Rectangle((x * (1000 / 10)), (y * (835 / 10)), beforeButton.Width, beforeButton.Height);

                            // making button object and adding it to array
                            button = new GameObject(location, beforeButton);
                            inputObjects.Add(button);

                            read++;
                            x++;
                            break;
                        }

                    default:
                        {
                            x++;
                            read++;
                            break;
                        }

                }

                if (read % width == 0)
                {
                    reader.ReadLine();
                    y++;
                    x = 0;
                }

            }
        }
    }
}
