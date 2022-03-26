using System;

namespace Crawler
{
    /**
     * The main class of the Dungeon Crawler Application
     * 
     * You may add to your project other classes which are referenced.
     * Complete the templated methods and fill in your code where it says "Your code here".
     * Do not rename methods or variables which already exist or change the method parameters.
     * You can do some checks if your project still aligns with the spec by running the tests in UnitTest1
     * 
     * For Questions do contact us!
     */
    public class CMDCrawler
    {
        /**
         * use the following to store and control the next movement of the yser
         */
        public enum PlayerActions { NOTHING, NORTH, EAST, SOUTH, WEST, PICKUP, ATTACK, QUIT };
        private PlayerActions action = PlayerActions.NOTHING;
        private string SimpleMap = @"..\..\..\maps\Simple.Map"; // Stores the location of the map to SimpleMap
        private char[][] nmap = new char[0][];
        private bool MapChosen = false;
        private bool PlayN = false;

        /**
         * tracks if the game is running
         */
        private bool active = true;


        /**
         * Reads user input from the Console
         * 
         * Please use and implement this method to read the user input.
         * 
         * Return the input as string to be further processed
         * 
         */
        private string ReadUserInput()
        {
            string inputRead = string.Empty;

            if (PlayN == true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                char KeyPress = info.KeyChar;
                inputRead = KeyPress.ToString();
                inputRead = inputRead.ToUpper();
            }
            else
            {
                inputRead = Console.ReadLine();
            }

            return inputRead;
        }

        /**
         * Processed the user input string
         * 
         * takes apart the user input and does control the information flow
         *  * initializes the map ( you must call InitializeMap)
         *  * starts the game when user types in Play
         *  * sets the correct playeraction which you will use in the GameLoop
         */
        public void ProcessUserInput(string input)
        {
            // Your Code here
            input = input.ToUpper(); // Character Sensitive. It will change the case to the opposite if neccessary
            if (input == "LOAD SIMPLE.MAP")
            {
                InitializeMap("Simple.Map"); // If Simple Map is requested, then Simple Map is loaded
            }
            if (input == "PLAY") // If the next input is play, the game is then started
            {
                if (MapChosen == true) // Checks if MapChosen is true
                {
                    PlayN = true; // If MapChosen is true then make PlayN true
                }
            }
            if (input == "QUIT")
            {
                action = PlayerActions.QUIT; // If the input is 'QUIT' then the user quits the game
            }
            if (input == "W")
            {
                action = PlayerActions.NORTH; // if 'W' is pressed, the character moves NORTH
            }
            if (input == "A")
            {
                action = PlayerActions.WEST; // If 'A' is pressed, the character moves WEST
            }
            if (input == "S")
            {
                action = PlayerActions.SOUTH; // If 'S' is pressed, the character moves SOUTH
            }
            if (input == "D")
            {
                action = PlayerActions.EAST; // If 'D' is pressed, the character moves EAST
            }

        }

        /**
         * The Main Game Loop. 
         * It updates the game state.
         * 
         * This is the method where you implement your game logic and alter the state of the map/game
         * use playeraction to determine how the character should move/act
         * the input should tell the loop if the game is active and the state should advance
         */
        public void GameLoop(bool active)
        {
            // Your code here
            if (PlayN == true) // Checks if PlayN is true
            {
                int[] nPosition = GetPlayerPosition(); // The players position is saved as integer 'nPosition'
                int x = nPosition[0]; // int x is the position of the Player. X Axis
                int y = nPosition[1]; // Int Y is the position of the Player. Y Axis
                if (action == PlayerActions.NORTH) //NORTH
                {
                    nmap[y][x] = '.';
                    y = nPosition[1] - 1; // X Position is subtracted by 1 to go SOUTH on X Axis
                    if (nmap[y][x] == 'E')// Checks if the character is on 'E'
                    {
                        action = PlayerActions.QUIT; // If the character is on 'E' then the game is QUIT
                    }
                    if (nmap[y][x] == '#' || nmap[y][x] == 'M')
                    {
                        x = nPosition[0];
                        y = nPosition[1];
                        action = PlayerActions.NOTHING;
                    }
                    nmap[y][x] = '@';
                }
                if (action == PlayerActions.WEST) // WEST
                {
                    nmap[y][x] = '.';
                    x = nPosition[0] - 1; // X Position is subtracted by 1 to go WEST on X Axis
                    if (nmap[y][x] == 'E') // Checks if the character is on 'E'
                    {
                        action = PlayerActions.QUIT; // If the character is on 'E' then the game is QUIT
                    }
                    if (nmap[y][x] == '#' || nmap[y][x] == 'M')
                    {
                        x = nPosition[0];
                        y = nPosition[1];
                        action = PlayerActions.NOTHING;
                    }
                    nmap[y][x] = '@';
                }
                if (action == PlayerActions.SOUTH) // SOUTH
                {
                    nmap[y][x] = '.';
                    y = nPosition[1] + 1; // Y Position is added by 1 to go SOUTH on Y Axis
                    if (nmap[y][x] == 'E')// Checks if the character is on 'E'
                    {
                        action = PlayerActions.QUIT; // If the character is on 'E' then the game is QUIT
                    }
                    if (nmap[y][x] == '#' || nmap[y][x] == 'M')
                    {
                        x = nPosition[0];
                        y = nPosition[1];
                        action = PlayerActions.NOTHING;
                    }
                    nmap[y][x] = '@';
                }
                if (action == PlayerActions.EAST) //EAST
                {
                    nmap[y][x] = '.';
                    x = nPosition[0] + 1; // X Position is added by 1 to go EAST on X Axis
                    if (nmap[y][x] == 'E')// Checks if the character is on 'E'
                    {
                        action = PlayerActions.QUIT; // If the character is on 'E' then the game is QUIT
                    }
                    if (nmap[y][x] == '#' || nmap[y][x] == 'M')
                    {
                        x = nPosition[0];
                        y = nPosition[1];
                        action = PlayerActions.NOTHING;
                    }
                    nmap[y][x] = '@';
                }
                for (int i = 0; i < nmap.Length; i++)
                {
                    Console.WriteLine(new string(nmap[i])); // New string created with nmap
                }
                Console.WriteLine("{0}, {1}", y, x);
            }
        }

