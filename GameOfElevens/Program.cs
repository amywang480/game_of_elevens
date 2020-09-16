//Author:           Amy Wang
//File Name:        Program.cs
//Project Name:     GameOfElevens
//Creation Date:    September 11, 2018
//Modified Date:    September 20, 2018
/*Description:      Run Game of Elevens, a simple card game where the user's goal is to have face cards on
                    all piles by either selecting two cards adding up to eleven or selecting a pile with a
                    single face card to move to the bottom of the deck*/

using System;
using System.Collections.Generic;

namespace GameOfElevens
{
    class Program
    {
        //CARDS:
        //Store card deck
        static List<string> deck = new List<string>();

        //Store laid out card piles
        static string[,] cards = new string[2, 6];
        
        //Store rows and columns of cards
        static int[] row = new int[3];
        static int[] col = new int[3];

        //DISPLAY CARDS:
        //Store number of cards in pile
        static int[] numPile = new int[12];

        //Store letters for selecting pile
        static string selectLetter;

        //BEGINNING OF GAME:
        //Store user's choice
        static string choice;

        //PLAYING THE GAME:
        //Store card piles selected
        static string pile1;
        static string pile2;
        static string pile3;

        //Store values of cards on piles
        static string value1;
        static string value2;
        static string value3;
        
        //Store number of cards in pile
        static int number;
        
        //END OF GAME:
        //Determine if user has lost the game
        static bool losing;

        //Determine if user has won the game
        static bool isGameWon;

        static void Main(string[] args)
        {
            //Arrange deck of cards
            PrepareDeck();

            //Set initial pile numbers
            SetPileNumbers();

            //Display game
            DisplayGame();
            DisplayWelcome();
            DisplayOptions();

            //Carry out user's choice
            UserChoice();
            
            //Wait for user input
            Console.ReadLine();
        }

        //Pre: None
        //Post: Shuffled list of cards
        //Description: Prepare deck of cards by randomizing locations of cards
        private static void PrepareDeck()
        {
            //Generate random numbers
            Random rng = new Random();
            int randomNum;

            //Select cards to add back with new index
            string addBack;
            int newIndex;

            //Add full set of cards to deck
            for (int i = 0; i < 4; ++i)
            {
                deck.Add("J");
                deck.Add("Q");
                deck.Add("K");

                for (int j = 1; j < 11; ++j)
                {
                    deck.Add(Convert.ToString(j));
                }
            }
            
            //Shuffle deck
            for (int i = 0; i < 60; ++i)
            {
                //Select random card
                randomNum = rng.Next(0, 52);
                addBack = deck[randomNum];

                //Remove card
                deck.RemoveAt(randomNum);

                //Add back in random location
                newIndex = rng.Next(0, 52);
                deck.Insert(newIndex, addBack);
            }

            //Distribute cards to piles
            Distribute();
        }

        //Pre: None
        //Post: Cards are laid out on the piles
        //Description: Distribute the first 12 cards of the deck to the 12 piles
        private static void Distribute()
        {
            //Distribute among rows
            for (int i = 0; i < 6; ++i)
            {
                cards[0, i] = deck[i];
                cards[1, i] = deck[i + 6];
            }

            //Remove distributed cards from deck
            deck.RemoveRange(0, 12);
        }

        //Pre: None
        //Post: Pile numbers are set to 1
        //Description: Set pile numbers to starting values of 1
        private static void SetPileNumbers()
        {
            //Each pile starts with one card
            for (int i = 0; i < 12; ++i)
            {
                numPile[i] = 1;
            }
        }
  
