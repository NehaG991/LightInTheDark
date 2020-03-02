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

        // file path variable
        private string filePath;

        // gameobject list for interactable sprites
        private List<GameObject> interactable;

        // gameobject for player
        private Player player;

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
            set
            {
                player = value;
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

        // constructor
        public LevelReader(Texture2D plat, Texture2D play, string path)
        {
            filePath = path;
            interactable = new List<GameObject>();
            platform = plat;
            playerSprite = play;
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