        /**
        * Map and GameState get initialized
        * mapName references a file name 
        * 
        * Create a private object variable for storing the map in Crawler and using it in the game.
        */
        public bool InitializeMap(String mapName)
        {
            bool initSuccess = true;
            // Your code here
            MapChosen = true;
            string[] ReadLines = System.IO.File.ReadAllLines(SimpleMap); // Reads all lines from Simple Map and saves to string ReadLines
            char[][] NewArray = new char[ReadLines.Length][]; // New array with the length of ReadLines string
            for (int line = 0; line < ReadLines.Length; line++)
            {
                NewArray[line] = ReadLines[line].ToCharArray(); // ReadLines line saved into NewArray
            }
            nmap = NewArray;
            for (int y = 0; y < nmap.Length; y++)
            {
                for (int x = 0; x < nmap[y].Length; x++)
                {
                    if (nmap[y][x] == 'S') // If S is at the co ordinates
                    {
                        nmap[y][x] = '@'; // If S is there then put the character '@' there
                    }
                }
            }
            for (int i = 0; i < nmap.Length; i++)
            {
                Console.WriteLine(new string(nmap[i]));
            }
            return initSuccess;
        }

        /**
         * Returns a representation of the currently loaded map
         * before any move was made.
         */
        public char[][] GetOriginalMap()
        {
            char[][] map = new char[0][];

            // Your code here 
            if (MapChosen)
            {
                map = nmap; // If it is MapChosen, then "map = nmap"
            }

            return map; // Return map
        }

        /*
         * Returns the current map state 
         * without altering it 
         */
        public char[][] GetCurrentMapState()
        {
            // the map should be map[y][x]
            char[][] map = new char[0][];

            // Your code here
            for (int y = 0; y < nmap.Length; y++)
            {
                for (int x = 0; x < nmap[y].Length; x++)
                {
                    if (nmap[y][x] == '@') // Checks if the co ordinates = '@'
                    {
                        nmap[y + 1][x] = '.'; // y + 1 with '.' 
                    }
                }
            }
            return nmap; // Returns nmap
        }

        /**
         * Returns the current position of the player on the map
         * 
         * The first value is the x corrdinate and the second is the y coordinate on the map
         */
        public int[] GetPlayerPosition()
        {
            int[] position = { 0, 0 };

            // Your code here
            for (int y = 0; y < nmap.Length; y++)
            {
                for (int x = 0; x < nmap[y].Length; x++)
                {
                    if (nmap[y][x] == '@') // Checks if the co ordinates have '@'
                    {
                        position[0] = x; // if so, 'x' turns into position[0]
                        position[1] = y; // if so, 'y' turns into position[1]
                    }
                }
            }
            return position;
        }

        /**
        * Returns the next player action
        * 
        * This method does not alter any internal state
        */
        public int GetPlayerAction()
        {
            int action = (int)this.action;
            // Your code here

            return action;
        }


        public bool GameIsRunning()
        {
            bool running = false;

            //Your code here
            if (PlayN == true) // Checks if PlayN = true
            {
                running = true; // if PlayN was true, it turns running into true
            }
            return running; // Returns running
        }

        /**
         * Main method and Entry point to the program
         * ####
         * Do not change! 
        */
        static void Main(string[] args)
        {
            CMDCrawler crawler = new CMDCrawler();
            string input = string.Empty;
            Console.WriteLine("Welcome to the Commandline Dungeon!" + Environment.NewLine +
                "May your Quest be filled with riches!" + Environment.NewLine);

            // Loops through the input and determines when the game should quit
            while (crawler.active && crawler.action != PlayerActions.QUIT)
            {
                Console.WriteLine("Your Command: ");
                input = crawler.ReadUserInput();
                Console.WriteLine(Environment.NewLine);

                crawler.ProcessUserInput(input);

                crawler.GameLoop(crawler.active);
            }

            Console.WriteLine("See you again" + Environment.NewLine +
                "In the CMD Dungeon! ");
        }
    }