        //Pre: None
        //Post: Elements of game are displayed on screen
        //Description: Display game screen
        private static void DisplayGame()
        {
            //Set title colour 
            Console.ForegroundColor = ConsoleColor.Cyan;
     
            //Display title
            Console.WriteLine("\n" + "\t" + "\t" + "\t" + "      " + "11 -----{GAME OF ELEVENS}----- 11" + "\n");
            
            //Set card colour
            Console.ForegroundColor = ConsoleColor.White;

            //Display cards
            CardRows();

            //Set text colour
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        //Pre: None
        //Post: Welcome to the game is displayed
        //Description: Display welcome to the game
        private static void DisplayWelcome()
        {
            //Store warm welcome for the user
            string welcome = " Welcome to the Game of Elevens! If you know all the pairs of natural positive rational mainly" + "\n" +
                             " single digit integers that are found in the domain x is an element of all integers such that 1" + "\n" +
                             " is less than or equal to x and x is less or equal to 10 that produce a sum of eleven, then this" + "\n" +
                             " is the game for you!";

            //Display warm welcome
            Console.WriteLine(welcome + "\n");
        }
        
        //Pre: None
        //Post: All cards and their holders are displayed
        //Description: Display rows of cards with pile letters, pile numbers, and holders
        private static void CardRows()
        {
            //Display cards for first row 
            for (int i = 0; i < 6; ++i)
            {
                //Change color of face cards
                ChangeColor(0, i);
                
                //Display cards
                Console.Write("  " + cards[0, i] + " [#: " + numPile[i] + "]" + "    " + "\t");
            }
            
            //Move cursor to next line
            Console.WriteLine("");
            
            //Display first row card holders
            CardHolders(0, 6, "top");
            CardHolders(0, 6, "middle");
            CardHolders(0, 6, "bottom");

            //Display stars between rows
            Stars();

            //Display cards for second row 
            for (int i = 6; i < 12; ++i)
            {
                //Change color of face cards
                ChangeColor(1, (i - 6));
                
                //Display cards
                Console.Write("  " + cards[1, i - 6] + " [#: " + numPile[i] + "]" + "    " + "\t");
            }

            //Move cursor to next line
            Console.WriteLine("");
            
            //Display second row card holders
            CardHolders(6, 12, "top");
            CardHolders(6, 12, "middle");
            CardHolders(6, 12, "bottom");

            //Move cursor to next line
            Console.WriteLine("");
        }
        
        //Pre: r is an integer that is 0 or 1 and c is an integer between 0 and 5
        //Post: Color of face cards is changed
        //Description: Change colors of face cards, using r and c to determine their location
        private static void ChangeColor(int r, int c)
        {
            //Change face cards to magenta and keep all other cards white
            if (cards[r, c] == "J" || cards[r, c] == "Q" || cards[r, c] == "K")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
   
        //Pre: start and end are integers of 0, 6, or 12 and display is a string of "top", "middle", or "bottom"
        //Post: Card holders are displayed
        //Description: Display top, middle, and bottom parts of card holders using start and end to display each row
        private static void CardHolders(int start, int end, string display)
        {
            //Set card holders to white
            Console.ForegroundColor = ConsoleColor.White;
           
            //Display all parts
            for (int i = start; i < end; ++i)
            {
                //Determine letters for piles
                DetermineLetter(i);
               
                //Store parts of the card holder
                string top = " \\_________/" + "\t";
                string middle = "    | " + selectLetter + " |" + "\t";
                string bottom = "    " + "^^^^^" + "\t";

                //Display card holders
                switch (display)
                {
                    case "top":
                        Console.Write(top);
                        break;
                    case "middle":
                        Console.Write(middle);
                        break;
                    case "bottom":
                        Console.Write(bottom);
                        break;
                }
            }

            //Move cursor to next line
            Console.WriteLine("");
        }
        
        //Pre: i is an integer between 0 and 11
        //Post: Letters are assigned to piles
        //Description: Call Letters subprogram to assign letters based on pile number(i)
        private static void DetermineLetter(int i)
        {
            //First row
            Letters(i, 0, "a");
            Letters(i, 1, "b");
            Letters(i, 2, "c");
            Letters(i, 3, "d");
            Letters(i, 4, "e");
            Letters(i, 5, "f");

            //Second row
            Letters(i, 6, "g");
            Letters(i, 7, "h");
            Letters(i, 8, "i");
            Letters(i, 9, "j");
            Letters(i, 10, "k");
            Letters(i, 11, "l");
        }

        //Pre: i and num are integers between 0 and 11 and letter is a string between a and l
        //Post: Letters are assigned to piles
        //Description: Assign letters to piles based on pile number(i)
        private static void Letters(int i, int num, string letter)
        {
            //Store possible letters to select
            if (i == num)
            {
                selectLetter = letter;
            }
        }
        
        //Pre: None
        //Post: Stars are displayed
        //Description: Display stars between card rows
        private static void Stars()
        {
            //First row of stars
            for (int i = 0; i < 3; ++i)
            {
                Console.Write("      " + "*" + "\t" + "\t" + "\t" + "\t");
            }

            //Move cursor to next line
            Console.WriteLine("");

            //Second row of stars
            for (int i = 0; i < 3; ++i)
            {
                Console.Write("\t" + "\t" + "      " + "*" + "\t" + "\t");
            }

            //Move cursor to next line
            Console.WriteLine("");
        }

        //Pre: None
        //Post: User's choice is determined 
        //Description: Display options and retrieve user's choice
        private static void DisplayOptions()
        {
            //Display instructions
            Console.WriteLine(" ---------------------------");
            Console.Write(" |Select your choice below:|");

            //Tab to place "Cards Left" on left side of screen
            for (int i = 0; i < 7; ++i)
            {
                Console.Write("\t");
            }

            //Display "Cards Left"
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Cards Left]");
            Console.WriteLine("");

            //Display underline for instructions
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" ---------------------------");

            //Tab to place number of cards in the deck on the left side
            for (int i = 0; i < 7; ++i)
            {
                Console.Write("\t");
            }

            //Display number of cards in deck
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[" + deck.Count + "]");

            //Display options
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" 1) " + "Create a sum of 11");
            Console.WriteLine(" 2) " + "Move a face card to the bottom of the deck" + "\n");
            Console.Write(" ");

            //Store choice
            choice = Console.ReadLine();
        }
  
        //Pre: None
        //Post: Results of user's choice are carried out
        //Description: Carry out user's choice and ensure user selects one of the choices
        private static void UserChoice()
        {
            //Prevent user from entering other numbers
            while (choice != "1" && choice != "2")
            {
                //Display message
                Console.WriteLine("\n" + " Please select 1 or 2");
                Console.ReadLine();
                Console.Clear();

                //Display start screen
                DisplayGame();
                DisplayOptions();
            }

            //Carry out results of choice
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    DisplayGame();
                    Sum11();
                    break;
                case "2":
                    Console.Clear();
                    DisplayGame();
                    MoveFaceCard();
                    break;
            }
        }

        //Pre: None
        //Post: Cards that have a sum of 11 are added
        //Description: Add cards with a sum of 11 and determine if user wins or loses as a result
        private static void Sum11()
        {
            //Retrieve piles selected by user
            Piles();

            //Add cards to find sum
            AddCards();
        }

        //Pre: None
        //Post: Pile letters are retrieved
        //Description: Retrieve pile letters entered and handle errors
        private static void Piles()
        {
            //Ask for and retrieve first pile
            Console.WriteLine(" ***Type 's' to return to the start page***" + "\n");
            Console.WriteLine(" Select the first pile of cards:");
            Console.WriteLine(" -------------------------------");
            Console.Write(" ");
            pile1 = Console.ReadLine();
            
            //Determine card value on pile
            PileValue(pile1, 0);
        
            //Switch between first pile possible errors
            SwitchErrors1();
            
            //Ask for and retrieve second pile
            Console.WriteLine("\n" + " Select the second pile of cards:");
            Console.WriteLine(" -------------------------------");
            Console.Write(" ");
            pile2 = Console.ReadLine();
            
            //Determine card value on pile
            PileValue(pile2, 1);
        
            //Switch between second pile possible errors
            SwitchErrors2();
        }

        //Pre: pile is a string of a letter between a and l and index is an integer between 0 and 2
        //Post: Cards on top of chosen piles are determined
        //Description: Determine the top cards on chosen piles and use index to store rows and columns of cards
        private static void PileValue(string pile, int index)
        {
            //Store total number of columns
            int numCols = 6;

            //Catch errors for incorrect input
            try
            {
                //Use each letter's distance from 'a' to find each letter's corresponding pile
                int pileNumber = (int)Char.Parse(pile) - (int)('a');

                //Determine row and column of pile
                int rowNum = pileNumber / numCols;
                int colNum = pileNumber % numCols;

                //Store row and column
                row[index] = rowNum;
                col[index] = colNum;

                //Store value of top card on pile
                CardValue(index);
            }
            catch
            {
                //Store a value of 0 for incorrect input
                ValueDefault(index);

                //Allow user to type 's' to return to start screen
                if (pile == "s")
                {
                    StartScreen();
                }
            }
        }
        
        //Pre: index is an integer between 0 and 2
        //Post: Card values are stored
        //Description: Store value of cards on top of piles
        private static void CardValue(int index)
        {
            //Use values 1 and 2 for the two cards that produce 11 
            //Use value 3 for the face card to be removed
            switch (index)
            {
                case 0:
                    value1 = cards[row[0], col[0]];
                    break;
                case 1:
                    value2 = cards[row[1], col[1]];
                    break;
                case 2:
                    value3 = cards[row[2], col[2]];
                    break;
            }
        }

        //Pre: index is an integer between 0 and 2
        //Post: Default value of 0 is given for incorrect input
        //Description: Assign values of 0 by using index to assign values individually
        private static void ValueDefault(int index)
        {
            //Give a value of 0 to the card value involved
            switch (index)
            {
                case 0:
                    value1 = "0";
                    break;
                case 1:
                    value2 = "0";
                    break;
                case 2:
                    value3 = "0";
                    break;
            }
        }

        //Pre: None
        //Post: Error messages are displayed
        //Description: Switch between and display error messages for incorrect input
        private static void SwitchErrors1()
        {
            //Ensure only pile letters are entered (not piles with face cards)
            while (value1 == "0" || value1 == "J" || value1 == "Q" || value1 == "K")
            {
                //Prevent input other than letters
                while (value1 == "0")
                {
                    Pile1Error(" Please select a pile letter");
                }
            
                //Prevent face cards from being selected
                while (value1 == "J" || value1 == "Q" || value1 == "K")
                {
                    Pile1Error(" Please select a pile without a face card");
                }
            }
        }
        
        //Pre: message is a string holding an error message
        //Post: Error message is displayed
        //Description: Display error message for incorrect input for first pile
        private static void Pile1Error(string message)
        {
            //Display message
            Console.WriteLine("\n" + message);
            Console.ReadLine();

            //Clear previous answer
            Console.Clear();

            //Display game and retrieve pile letter
            DisplayGame();
            SelectPile();
            Console.Write(" ");
            pile1 = Console.ReadLine();

            //Determine new pile value
            PileValue(pile1, 0);
        }

        //Pre: None
        //Post: Error messages are displayed
        //Description: Switch between and display error messages for incorrect input
        private static void SwitchErrors2()
        {
            //Ensure only pile letters are entered (not piles with face cards)
            while (value2 == "0" || value2 == "J" || value2 == "Q" || value2 == "K")
            {
                //Prevent input other than letters
                while (value2 == "0")
                {
                    Pile2Error(" Please select a pile letter");
                }

                //Prevent face cards from being selected
                while (value2 == "J" || value2 == "Q" || value2 == "K")
                {
                    Pile2Error(" Please select a pile without a face card");
                }
            }
        }

        //Pre: message is a string holding an error message
        //Post: Error message is displayed
        //Description: Display error message for incorrect input for second pile
        private static void Pile2Error(string message)
        {
            //Display message
            Console.WriteLine("\n" + message);
            Console.ReadLine();

            //Clear previous answer
            Console.Clear();

            //Display game and retrieve pile letter
            DisplayGame();
            SelectPile();
            Console.WriteLine(" " + pile1);
            Console.WriteLine("\n" + " Select the second pile of cards:");
            Console.WriteLine(" -------------------------------");
            Console.Write(" ");
            pile2 = Console.ReadLine();

            //Determine new pile value
            PileValue(pile2, 1);
        }
    
        //Pre: None
        //Post: Select pile text is displayed
        //Description: Display text to select the first pile of cards
        private static void SelectPile()
        {
            Console.WriteLine(" ***Type 's' to return to the start page***" + "\n");
            Console.WriteLine(" Select the first pile of cards:");
            Console.WriteLine(" -------------------------------");
        }
        
        //Pre: None
        //Post: Cards are added when a sum of 11 is produced and the start screen is redisplayed
        //Description: Add cards selected
        private static void AddCards()
        {
            //Calculate sum
            int sum = Convert.ToInt32(value1) + Convert.ToInt32(value2);
      
            //Determine if sum is 11
            if (sum == 11)
            {
                //Add new cards on piles
                NewCards(row[0], col[0], 0);
                NewCards(row[1], col[1], 1);

                //Remove cards from deck
                deck.RemoveRange(0, 2);

                //Display start screen
                StartScreen();
            }
            else
            {
                //Display message
                Console.WriteLine("\n" + " The sum of the cards is not 11. Try again!");
                Console.ReadLine();

                //Display start screen
                StartScreen();
            }
        }

        //Pre: r is an integer of 0 or 1, c is an integer between 0 and 5, and index is an integer of 0 or 1
        //Post: New cards from the deck are added on selected piles
        /*Description: Place next two cards on the pair of cards with a sum of 11 by using r and c to determine
        card location and index to remove first two cards from deck*/
        private static void NewCards(int r, int c, int index)
        {
            //Store new cards
            cards[r, c] = deck[index];

            //Increment number of cards in the pile
            numPile[r * 6 + c]++;

            //Determine if the user lost the game
            losing = Lose();

            //Display congratulatory losing message if game has been lost
            if (losing)
            {
                isGameWon = false;
                EndMessage("Congratulations! You lost the game!");
            }

            //Determine if the user has won the game
            Win();
        }
 
        //Pre: None
        //Post: A face card is moved to the bottom of the deck and replaced by another card
        //Description: Determine if a face card can be moved back into the deck and replaced
        private static void MoveFaceCard()
        {
            //Retrieve selected pile 
            Console.WriteLine(" ***Type 's' to return to the start page***" + "\n");
            Console.WriteLine(" Select the pile whose only card is a face card:");
            Console.WriteLine(" -----------------------------------------------");
            Console.Write(" ");
            pile3 = Console.ReadLine();
        
            //Determine value of card on pile
            PileValue(pile3, 2);

            //Store number of cards in pile
            if (row[2] == 0)
            {
                number = numPile[col[2]];
            }
            else if (row[2] == 1)
            {
                number = numPile[col[2] + 6];
            }

            //Switch between possible error messages
            SwitchErrors3();
           
            //Add face card in deck and replace with new card
            deck.Add(value3);
            cards[row[2], col[2]] = deck[0];
            deck.RemoveAt(0);

            //Determine if the user has lost the game
            losing = Lose();

            //Display losing message if game has been lost
            if (losing)
            {
                isGameWon = false;
                EndMessage("Congratulations! You lost the game!");
            }

            //Determine if the user has won the game
            Win();
        
            //Return to start screen
            StartScreen();
        }
   
        //Pre: None
        //Post: Error messages are displayed
        //Description: Switch between possible error messages for incorrect input
        private static void SwitchErrors3()
        {
            //Ensure only piles with single face cards are selected
            while ((value3 != "J" && value3 != "Q" && value3 != "K") || number > 1)
            {
                //Prevent any input that does not select a pile with a face card
                while (value3 != "J" && value3 != "Q" && value3 != "K")
                {
                    Pile3Error(" The selected pile does not have a face card. Try again!");
                }

                //Prevent selection of piles with a face card and more than one card
                while ((value3 == "J" || value3 == "Q" || value3 == "K") && number > 1)
                {
                    Pile3Error(" The selected pile has more than one card. Try again!");
                }
            }
        }
   
        //Pre: message is a string holding an error message
        //Post: Error message is displayed
        //Description: Continue to display error messages until correct input is received
        private static void Pile3Error(string message)
        {
            //Display message and game
            Console.WriteLine("\n" + message);
            Console.ReadLine();
            Console.Clear();
            DisplayGame();

            //Retrieve selected pile
            Console.WriteLine(" ***Type 's' to return to the start page***" + "\n");
            Console.WriteLine(" Select the pile whose only card is a face card:");
            Console.WriteLine(" -----------------------------------------------");
            Console.Write(" ");
            pile3 = Console.ReadLine();
        
            //Determine value of top card on pile
            PileValue(pile3, 2);

            //Determine number of cards in the pile
            if (row[2] == 0)
            {
                number = numPile[col[2]];
            }
            else if (row[2] == 1)
            {
                number = numPile[col[2] + 6];
            }
        }
       
        //Pre: None
        //Post: Start screen is displayed
        //Description: Display the game's start screen containing the cards and options
        private static void StartScreen()
        {
            Console.Clear();
            DisplayGame();
            DisplayOptions();
            UserChoice();
        }

        //Pre: None
        //Post: isGameFinished is returned and determines if the game is over
        //Description: Determine if the user has lost the game
        private static bool Lose()
        {
            //Determine if game ends
            bool isGameFinished = true;

            //Check if all options are impossible
            if (isGameFinished)
            {
                //Select a test card
                for (int row = 0; row < 2 && isGameFinished == true; ++row)
                {
                    for (int col = 0; col < 6 && isGameFinished == true; ++col)
                    {
                        //Determine if the card is a face card
                        if (cards[row, col] == "J" || cards[row, col] == "Q" || cards[row, col] == "K")
                        {
                            //If it is a single face card, game continues
                            if (numPile[row * 6 + col] == 1)
                            {
                                isGameFinished = false;
                            }
                        }
                        else
                        {
                            //Check if cards can produce a sum of 11
                            if (isGameFinished)
                            {
                                //Select comparison cards that are compared with test card
                                for (int row2 = 0; row2 < 2 && isGameFinished == true; ++row2)
                                {
                                    for (int col2 = 0; col2 < 6 && isGameFinished == true; ++col2)
                                    {
                                        //Select cards that are not face cards
                                        if (cards[row2, col2] != "J" && cards[row2, col2] != "Q" && cards[row2, col2] != "K")
                                        {
                                            //If the cards produce a sum of 11, game continues
                                            if (Convert.ToInt32(cards[row, col]) + Convert.ToInt32(cards[row2, col2]) == 11)
                                            {
                                                isGameFinished = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Return bool to determine if game finishes
            return isGameFinished;
        }
   
        //Pre: None
        //Post: Winning message is displayed with options of exiting and playing again
        //Description: Determine if the user has won the game
        private static void Win()
        {
            //Store number of face cards
            int jacks = 0;
            int queens = 0;
            int kings = 0;

            //Determine numbers of face cards
            for (int r = 0; r < 2; ++r)
            {
                for (int c = 0; c < 6; ++c)
                {
                    //Add up jacks
                    if (cards[r, c] == "J")
                    {
                        jacks++;
                    }

                    //Add up queens
                    if (cards[r, c] == "Q")
                    {
                        queens++;
                    }

                    //Add up kings
                    if (cards[r, c] == "K")
                    {
                        kings++;
                    }
                }
            }
      
            //Display winning message when all face cards are present
            if (jacks == 4 && queens == 4 && kings == 4)
            {
                isGameWon = true;
                EndMessage("Congratulations! You won the game!");
            }
        }
   
        //Pre: message is a string that holds the winning/losing message
        //Post: End message is displayed with options to play again and exit
        //Description: Display end message and allow user to play again or exit
        private static void EndMessage(string message)
        {
            //Store user input for playing or exiting
            string playExit;

            //Display game
            Console.Clear();
            DisplayGame();

            //Display options and retrieve user input
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ---------" + message + "---------" + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Would you like to go and do something productive with your life or play again?" + "\n");
            Console.WriteLine(" ***Type 'p' to play again or 'e' to exit***" + "\n");
            Console.Write(" ");
            playExit = Console.ReadLine();

            //Allow user to play again or exit
            PlayAgain(playExit);
        }
  
        //Pre: playExit is a string of 'p' or 'e'
        //Post: Game is either redisplayed or closed
        //Description: Allow the user to play again or close the game
        private static void PlayAgain(string playExit)
        {
            //Prevent user from entering input other than 'p' and 'e'
            while (playExit != "p" && playExit != "e")
            {
                Console.WriteLine("\n" + " Please select 'p' or 'e'");
                Console.ReadLine();

                //Determine message to display
                if (isGameWon)
                {
                    EndMessage("Congratulations! You won the game!");
                }
                else
                {
                    EndMessage("Congratulations! You lost the game!");
                }
            }
   
            //Carry out results of user choice
            if (playExit == "p")
            {
                //Display game
                Console.Clear();
                deck.Clear();
                PrepareDeck();
                SetPileNumbers();
                StartScreen();
            }
            else
            {
                //Close game
                Environment.Exit(0);
            }
        }
    }
}
