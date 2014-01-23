using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static int ability1 = 0, ability2 = 0;
        static int[] Stats = new int[11];
        static string[] StatNames = new string[9] { "HP", "MP", "AP", "MR", "ATK", "DEF", "LUCK", "XP", "LVL" };
        static int[] FightStats = new int[7];
        static string[] myskills = new string[2];
        static bool MagicalMonster = false;
        static int Area = 1;
        static int Checkpoint = 0;
        static int fightcounter = 0;
        static string userName = String.Empty;
        static string initialuserName = String.Empty;
        static int PossibleCharacter;
        static int[] repeat = new int[4] { 0, 0, 0, 0 };
        static int[] areas = new int[4] { 0, 0, 0, 0 };
        static int[] routestaken = new int[5] { 0, 0, 0, 0, 0 };
        static int actualroute = 0;
        static int areacounter = 0;
        static int BossCounter = 0;


        static void Main(string[] args)
        {
            bool runGame = true;
            do
            {
                for (int i = 0; i < 1000; i++)
                {
                    ShowDragon();
                    Console.Clear();
                }
                ShowDragon();
                Enter();

                //START MENU
                Console.WriteLine("Welcome to the Game." + Environment.NewLine + "Please press ENTER to continue.");
                Console.ReadLine();
                Console.Clear();
                InitialMenu();
                int initialOption = ValidOption(1, 3, 1, 0, 0, 0, "0");//the last four inputs do not matter
                Console.Clear();

                //INITIAL OPTION
                switch (initialOption)
                {
                    case 1://Start New Game
                        //Set username and password
                        userName = SetUserName();
                        initialuserName = userName;
                        string userPassword = SetUserPassword();
                        Console.Clear();
                        //Choose a race
                        ChoosingCharacter();
                        Stats = LoadStats(PossibleCharacter);//once the character is chosen, its initial stats are loaded from the method LoadStats
                        Console.Clear();
                        //Print stats
                        PrintStats(Stats, "You have the following Stats:");
                        Enter();
                        Console.WriteLine("Are you ready to start your quest?");
                        Enter();
                        //Tutorial
                        Console.WriteLine("{0}, you are about to embark on the adventure of your life.", userName);
                        Console.WriteLine("Throughout your journey you must make constant decisions. \nThey will shape your future, so be cautitious... \nSituations will be presented to you and you must choose from a menu. \nFor instance, you may:");
                        Tutorial();

                        //Initial Story & First Fight
                        InitialStory();

                        while (fightcounter < 10)
                        {
                            fight();
                        }

                        /*Event generator
                        Random RandomBoss = new Random();
                        int BossNumber = RandomBoss.Next(1, 5);//Assuming that there are 4 bosses to choose from
                        for (int i = 1; i <= 4; i++)
                        {
                            bool StartBossBattle;
                            for (int j = 1; j <= 4; i++)//4 events will happen before facing a boss
                            {
                                fight();
                            }
                            */

                        /*SUMMON BOSS
                         * do
                         * {
                         * StartBossBattle = true;
                         * int SummonBoss = BossMethod(BossNumber);
                         * int[] BossThatHaveAlreadyAppeared = new int[4];
                         * BossThatHaveAlreadyAppeared[i] = SummonBoss;
                         * foreach (int n in BossThatHaveAlreadyAppeared)
                         * {
                         *      if (BossThatHaveAlreadyAppeared[i] == n)
                         *      {
                         *          StartBossBattle = false;
                         *      }
                         * }
                         * }while(StartBossBattle = false);
                         * START BATTLE METHOD

                    }*/

                        //Final Boss
                        Console.WriteLine(userName + ", all your voyage has taken you here." + Environment.NewLine + "You befriended some of the most odd beings, and you have made unlikely alliances." + "And now you stand before the gates of Galourion." + Environment.NewLine + "In the guts of Zeme, in the Numb, lies the Kaikkivaltius." + Environment.NewLine + "The Tanrilar placed the mightiest of beasts there, knowing that one day someone would try to claim it.");
                        Enter();
                        break;

                    case 2://Load Game
                        break;

                    case 3://Quit Game
                        Console.WriteLine("Are you sure you want to Quit the Game?");
                        Console.WriteLine("{0} {1, 6}", "No", "Yes");
                        if (Console.ReadLine().ToLower() == "yes")
                        {
                            runGame = false;
                        }
                        else
                        {
                            Console.Clear();
                        }
                        break;
                }
            } while (runGame == true);
        }

        private static void Tutorial()
        {
            Console.WriteLine("1.Walk forward \n2.Turn left \n3.Turn right \n4.Turn around");
            int simpleDecision = ValidOption(1, 4, 7, 0, 0, 0, "0");
            switch (simpleDecision)
            {
                case 1:
                    Console.WriteLine("You may not walk any further. You have stumbled upon a wall. \nWhat do you want to do?");
                    Tutorial();
                    break;
                case 2:
                    Console.WriteLine("You may keep walking.");
                    break;
                case 3:
                    Console.WriteLine("You are now facing a wall. \nYou must turn around and keep walking.");
                    Console.WriteLine("Will you: \n1.Turn around. \n2.Keepwalking.");
                    switch (ValidOption(1, 2, 8, 0, 0, 0, "0"))
                    {
                        case 1:
                            Console.WriteLine("Perfect. You turned around.");
                            break;
                        case 2:
                            Console.WriteLine("Didn't you get it? You may not keep walking. \nAnyway, we are done here.");
                            break;
                    }
                    break;
                case 4:
                    Console.WriteLine("Good, now you have turned around. \nThere is a whole path for you to explore.");
                    break;
            }
            Console.WriteLine("You have reached the end of the tutorial. Press any button to continue.");
            Enter();
        }

        private static void InitialStory()
        {
            switch (Stats[9])//Check what species is the player
            {
                case 1://HUMAN
                    switch (Stats[10])//Check what type is the player
                    {
                        case 1://PALADIN
                            Console.WriteLine("You are a very powerful human, among its most valuable members." + Environment.NewLine + "You woke up in the woods; alone and cold." + Environment.NewLine + "You are trying to remember, and as you see yourself you realize..." + Environment.NewLine + "You are gravely injured!" + Environment.NewLine + "Now you remember. You were left behind after the great battle." + Environment.NewLine + "In order to find your people you must regain your strength, heal yourself and make your way through Zeme.");
                            FirstFight("An elf has appeared behind you.", "You can not run. The elf is too fast. \nHe has inflicted damage on you. \nYou must be brave and attack!", "You defeated the elf. \nBut he was not very strong. \nDo not hope any future enemies will be as easy to defeat as this one.");
                            break;
                        case 2://WIZARD
                            Console.WriteLine("You have lived in isolation over the last years." + Environment.NewLine + "But now you know the time has come for humans to take their finale revenge against the evil forces that oppressed you." + Environment.NewLine + "You have not battled for decades, so in order to be prepared you must increase your power." + Environment.NewLine + "As you know, magic is a natural force, so you must become one with it in order to control it.");
                            FirstFight("As you walk out of your den, you see a group of Lemurian soldiers.", "You can not flee. You must fight.", "You have overcomed these Lemurians. \nBut they were lousy warriors. Trust me, this is nothing compared to what is prepared for you.");
                            break;
                        case 3://WARRIOR
                            Console.WriteLine("Alone. You have been alone for the longest weeks of your life." + Environment.NewLine + "You are now lurking for the blood of those who killed your friends." + Environment.NewLine + "Prepare to encounter the mighty Lemurians");
                            FirstFight("A band of Dark Elves is in your path.", "You must fight them. \nYou have no alternative.", "You defeated them, but do not feel too lively. \nThis is just your first encounter with an enemy.");
                            break;
                    }
                    break;
                case 2://LEMURIAN
                    switch (Stats[10])
                    {
                        case 1://PALADIN
                            Console.WriteLine("You are gathered with your tribe. The clan of Zkahart." + Environment.NewLine + "You have seen great battles. Terrible battles." + Environment.NewLine + "For a long time you were against this war. But you realized you had no option." + Environment.NewLine + "Now you work as a healer among warriors.");
                            FirstFight("Suddenly, a horde of Dark Elves attacks you.", "You can not run. You are sorrounded. \nThe Elves have inflicted damage on you. \nYou must be brave and attack!", "You defeated the Dark Elves. \nBut this is just the beginning of your quest.");
                            break;
                        case 2://ROGUE
                            Console.WriteLine("Very deep into the heart of the Sacred Mountains of the Jungle of Eve you spy upon your enemies." + Environment.NewLine + "You heard, at the frontiers of Lemurian territory, that Humans and Dark Elves plot against Lemurians." + Environment.NewLine + "In your very own territory your enemies are preparing their final blow.");
                            FirstFight("Suddenly, a horde of Dark Elves attacks you.", "You can not run. You are sorrounded. \nThe Elves have inflicted damage on you. \nYou must be brave and attack!", "You defeated the Dark Elves. \nBut this is just the beginning of your quest.");
                            break;
                        case 3://WARRIOR
                            Console.WriteLine("The blood of weak humans lies in your hand. Their skulls are tied around your neck." + Environment.NewLine + userName + ", the great Warrior of the Lemurians." + Environment.NewLine + "Tale tellers' tales are whispers through time." + Environment.NewLine + "Their whispers whisper about the atrocities you have committed." + Environment.NewLine + "So big is your wrath that you have been exiled from the city of Hart Roth, cradle of the Lemurian civilization.");
                            FirstFight("Suddenly, a horde of Human Cannibals attacks you.", "You can not run. You are sorrounded. \nThe humans have inflicted damage on you. \nYou must be brave and attack!", "You defeated the Human Cannibals. \nBut this is just the beginning of your quest.");
                            break;
                    }
                    break;
                case 3://DARK ELVES
                    switch (Stats[10])
                    {
                        case 1://WARLOCK
                            Console.WriteLine("The ancient texts written by Wrezllt narrate the story of a mortal that shut the sun down." + Environment.NewLine + "As you read the last lines you realize the unimaginable:" + Environment.NewLine + "this mortal was a human; the most disgusting living being on Zeme was capable of stopping the loathsome star.");
                            FirstFight("All of a sudden, you hear a squeaky noise through the halls of the library. \nA Dark Mage wants to steal the texts from you!", "You may not escape. \nThe mage has sealed all the exits.", "The mage has been defetead. \nBut it is mostly certain that more enemies like him will appear on your path.");
                            break;
                        case 2://WIZARD
                            Console.WriteLine("You have worked endlessly on hidding the forbidden texts." + Environment.NewLine + userName + ", the great protector." + Environment.NewLine + "That is how you were called before you disappeared, fleeing into the darkest realm of Zeme... the Numb.");
                            FirstFight("But another Elf yearns those texts. \nAnd he has appeared before you to reclaim them. \nYou shall stand strong and protect the texts.", "You cannot flee. The Elf is a powerfull Warlock, and has sealed every exit.", "Now you must exit the Numb, and take the texts somewhere else. \nIt is evident that they are not safe there anymore.");
                            break;
                        case 3://ROGUE
                            Console.WriteLine(userName + ", you are guarding a secret pact between Humans and Elves." + Environment.NewLine + "It was inconceivable before, but the path that the war has taken required such measures." + Environment.NewLine + "Although you are reluctant to believe such a pact will render Elves victorious," + Environment.NewLine + "you know it is your sole duty to guard the great generals while they generate a plan along humans." + Environment.NewLine + "Deep into the heart of the sacred Mountains of the Jungle of Eve you serve your purpose.");
                            FirstFight("You listen to a gentle a whisper by your ear. \nWhen you turn around you see a Lemurian Warrior. \nHe has come to kill the generals!", "What are you thinking about? \nIf you flee, you will be accused of high treason. \nStand your grpund soldier.", "You killed the Lemurian. But he was a mere distraction. \nWhile you were fighting a small band of lemurians");
                            break;
                    }
                    break;
            }
        }

        private static void ShowDragon()
        {
            string dragonImage = System.IO.File.ReadAllText(@"C:\Users\David\Documents\Universidad - ICE\Semestre 1\Métodos de progra I\MK Ultra\Images\dragon_image.txt");
            Console.WriteLine(dragonImage);
        }

        private static void FirstFight(string Text1, string Text2, string Text3)//This method changes the ammount of life Points, i.e. Stats[0]. Since this variable is global, this method may be void. 
        {
            Enter();
            Console.WriteLine(Text1);
            bool FirstBattle = true;
            while (FirstBattle == true)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Try to flee \n2.Attack");
                int option = ValidOption(1, 2, 5, 0, 0, 0, "0");
                switch (option)
                {
                    case 1:
                        Console.WriteLine(Text2);
                        Console.WriteLine("You lost 10 life points!");
                        Stats[0] -= 10;
                        Console.WriteLine("Now you have {0} HP Points", Stats[0]);
                        Enter();
                        break;
                    case 2:
                        Console.WriteLine(Text3);
                        FirstBattle = false;
                        Enter();
                        break;
                }
            }
            ChooseArea();
        }

        private static void PrintStats(int[] Stats, string InputString)
        {
            Console.WriteLine("{0}" + Environment.NewLine, InputString);
            for (int i = 0; i < StatNames.Length; i++)
            {
                Console.WriteLine("{0,5}{1,8}", StatNames[i], Stats[i]);
            }
        }
        private static void ChoosingCharacter()
        {
            RacesMenu();
            int racesOption = ValidOption(1, 3, 2, 0, 0, 0, "0");//the last four inputs do not matter
            int chooseCharacter = 0;
            switch (racesOption)
            {
                case 1://Human
                    IntroHuman();
                    Console.WriteLine();
                    chooseCharacter = TypesMenu("Human", 1, 2, 3);
                    if (chooseCharacter == 4)//this allows the user to go back and choose his race again
                    {
                        Console.Clear();
                        ChoosingCharacter();
                    }
                    break;
                case 2://Lemurian
                    IntroLemur();
                    Console.WriteLine();
                    chooseCharacter = TypesMenu("Lemurian", 1, 3, 4);
                    if (chooseCharacter == 4)
                    {
                        Console.Clear();
                        ChoosingCharacter();
                    }
                    chooseCharacter += 3;//this way, if the first option was chosen, the chooseCharacter key will be 4, if the second option was chosen the hey will be 5, and so on
                    break;
                case 3://Dark Elf
                    IntroElf();
                    Console.WriteLine();
                    chooseCharacter = TypesMenu("Dark Elf", 4, 2, 5);
                    if (chooseCharacter == 4)
                    {
                        Console.Clear();
                        ChoosingCharacter();
                    }
                    break;
            }
        }//this method displays the user all the character options

        private static int TypesMenu(string race, int type1, int type2, int type3)//this method displays the 3 types that exist among a race so that the user may choose one of them
        {
            /*
             * 1 = Paladin
             * 2 = Wizard
             * 3 = Warrior
             * 4 = Rogue
             * 5 = Warlock
            */
            --type1;//since the menu option shows a position, but its actual position in the array is 1 less
            --type2;
            --type3;
            string[] types = new string[5] { "Paladin", "Wizard", "Warrior", "Rogue", "Warlock" };
            string[] weapons = new string[5] { "Sword and Shield", "Staff", "Two Handed Axe", "Dagger", "Rod and Shield" };
            bool Run = false;
            int chooseCharacter = 0;
            do
            {
                Run = false;
                Console.WriteLine("{0}" + Environment.NewLine + "{1}" + Environment.NewLine + "{2}" + Environment.NewLine + "{3}" + Environment.NewLine + "{4}" + Environment.NewLine + "{5}", "Now you have to choose what kind of " + race + " you want to be.", "You may be a:", "1." + types[type1], "2." + types[type2], "3." + types[type3], "4.Or you may Go Back");
                chooseCharacter = ValidOption(1, 4, 3, type1, type2, type3, race);
                switch (chooseCharacter)
                {
                    case 1:
                        if (race.ToLower() == "human")
                        {
                            PossibleCharacter = 1;
                        }
                        if (race.ToLower() == "lemurian")
                        {
                            PossibleCharacter = 4;
                        }
                        if (race.ToLower() == "dark elf")
                        {
                            PossibleCharacter = 7;
                        }
                        Console.WriteLine("A {0} uses a {1}.", types[type1], weapons[type1]);
                        Console.WriteLine();
                        PrintStats(LoadStats(PossibleCharacter), "You have the following Stats:");//check this later
                        Console.WriteLine();
                        Console.WriteLine("1.Do you want to be a {0} {1}?", race, types[type1]);
                        Console.WriteLine("2.Or do you wish to Go Back?");
                        int answer = ValidOption(1, 2, 4, type1, 0, 0, race);
                        if (answer == 2)
                        {
                            Console.Clear();
                            Run = true;
                        }
                        break;
                    case 2:
                        if (race.ToLower() == "human")
                        {
                            PossibleCharacter = 2;
                        }
                        if (race.ToLower() == "lemurian")
                        {
                            PossibleCharacter = 5;
                        }
                        if (race.ToLower() == "dark elf")
                        {
                            PossibleCharacter = 8;
                        }
                        Console.WriteLine("A {0} uses a {1}.", types[type2], weapons[type2]);
                        Console.WriteLine();
                        PrintStats(LoadStats(PossibleCharacter), "You have the following Stats:");
                        Console.WriteLine();
                        Console.WriteLine("1.Do you want to be a {0} {1}?", race, types[type2]);
                        Console.WriteLine("2.Or do you wish to Go Back?");
                        answer = ValidOption(1, 2, 4, type2, 0, 0, race);
                        if (answer == 2)
                        {
                            Console.Clear();
                            Run = true;
                        }
                        break;
                    case 3:
                        if (race.ToLower() == "human")
                        {
                            PossibleCharacter = 3;
                        }
                        if (race.ToLower() == "lemurian")
                        {
                            PossibleCharacter = 6;
                        }
                        if (race.ToLower() == "dark elf")
                        {
                            PossibleCharacter = 9;
                        }
                        Console.WriteLine();
                        PrintStats(LoadStats(PossibleCharacter), "You have the following Stats:");
                        Console.WriteLine();
                        Console.WriteLine("A {0} uses a {1}.", types[type3], weapons[type3]);
                        Console.WriteLine("1.Do you want to be a {0} {1}?", race, types[type3]);
                        Console.WriteLine("2.Or do you wish to Go Back?");
                        answer = ValidOption(1, 2, 4, type3, 0, 0, race);
                        if (answer == 2)
                        {
                            Console.Clear();
                            Run = true;
                        }
                        break;
                }
            } while (Run == true);
            return chooseCharacter;
        }

        private static int ValidOption(int LowerBoundary, int UpperBoundary, int menu, int type1, int type2, int type3, string race)
        {
            int option;
            bool validOption = int.TryParse(Console.ReadLine(), out option);
            string[] types = new string[5] { "Paladin", "Wizard", "Warrior", "Rogue", "Warlock" };
            if (validOption == false || option < LowerBoundary || option > UpperBoundary)//if the player chooses an option that does not belong to those of the menu (1, 2 or 3) the game will prompt the player to try again
            {
                do
                {
                    Console.WriteLine("You chose an invalid function from the menu." + Environment.NewLine + "Please press ENTER and try again.");
                    Enter();
                    switch (menu)
                    {
                        case 1:
                            InitialMenu();
                            break;
                        case 2:
                            RacesMenu();
                            break;
                        case 3:
                            Console.WriteLine("{0}" + Environment.NewLine + "{1}" + Environment.NewLine + "{2}" + Environment.NewLine + "{3}" + Environment.NewLine + "{4}", "Now you have to choose what kind of " + race + " you want to be.", "You may be a:", "1." + types[type1], "2." + types[type2], "3." + types[type3]);
                            break;
                        case 4:
                            Console.WriteLine("type1 = " + type1);
                            Console.ReadLine();
                            Console.WriteLine("1.Do you want to be a {0} {1}?", race, types[type1]);
                            Console.WriteLine("2.Or do you wish to Go Back?");
                            break;
                        case 5://Fisrt battle
                            Console.WriteLine("1.Try to flee \n2.Attack");
                            break;
                        case 6:
                            Console.WriteLine("What do you wish to do first? \n1.Upgrade one skill \n2.Use your Stat Points");
                            break;
                        case 7:
                            Console.WriteLine("1.Walk forward \n.2.Turn left \n3.Turn right \n4.Turn around");
                            break;
                    }
                    validOption = int.TryParse(Console.ReadLine(), out option);
                } while (validOption == false || option < LowerBoundary || option > UpperBoundary);//the game will ask the player to choose a valid option from the game until he does
            }
            return option;
        }
        private static void RacesMenu()//Switch #2
        {
            Console.WriteLine("Please choose your race:" + Environment.NewLine + "1.Human" + Environment.NewLine + "2.Lemurian" + Environment.NewLine + "3.Dark Elf");
        }
        private static void Enter()
        {
            Console.ReadLine();
            Console.Clear();
        }
        private static void InitialMenu()//Switch #1
        {
            Console.WriteLine("{0}" + Environment.NewLine + "{1}" + Environment.NewLine + "{2}", "1.Start New Game", "2.Load Game", "3.Quit Game");
        }
        static int[] LoadStats(int chooseCharacter)
        {
            //Every time the player chooses to start a new game, he will be prompted to choose a character type. Depending on what he chooses, one of the following stats will be loaded.
            //Arrays 7 and 8 are exp and lvl respectively

            //HUMANS
            int[] HumanPaladin = new int[11] { 275, 60, 0, 0, 30, 45, 0, 0, 1, 1, 1 };
            int[] HumanWizard = new int[11] { 120, 80, 75, 5, 12, 0, 0, 0, 1, 1, 2 };
            int[] HumanWarrior = new int[11] { 280, 0, 0, 0, 50, 30, 0, 0, 1, 1, 3 };

            //LEMURIANS
            int[] LemurianPaladin = new int[11] { 250, 60, 0, 0, 30, 40, 5, 0, 1, 2, 1 };
            int[] LemurianRogue = new int[11] { 90, 0, 0, 0, 70, 20, 5, 0, 1, 2, 4 };
            int[] LemurianWarrior = new int[11] { 250, 0, 0, 0, 47, 30, 5, 0, 1, 2, 3 };

            //DARK ELVES
            int[] DarkElfWarlock = new int[11] { 140, 0, 70, 8, 0, 20, 0, 0, 1, 3, 5 };
            int[] DarkElfWizard = new int[11] { 100, 80, 90, 7, 0, 0, 0, 0, 1, 3, 2 };
            int[] DarkElfRogue = new int[11] { 100, 0, 0, 0, 75, 20, 0, 0, 1, 3, 4 };

            int[] NonValid = new int[11];

            switch (chooseCharacter)
            {
                case 1:
                    return HumanPaladin;
                case 2:
                    return HumanWizard;
                case 3:
                    return HumanWarrior;
                case 4:
                    return LemurianPaladin;
                case 5:
                    return LemurianWarrior;
                case 6:
                    return LemurianRogue;
                case 7:
                    return DarkElfRogue;
                case 8:
                    return DarkElfWizard;
                case 9:
                    return DarkElfWarlock;
                default:
                    return NonValid;
            }
        }
        static string SetUserName()
        {
            bool rightUsername = false;
            string userName = string.Empty;//initializes the variable userName
            do
            {
                Console.Write("Please choose a Username and press ENTER: ");
                userName = Console.ReadLine();
                if (userName != "")//this is to avoid having blank usernames
                {
                    rightUsername = true;
                }
                else
                {
                    Console.WriteLine("You did not choose a Username." + Environment.NewLine + "Please press ENTER and try Again");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (rightUsername == false);
            return userName;
        }
        static string SetUserPassword()
        {
            bool rightPassword = false;
            bool noSpaces = true;
            string userPassword = string.Empty;
            string sub = string.Empty;
            do
            {
                do
                {
                    Console.Clear();
                    noSpaces = true;
                    Console.Write("Please choose a password: ");
                    userPassword = Console.ReadLine();
                    for (int i = 0; i < userPassword.Length; i++)
                    {
                        sub = userPassword.Substring(i, 1);
                        if (sub == " ")
                        {
                            Console.WriteLine("You may not leave blank spaces in your password. \nPlease try again.");
                            noSpaces = false;
                        }
                    }
                } while (noSpaces == false);
                Console.Write("Please verify your password: ");
                if (Console.ReadLine() == userPassword)
                {
                    rightPassword = true;
                }
                else
                {
                    Console.WriteLine("Your passwords did not match." + Environment.NewLine + "Please try again");
                    Enter();
                }
            } while (rightPassword == false);
            return userPassword;
        }
        static void IntroHuman()
        {
            Console.Clear();
            Console.WriteLine("The Humans, a race of warriors that had its originis in the mountains and fields of Zeme." + Environment.NewLine + "When compared to Lemurians or Elves one can notice the great differences;" + Environment.NewLine + "the humans, a fairly new race with only a couple thousands of years has evolved from cavemen,to nomads, to farmers and to the fine race that they are now," + Environment.NewLine + "but not with great difficulties, since their very start the humans have been" + Environment.NewLine + "prosecuted and enslaved." + Environment.NewLine + "With nothing but will and fate this race has stepped up." + Environment.NewLine + "And through the mastering of the fate and magic they have build an empire." + Environment.NewLine + "Now taking advantage of the debility of, once their former enslavers , both lemurians and dark elves; the humans are now on a blood hunt," + Environment.NewLine + "both to show their power and to expand their empire." + Environment.NewLine + "Are you prepared to take the judgement of the humans to this formidable enemies?" + Environment.NewLine + "Do you have what it takes to lead them to victory?" + Environment.NewLine + "Choose your path as a master magician, paladin or a blood lust warrior and fight for the dominance of Zeme.");
        }
        static void IntroLemur()
        {
            Console.Clear();
            Console.WriteLine("The Lemurians, a lizard race that flourish in the  deepness of the jungles" + Environment.NewLine + "of Zeme, masters in combat and gambling, they are race of formidablle strenght." + Environment.NewLine + "But the deepness of the jungle wasn´t always their home, millenia ago," + Environment.NewLine + "before the humans had even steped on Zeme." + Environment.NewLine + "The Lemurians shared the earth with the Elves having the seas for themselves and entrusting not only their future but also their golden city in the oceans Dinas Aur on Marmadeus, The God of Tempest, Oceans and Strenght." + Environment.NewLine + "But one fateful day a tempest fell upon Dinas Aur, many lives where lost and the history and knowledge sank with the city, many blame King Lamdan the IV" + Environment.NewLine + "for bringing the rage of the gods upon them." + Environment.NewLine + "You, as the heir to the throne, have a desicion to make, should you stay and defend the new home of your people against the attacks of the humans in their searh for revenge?" + Environment.NewLine + "To succeed and secure your people or to lose and face slavery?" + Environment.NewLine + "Should you clean you ancestors name and find out what truly happened millenia ago?" + Environment.NewLine + "Should you find the safety of the oceans in Dinas Aur" + Environment.NewLine + "or should you die by the rage of the Tempest God?" + Environment.NewLine + "Many roads to follow, only one shall take you to eternal glory.");
        }
        static void IntroElf()
        {
            Console.Clear();
            Console.WriteLine("The Dark Elves unable to live on the above, thanks to a curse that causes the sun to fatally wound them, have seek refugee in the deepness of the earth, built their great monuments and cities in the hearts of mountains and the deepness of caves. But it wasn't like this to the elves since the beginning of time, thousands of years ago they shared the earth with the Lemurians.");
            Console.WriteLine("Revered as the master race, the elves where master magicians and had total control over nature and the ground around them, as partners in commerce with the Lemurians, both civilizations prospered greatly,  until both the Lemurians and the Elves wanted more, this lead to a great war for expansion, in the quest to find great power to defeat their foes, the elves found a rock of great power deep in the heart of a mountain, it was named Tumsa.");
            Console.WriteLine("Tumsa was used to develop new magic and ways to control the nature and the very fabric of reality,  with this new power in hand the King of the Elves, Karalis lead to  crushing victories");
            Console.WriteLine("But to what cost? A deep darkness covered the elves and forbidden to withstand the power of the sun, they where forced to leave the above and take refugee in the darkness, that which had gave them victory, took that and more from them.");
            Console.WriteLine("It is in your hands to take the master race above ground, shall you erase the curse, or bring eternal darkness to Zeme, it is all in your hands.");

        }

        //DICE METHOD
        static int Dice(int lowerb, int upperb)
        {
            Random r = new Random();
            return r.Next(lowerb, upperb + 1);
        }
        static void printdice(int lowerb, int upperb)
        {
            Console.WriteLine("{0} throws {1}", "userName", Dice(lowerb, upperb));
        }

        //FIGHT METHOD
        static int[] fight()
        {
            Console.Clear();
            int[] FightStats = new int[7] { Stats[0], Stats[1], Stats[2], Stats[3], Stats[4], Stats[5], Stats[6] };//this is a global variable

            int dañomon, dañoper;
            int mobdice, playerdice, option;
            int[] monster;
            string[] monstername;
            string firstprint;
            firstprint = "A {0} has appeared in front of you, what do you wish to do?";
            bool valid = false, fight = true;
            bool IsItABoss = false;
            monster = mobs();
            monstername = namelibrary(monster);
            Random r = new Random();
            int choice = r.Next(2, 4);


            int indiceAttack = 4;
            int indiceDefence = 5;
            if (MagicalMonster)
            {
                indiceAttack = 2;
                indiceDefence = 3;
            }

            if (fightcounter == 5)
            {
                IsItABoss = true;
                monster = miniboss();
                monstername = bosslibrary(monster);
                firstprint = "{0} has appeared in front of you, what do you wish to do?";
                fightcounter = 0;
            }


            Console.WriteLine(firstprint, monstername[0]);
            Console.WriteLine(monstername[choice]);
            PrintMyCurrentStats();
            Console.ReadLine();

            while (fight)
            {
                Console.Clear();
                Console.WriteLine("1.Try to flee");
                Console.WriteLine("2.Attack");
                if (ability1 > 0 && ability2 > 0)
                {
                    Console.WriteLine("3.{0}", myskills[0]);
                    Console.WriteLine("4.{1}", myskills[1]);
                }
                if (ability1 > 0 && ability2 == 0)
                {
                    Console.WriteLine("3.{0}", myskills[0]);
                }
                if (ability1 == 0 && ability2 > 0)
                {
                    Console.WriteLine("4.{0}", myskills[1]);
                }
                valid = int.TryParse(Console.ReadLine(), out option);
                if (valid)
                {
                    switch (option)
                    {
                        case 1: //flee
                            mobdice = Dice(luck(monster), 20);
                            PrintMyCurrentStats();
                            Console.WriteLine("{0} has thrown {1}", monstername[0], mobdice);
                            Console.WriteLine("Press ENTER when you want to throw your dice...");
                            Console.ReadLine();
                            playerdice = Dice(luck(Stats), 20);
                            Console.WriteLine("{0}, you have thrown {1}", userName, playerdice);
                            if (IsItABoss)
                            {
                                Console.WriteLine("You cannot flee from a bosses wrath");
                                Enter();
                            }

                            else
                            {
                                if (playerdice > mobdice)
                                {
                                    Console.WriteLine("You have managed to escape.");
                                    Enter();
                                    fight = false;
                                }
                                else
                                {
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);//Fight Stats isn't changing 
                                    FightStats[0] = FightStats[0] - dañoper;

                                    PrintMyCurrentStats();
                                    Console.WriteLine("You have failed to escape");
                                    Enter();
                                    PrintMyCurrentStats();
                                    Console.WriteLine(monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Enter();
                                }
                                valid = false;
                            }
                            break;

                        case 2: //normal attack
                            switch (Stats[10])
                            {
                                case 1:
                                    dañomon = substractfromattack(monster[5], FightStats[4]);
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    monster[0] = monster[0] - dañomon;
                                    FightStats[0] = FightStats[0] - dañoper;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;

                                case 2:
                                    dañomon = substractfromattack(monster[3], FightStats[2]);
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    monster[0] = monster[0] - dañomon;
                                    FightStats[0] = FightStats[0] - dañoper;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;

                                case 3:
                                    dañomon = substractfromattack(monster[5], FightStats[4]);
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    monster[0] = monster[0] - dañomon;
                                    FightStats[0] = FightStats[0] - dañoper;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                                case 4:
                                    dañomon = substractfromattack(monster[5], FightStats[4]);
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    monster[0] = monster[0] - dañomon;
                                    FightStats[0] = FightStats[0] - dañoper;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} Hp", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                                case 5:
                                    dañomon = substractfromattack(monster[3], FightStats[2]);
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    monster[0] = monster[0] - dañomon;
                                    FightStats[0] = FightStats[0] - dañoper;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} Hp", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                            }
                            valid = false;
                            break;

                        case 3: //ability1
                            switch (Stats[10])
                            {
                                case 1:
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);
                                    abilityeffects(3);
                                    FightStats[0] = FightStats[0] - dañoper;//Shouldn't this be down?
                                    PrintMyCurrentStats();
                                    Console.WriteLine("You have used {0}, receiving a boost in your magic resist and armour", myskills[0]);
                                    PrintStats(Stats, "Now you have the following Stats:");//PERHAPS WE COULD TAKE THIS OUT
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Enter();
                                    break;
                                case 2:
                                    dañomon = substractfromattack(monster[3], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);
                                    abilityeffects(3);
                                    monster[0] = monster[0] - dañomon;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("You have used {0}", myskills[0]);
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                                case 3:
                                    dañomon = substractfromattack(monster[5], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);
                                    abilityeffects(3);
                                    monster[0] = monster[0] - dañomon;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("You have used {0}", myskills[0]);
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} Hp", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                                case 4:
                                    dañomon = substractfromattack(monster[5], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);
                                    abilityeffects(3);
                                    monster[0] = monster[0] - dañomon;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("You have used {0}", myskills[0]);
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;//Shouldn't this be up?
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                                case 5:
                                    dañomon = substractfromattack(monster[5], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);
                                    abilityeffects(3);
                                    monster[0] = monster[0] - dañomon;
                                    PrintMyCurrentStats();
                                    Console.WriteLine("You have used {0}", myskills[0]);
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                            }
                            valid = false;
                            break;

                        case 4: //ability2
                            switch (Stats[10])
                            {
                                case 1:
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    Console.WriteLine("You have used {0}, healing your HP and damaging your oponent", myskills[1]);
                                    abilityeffects(4);
                                    monster[0] = monster[0] - abilitydmg2();
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], abilitydmg2());
                                    Console.WriteLine("You have healed {0}", abilitydmg2());
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;

                                case 2:
                                    dañomon = substractfromattack(monster[3], abilitydmg2());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    Console.WriteLine("You have used {0}, damaging your oponent and receiving a boost in armor", myskills[1]);
                                    abilityeffects(4);
                                    monster[0] = monster[0] - dañomon;
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;

                                case 3:
                                    dañomon = substractfromattack(monster[5], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    Console.WriteLine("You have used {0}, healing your HP and damaging the oponnent", myskills[1]);
                                    abilityeffects(4);
                                    monster[0] = monster[0] - dañomon;
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;

                                case 4:
                                    dañomon = substractfromattack(monster[5], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    Console.WriteLine("You have used {0}", myskills[1]);
                                    abilityeffects(4);
                                    monster[0] = monster[0] - dañomon;
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                                case 5:
                                    dañomon = substractfromattack(monster[3], abilitydmg1());
                                    dañoper = substractfromattack(FightStats[indiceDefence], monster[indiceAttack]);

                                    Console.WriteLine("You have used {0}", myskills[1]);
                                    abilityeffects(4);
                                    monster[0] = monster[0] - dañomon;
                                    Console.WriteLine("{0} has received {1} dmg", monstername[0], dañomon);
                                    FightStats[0] = FightStats[0] - dañoper;
                                    Console.WriteLine("{0} {1}", monstername[0], monstername[1]);
                                    Console.WriteLine("You have received {0} dmg", dañoper);
                                    Console.WriteLine("Now you have {0} HP", FightStats[0]);
                                    Console.WriteLine("{0} now has {1} HP", monstername[0], monster[0]);
                                    Enter();
                                    break;
                            }
                            valid = false;
                            break;

                    }

                    if (monster[0] <= 0)
                    {
                        Console.WriteLine("You have defeated the {0}", monstername[0]);
                        Stats[7] += 10;  //10 is just for the moment, need to discuss
                        fight = false;
                        Enter();
                    }
                    else if (FightStats[0] <= 0)
                    {
                        Console.WriteLine("You have failed to defeat the minion");
                        fight = false;
                        Enter();
                    }

                }
                else
                {
                    Console.WriteLine("Not a valid option, I am sorry");
                    Console.ReadLine();
                }
            }
            fightcounter++;
            MagicalMonster = false;
            return lvlup();
        }

        //LUCK METHOD FOR DICE
        static int luck(int[] array)
        {
            return 1 + (array[6] / 20);
        }

        //Lvl Up Method
        static int[] lvlup()
        {
            string[] possibleskills = new string[2];
            string[] yourabilities = new string[6];
            int menuinput, rank1 = 0, rank2 = 0;
            string skilldecision = string.Empty;
            bool valid = true;
            yourabilities = abilities();
            //possible skills arranger
            if (ability1 >= 0 & ability1 <= 2)
            {
                switch (ability2)
                {
                    case 0:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 1:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 2:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 3:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 4:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 5:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 6:
                        possibleskills = new string[2] { yourabilities[0], yourabilities[5] };
                        rank2 = 3;
                        break;
                    default:
                        if (ability2 > 6)
                        {
                            possibleskills = new string[2] { yourabilities[0], yourabilities[5] };
                            rank2 = 3;
                        }
                        break;
                }
                rank1 = 1;
            }
            else if (ability1 >= 3 & ability1 <= 5)
            {
                switch (ability2)
                {
                    case 0:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 1:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 2:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 3:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 4:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 5:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 6:
                        possibleskills = new string[2] { yourabilities[1], yourabilities[5] };
                        rank2 = 3;
                        break;
                    default:
                        if (ability2 > 6)
                        {
                            possibleskills = new string[2] { yourabilities[1], yourabilities[5] };
                            rank2 = 3;
                        }
                        break;
                }
                rank1 = 2;
            }
            else if (ability1 >= 6)
            {
                switch (ability2)
                {
                    case 0:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 1:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 2:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[3] };
                        rank2 = 1;
                        break;
                    case 3:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 4:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 5:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[4] };
                        rank2 = 2;
                        break;
                    case 6:
                        possibleskills = new string[2] { yourabilities[2], yourabilities[5] };
                        rank2 = 3;
                        break;
                    default:
                        if (ability2 > 6)
                        {
                            possibleskills = new string[2] { yourabilities[2], yourabilities[5] };
                            rank2 = 3;
                        }
                        break;
                }
                rank1 = 3;
            }
            if (Stats[7] == 100)
            {
                int totalskillpoints = 1;
                Stats[8]++;
                Stats[7] = 0;
                Console.WriteLine("It's your birthday! \nYou are now lvl {0} \nYou have been granted 1 Skill Point and 10 Stat Points \nYou can use it in one of the next abilities", Stats[8]);
                Console.WriteLine("What do you wish to do first? \n1.Upgrade one skill \n2.Use your Stat Points");
                int Upgrade = ValidOption(1, 2, 6, 0, 0, 0, "0");
                switch (Upgrade)
                {
                    case 1://UPGRADE ONE SKILL THEN USE YOUR STAT POINTS
                        menuinput = UpgradeSkill(possibleskills, rank1, rank2, ref skilldecision, ref valid, ref totalskillpoints);
                        UseStatPoints(0, 10, 0);
                        break;
                    case 2://USE YOUR STAT POINTS THEN UPGRADE ONE SKILL
                        UseStatPoints(0, 10, 0);
                        menuinput = UpgradeSkill(possibleskills, rank1, rank2, ref skilldecision, ref valid, ref totalskillpoints);
                        break;
                }
            }
            return Stats;
        }

        private static int UpgradeSkill(string[] possibleskills, int rank1, int rank2, ref string skilldecision, ref bool valid, ref int totalskillpoints)
        {
            int menuinput = 0;
            while (valid)
            {
                Console.Clear();
                Console.WriteLine("Remember that each ability has 3 ranks. To achieve the next rank you have to lvl each skill 3 times");
                Console.WriteLine("Abilities do recieve a boost each lvl up before reaching the next rank (20% of the difference between that rank and the next). So there's a 40% damage boost between a max lvled rank 1 skill and a lvl 1 ranked 2 skill");
                Console.WriteLine("1.{0} {1}/9 \n2.{2} {3}/9", possibleskills[0], ability1, possibleskills[1], ability2);
                valid = int.TryParse(Console.ReadLine(), out menuinput);
                //Console.ReadLine();
                if (valid)
                {
                    switch (menuinput)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine(possibleskills[0]); //ability 1
                            switch (Stats[10])
                            {
                                case 1:
                                    Console.WriteLine("This ability gives you magic resist and armor depending on your hp: \n7% of max hp for 3 turns at rank 1 \n10% of max hp for 5 turns at rank 2 \n15% of max hp for 7 turns at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability1 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank1);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.WriteLine("This ability launches a fireball at the oponent inflicting magical damage and burning him for a period of time: \n160% of your magical damage and 5% of the damage dealt is inflicted as burn for 5 seconds at rank 1 \n180% of your magical damage and 7% of the damage dealt is inflicted as burn for 7 seconds at rank 2 \n200% of your magical damage and 10% of the damage dealt is inflicted as burn for 10 seconds at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability1 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank1);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 3:
                                    Console.WriteLine("This ability costs a % of your total life giving you in return a boost in attack damage for a number of turns: \n20% of your hp for 5% more attack damage for 3 turns at rank 3 \n25% of your hp for 15% more attack damage for 5 turns at rank 2 \n30% of your hp for 25% more attack damage for 6 turns at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability1 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank1);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 4:
                                    Console.WriteLine("This ability deals a % of your physical damage as posion damage(magical damage) to your oponent for a number of turns: \n20% of your attack damage for 3 turns at rank 1 \n25% of your attack damage for 5 turns at rank 2 \n35% of your attack damage for 7 turns at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability1 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank1);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 5:
                                    Console.WriteLine("This ability deals a % of your magical damage to your oponent for a number of turns (DoT): \n150% of your magical damage for 5 turns at rank 1 \n175% of your magical damage for 5 turns at rank 2 \n250% of your magical damage for 5 turns at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability1 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank1);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                            }
                            if (skilldecision.ToLower() == "y")
                            {
                                Console.Clear();
                                totalskillpoints--;
                                ability1++;
                                Console.WriteLine("You have added 1 skill point to {0}", possibleskills[0]);
                                switch (ability1)
                                {
                                    case 3:
                                        Console.WriteLine("Congratulations this ability is now rank 2");
                                        break;
                                    case 6:
                                        Console.WriteLine("Congratulations this ability is now rank 3");
                                        break;
                                    default:
                                        break;
                                }
                                valid = false;
                            }
                            else
                            {
                                UpgradeSkill(possibleskills, rank1, rank2, ref skilldecision, ref valid, ref totalskillpoints);
                            }
                            break;
                        case 2:
                            Console.WriteLine(possibleskills[1]); //ability2
                            switch (Stats[10])
                            {
                                case 1:
                                    Console.WriteLine("This ability heals a % of your maximum health and deals that as damage to your oponent: \nHeals and deals 10% of your max hp at rank 1 \nHeals and deals 20% of your max hp at rank 2 \nHeals and deals 30% of your max hp at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability2 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank2);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.WriteLine("This ability hits the enemy with an ice projectile and gives you an armor bonus: \n145% of your magical damage and a 20% bonus armor for 3 turns at rank 1 \n165% of your magical damage and a 25% bonus armor for 3 turns at rank 2 \n180% of your magical damage and a 30% bonus armor for 3 turns at rank 3");
                                    Console.WriteLine();
                                    Console.WriteLine("Currently " + ability2 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank2);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 3:
                                    Console.WriteLine("This ability heals hp and deals decreased damage to your oponent \n60% of your attack damage and heals 15% of your hp at rank 1 \n70% of your attack damage and heals 20% of your hp at rank 2 \n75% of your attack damage and heals 30% of your hp at rank 3");
                                    Console.WriteLine("Currently " + ability2 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank2);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 4:
                                    Console.WriteLine("This ability launches an attack from the shadows to your enemy: \n 180% of your attack damage at rank 1 \n 200% of your attack damage at rank 2 \n 250% of your attack damage at rank 3 ");
                                    Console.WriteLine("Currently " + ability2 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank2);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                                case 5:
                                    Console.WriteLine("This ability deals magical damage and heals HP equal to the damage: \n75% of your magical damage at rank 1 \n95% of your magical damage at rank  \n120% of your magical damage at rank 3");
                                    Console.WriteLine("Currently " + ability2 + "/9 points");
                                    Console.WriteLine("Currently at rank {0}/3", rank2);
                                    Console.WriteLine("Would you like to put one point into ability 1? (Y/N)");
                                    skilldecision = Console.ReadLine();
                                    break;
                            }
                            if (skilldecision.ToLower() == "y")
                            {
                                totalskillpoints--;
                                ability1++;
                                Console.WriteLine("You have added 1 skill point to {0}", possibleskills[0]);
                                switch (ability1)
                                {
                                    case 3:
                                        Console.WriteLine("Congratulations this ability is now rank 2");
                                        break;
                                    case 6:
                                        Console.WriteLine("Congratulations this ability is now rank 3");
                                        break;
                                    default:
                                        break;
                                }
                                valid = false;
                            }
                            break;
                    }
                }
                else
                {
                    UpgradeSkill(possibleskills, rank1, rank2, ref skilldecision, ref valid, ref totalskillpoints);
                }
            }
            myskills = new string[2] { possibleskills[0], possibleskills[1] };
            return menuinput;
        }
        private static void UseStatPoints(int FirstStat, int TotalStatPoints, int AmmountAlreadyAdded)
        {
            PrintStats(Stats, "Currently, you have the following Stats:");
            Console.WriteLine("You may add your 10 Stat Points to a single Stat or distribute them among many Stats.");
            Console.ReadLine();
            int AddStats, i = FirstStat;
            do
            {
                Console.Write("How many Stat Points do you want to add to {0}: ", StatNames[i]);
                int.TryParse(Console.ReadLine(), out AddStats);
                if (AddStats > TotalStatPoints)
                {
                    Console.WriteLine("You can not add this ammount of Stat Points. \nPlease try again.");
                    Console.ReadLine();
                    Console.Clear();
                    UseStatPoints(i, TotalStatPoints, AmmountAlreadyAdded);
                    break;
                }
                if (AddStats <= 0)
                {
                    Stats[i] += 0;
                    AmmountAlreadyAdded += 0;
                    TotalStatPoints -= 0;
                    ++i;
                }
                else
                {
                    Stats[i] += AddStats;
                    AmmountAlreadyAdded += AddStats;
                    TotalStatPoints -= AddStats;
                    ++i;
                }
            } while (i <= 6);
            if (AmmountAlreadyAdded < 10)//In case the user didn't use all his Stat Points, this "if" avoids the user to continue without using said points
            {
                Console.WriteLine("You haven't used all your Stat Points yet. \nYou have {0} Points left.", TotalStatPoints);
                Console.WriteLine("Please press any key in order to use your remaining Stat Points.");
                Enter();
                UseStatPoints(0, TotalStatPoints, AmmountAlreadyAdded);//Verify that this works
            }
            PrintStats(Stats, "Now you have the following Stats:");
            Enter();
        }

        //MONSTER LIBRARY
        static int[] mobs()
        {
            Random r = new Random();
            int[] monster = new int[8];
            switch (Area)
            {
                case 1: //Nightmare Jungle
                    switch (actualroute)
                    {
                        case 1:
                            //giantlizard
                            monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 30 * Stats[8], 15 * Stats[8], 1 * Stats[8], 9 };
                            break;

                        case 2:
                            //Cat
                            monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 20 * Stats[8], 1 * Stats[8], 16 };
                            break;
                        case 3:
                            //quara
                            monster = new int[8] { 80 * Stats[8], 0, 0, 20 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 11 };
                            break;
                        default:
                            switch (r.Next(1, 8))
                            {
                                case 1://monkey
                                    monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 10 * Stats[8], 1 * Stats[8], 6 };
                                    break;

                                case 2://cow
                                    monster = new int[8] { 250 * Stats[8], 0, 0, 40 * Stats[8], 1 * Stats[8], 60 * Stats[8], 1 * Stats[8], 7 };
                                    break;

                                case 3://kamikaze
                                    monster = new int[8] { 5 * Stats[8], 0, 60 * Stats[8], 2 * Stats[8], 0, 2 * Stats[8], 10 * Stats[8], 8 };
                                    MagicalMonster = true;
                                    break;

                                case 4://behemoth
                                    monster = new int[8] { 180 * Stats[8], 0, 0, 20 * Stats[8], 35 * Stats[8], 20 * Stats[8], 1 * Stats[8], 10 };
                                    break;

                                case 5://quara
                                    monster = new int[8] { 80 * Stats[8], 0, 0, 20 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 11 };
                                    break;

                                case 6://moboi
                                    monster = new int[8] { 80 * Stats[8], 0, 0, 30 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 12 };
                                    break;

                                case 7://chaki
                                    monster = new int[8] { 100 * Stats[8], 0, 0, 10 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 13 };
                                    break;
                            }
                            break;
                    }
                    break;
                case 2: //Molten Mountains
                    switch (actualroute)
                    {
                        case 1://troll
                            monster = new int[8] { 180 * Stats[8], 0, 0, 20 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 23 };
                            break;
                        case 4://cyclops
                            monster = new int[8] { 160 * Stats[8], 0, 0, 20 * Stats[8], 35 * Stats[8], 18 * Stats[8], 1 * Stats[8], 24 };
                            break;
                        case 5://mother earthquake
                            monster = new int[8] { 100 * Stats[8], 0, 25 * Stats[8], 20 * Stats[8], 0, 10 * Stats[8], 0, 25 };
                            MagicalMonster = true;
                            break;
                        default:
                            switch (r.Next(1, 8))
                            {
                                case 1://drago
                                    monster = new int[8] { 90 * Stats[8], 0, 30 * Stats[8], 30 * Stats[8], 0 * Stats[8], 35 * Stats[8], 10 * Stats[8], 3 };
                                    MagicalMonster = true;
                                    break;

                                case 2://mage
                                    monster = new int[8] { 75 * Stats[8], 0, 25 * Stats[8], 20 * Stats[8], 0, 25 * Stats[8], 0, 4 };
                                    MagicalMonster = true;
                                    break;

                                case 3://witch
                                    monster = new int[8] { 100 * Stats[8], 0, 25 * Stats[8], 20 * Stats[8], 0, 10 * Stats[8], 0, 5 };
                                    MagicalMonster = true;
                                    break;

                                case 4://shaman
                                    monster = new int[8] { 70 * Stats[8], 0, 30 * Stats[8], 40 * Stats[8], 0, 18 * Stats[8], 1 * Stats[8], 14 };
                                    MagicalMonster = true;
                                    break;

                                case 5://behemoth
                                    monster = new int[8] { 180 * Stats[8], 0, 0, 20 * Stats[8], 35 * Stats[8], 20 * Stats[8], 1 * Stats[8], 10 };
                                    break;
                            }
                            break;
                    }
                    break;
                case 3: //Death Desert
                    switch (actualroute)
                    {
                        case 1://crocodile
                            monster = new int[8] { 100 * Stats[8], 0, 0, 10 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 19 };
                            break;
                        case 2://bandits
                            monster = new int[8] { 80 * Stats[8], 0, 0, 30 * Stats[8], 28 * Stats[8], 18 * Stats[8], 1 * Stats[8], 20 };
                            break;
                        case 3://Stone Guards
                            monster = new int[8] { 180 * Stats[8], 0, 0, 20 * Stats[8], 35 * Stats[8], 20 * Stats[8], 1 * Stats[8], 21 };
                            break;
                        case 4://Worm 
                            monster = new int[8] { 250 * Stats[8], 0, 0, 40 * Stats[8], 20 * Stats[8], 60 * Stats[8], 1 * Stats[8], 22 };
                            break;
                        default:
                            switch (r.Next(1, 8))
                            {
                                case 1://mangyang
                                    monster = new int[8] { 5 * Stats[8], 0, 60 * Stats[8], 2 * Stats[8], 0, 2 * Stats[8], 10 * Stats[8], 15 };
                                    MagicalMonster = true;
                                    break;

                                case 2://cow
                                    monster = new int[8] { 250 * Stats[8], 0, 0, 40 * Stats[8], 1 * Stats[8], 60 * Stats[8], 1 * Stats[8], 7 };
                                    break;

                                case 3://kamikaze
                                    monster = new int[8] { 5 * Stats[8], 0, 60 * Stats[8], 2 * Stats[8], 0, 2 * Stats[8], 10 * Stats[8], 8 };
                                    MagicalMonster = true;
                                    break;

                                case 4://behemoth
                                    monster = new int[8] { 180 * Stats[8], 0, 0, 20 * Stats[8], 35 * Stats[8], 20 * Stats[8], 1 * Stats[8], 10 };
                                    break;
                            }
                            break;
                    }
                    break;
                case 4: //City of the Death
                    switch (actualroute)
                    {
                        case 1://skeletons
                            monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 20 * Stats[8], 1 * Stats[8], 1 };
                            break;
                        case 2://dogs
                            monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 20 * Stats[8], 1 * Stats[8], 17 };
                            break;
                        case 3://skeleton
                            monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 20 * Stats[8], 1 * Stats[8], 1 };
                            break;
                        case 4://gargoyle
                            monster = new int[8] { 130 * Stats[8], 0, 0, 18 * Stats[8], 30 * Stats[8], 30 * Stats[8], 5 * Stats[8], 18 };
                            break;
                        case 5://skeleton
                            monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 20 * Stats[8], 1 * Stats[8], 1 };
                            break;
                        default:
                            switch (r.Next(1, 8))
                            {
                                case 1://skeleton
                                    monster = new int[8] { 100 * Stats[8], 0, 0, 20 * Stats[8], 25 * Stats[8], 20 * Stats[8], 1 * Stats[8], 1 };
                                    break;

                                case 2://zombie
                                    monster = new int[8] { 130 * Stats[8], 0, 0, 18 * Stats[8], 30 * Stats[8], 30 * Stats[8], 5 * Stats[8], 2 };
                                    break;
                            }
                            break;
                    }
                    break;
            }
            return monster;
        }

        //Bosses Library
        static int[] miniboss()
        {
            Random r = new Random();
            int bossnumber = r.Next(1, 5);
            int[] Boss = new int[8];
            bool IsRepeating = false;

            for (int i = 0; i < 4; i++)
            {
                if (bossnumber == repeat[i])
                {
                    IsRepeating = true;
                }
            }
            if (IsRepeating == false)
            {
                switch (bossnumber)
                {
                    case 1: //Zartar
                        repeat[BossCounter] = 1;
                        Boss = new int[8] { 200 * Stats[8], 150, 80, 40, 0, 50, 10, 1 };
                        MagicalMonster = true;
                        break;

                    case 2: //Quilios
                        repeat[BossCounter] = 2;
                        Boss = new int[8] { 300 * Stats[8], 100, 75, 55, 0, 40, 5, 2 };
                        MagicalMonster = true;
                        break;

                    case 3: //Vilogas
                        repeat[BossCounter] = 3;
                        Boss = new int[8] { 150 * Stats[8], 0, 0, 40, 80, 20, 35, 3 };
                        break;

                    case 4: //Moarte
                        repeat[BossCounter] = 4;
                        Boss = new int[8] { 250 * Stats[8], 50, 0, 25, 40, 35, 50, 4 };
                        break;
                }
                BossCounter++;
            }
            else
            {
                miniboss();
            }
            return Boss;
        }

        //Print bosses library
        static string[] bosslibrary(int[] array)
        {
            string[] printboss = new string[5];
            switch (array[7])
            {
                case 1:
                    printboss = new string[5] { "Zartar", "Has brought upon you the wrath of his ancestors", "Who dafuq are you who dare interrupt my slumber? You shall be punished!", "Who dafuq do you think you are slaying my slaves and killing my minions, you thought I would do nothing? HA", "You insolent insect, you shall die by my hand" };
                    break;
                case 2:
                    printboss = new string[5] { "Quilios", "Has brought the fury of the ocean upon you", "WHO DARES TO TAKE A SINGLE STEP INTO MY PALACE?! YOU SHALL NEVER GET OUT!", "I have been feared by countless generations before yours little insect, you are not even worthy of my foot", "Nightmares have nightmares of me lil trash, you understimate the power of a God?! HAhaHAHhahAHhahah" };
                    break;
                case 3:
                    printboss = new string[5] { "Vilogas", "Has shaken the earth with his all mighty strenght", "Have you said your goodbyes? He who has seen me has never lived to tell ", "I remember trillions of years ago when the universe was nothing more than blackness, you monkeys had to destroy my tranquility", "So greed and lust for power have brought you here huh, those will be the sins that destroy your race, your planet, everything, you have awaken me and you shall now perish" };
                    break;
                case 4:
                    printboss = new string[5] { "Moarte", "His sole breathing has paralyzed you with fear", "Im not more than another knight of my dark lord, embrace yourself lil warrior I shall never let you see my lord, embrace death", "Think I am scary? Your imagination cannot even comprehend the malice in my lords soul", "You should die by my hand, my lord will not be as good to you as I would be, atleast I will be quick" };
                    break;
            }
            return printboss;
        }

        //Print monster library
        static string[] namelibrary(int[] array)
        {
            string[] printmonster = new string[5];
            switch (array[7])
            {
                case 1:
                    printmonster = new string[5] { "Skeleton", "Has hitted you with his clavicule", "I shall crush your bones with mine", "Im gonna bone you", "Eat my bone" };
                    break;
                case 2:
                    printmonster = new string[5] { "Zombie", "Has bitten you", "I love the smell of zombies in the morning", "Keep your friends close, but your zombies closer", "Sorry Im a vegan" };
                    break;
                case 3:
                    printmonster = new string[5] { "Dragon", "Has thrown you a fireball", "I shall spit on your burned corpse", "Die gray one, die", "Im colorblind" };
                    break;
                case 4:
                    printmonster = new string[5] { "Mage", "Has casted a curse upon thy", "I shall burn you to a crisp", "You, your family and all of your crops shall be cursed", "I have studied all my life for this" };
                    break;
                case 5:
                    printmonster = new string[5] { "Witch", "Has thrown you a fireball", "You shall burn like my sister did", "Curse you and your journey", "Ill go all necromancer on you" };
                    break;
                case 6:
                    printmonster = new string[5] { "Monkey", "Has thrown you a piece of feces", "Give me a banana and die", "And they said you evolved from me, ha", "Uh uh uh ah ah ah ah uh uh uh" };
                    break;
                case 7:
                    printmonster = new string[5] { "Cow", "Has milked you", "Im gonna milk you mofo", "MOOOOOOOooooOOOOOooooOOOOOOooooOOOOOOOooooo", "I dare you, Touch my boobs!, I double dare you" };
                    break;
                case 8:
                    printmonster = new string[5] { "Kamikaze", "You just where bombed", "Im gonna go pearl harbor on you", "We shall die by my hand", "If I where you I would blow up" };
                    break;
                case 9:
                    printmonster = new string[5] { "Giant Lizard", "Has spit some weird acid at you", "No I cannot talk to Voldemort", "I do not want to eat you, you are way too skinny", "ack-ack-ack-ackawoooo-ack-ack-ack" };
                    break;
                case 10:
                    printmonster = new string[5] { "Behemoth", "Has knock you back with he raw power", "I am more than just a big cow, I am more like a big bull", "Grrrrrrrrrrrrrrrrrrrrrr", "MOOOOOOOOOOOOOOOOooooooooooOOOOOOOoooooooOOOOOOOOOOoooo" };
                    break;
                case 11:
                    printmonster = new string[5] { "Quara", "Has blasted you with water", "Next time you take a shower remember it might be me in disguise", "Be like water, flow, but dont freeze freezing hurts", "I am never thirsty" };
                    break;
                case 12:
                    printmonster = new string[5] { "Mr. Bushybushy", "Has hitted you with his leafy arm", "I wanna be a big tree when I'm older", "I hate dogs, they always pee on me", "Is that a book you have there?! You shall die!" };
                    break;
                case 13:
                    printmonster = new string[5] { "Lizardious", "Has speared you", "What is you doing? Stahp!", "The King Lizard is my dad, so be careful with how you speak to me", "Don't you dare look at me" };
                    break;
                case 14:
                    printmonster = new string[5] { "Shaman", "Has invoked the spirits to hunt you down ", "If you where on my tribe, we would have sacrificed you at birth", "The ancients have chosen me to delete you", "I have godlike powers and you? what do you have to offer?" };
                    break;
                case 15:
                    printmonster = new string[5] { "Straw Monster", "Has hitted you with his straw hands", "You shall be CRUSHED", "Tururururuurururuur", "Mom says that I am really smart :)" };
                    break;
                case 16:
                    printmonster = new string[5] { "Cougar Mama", "Has tried to bite your head off", "How dare yoU!! MY BABIES!!!!", "How could someone eat somebody elses babies, you are a monster!", "Meow Meow Meow!" };
                    break;
                case 17://dogs
                    printmonster = new string[5] { "Hellhounds", "Have tried to bite your crotch", "We will sniff yo' ass nigga", "Throw me a bone! Throw me a bone!", "Woof! Woof!" };
                    break;
                case 18://gargoyle
                    printmonster = new string[5] { "Gargoyle", "Has tried to smack your face", "You remind me of Quasimodo", "I will blow you up", "01110010011011110110000101110010!" };
                    break;
                case 19://crocodile
                    printmonster = new string[5] { "Crocodile", "Has tried to champ your hands off", "I'm worse than Krokodil", "Don't try to make boots out of my skin", "Noup, I'm not the drug" };
                    break;
                case 20://bandit
                    printmonster = new string[5] { "Bandit", "Has tried to punch you.", "I'm Bandit Jesse", "I'm Bandit James", "I'm Bandit Meowth" };
                    break;
                case 21://stone guard
                    printmonster = new string[5] { "Stone Guard", "Has tried to crush you", "I'm hard", "I feel stoned", "I will sit on you" };
                    break;
                case 22://worm
                    printmonster = new string[5] { "Worm", "Has tried to eat you", "ROOOOOOOOAAAAAAR!!!!!", "ROOAAAAAAR!!!!!", "ROOOOOOOOOOOOOAAAAAAR!!!!!!" };
                    break;
                case 23://troll
                    printmonster = new string[5] { "Troll", "Has tried to eat you", "I pay my taxes. Leave my bridge... Motherlover", "Leave me alone, I want to listen to Justin Timberlake", "Do you ever feel, like a plastic bag, drifting through the wind..." };
                    break;
                case 24://cyclops
                    printmonster = new string[5] { "Cyclops", "Has wiped the floor with your ass", "No, I'm not the one from X - Men", "I said I don't know Xavier", "Nigga, what?!" };
                    break;
                case 25://mother earthquake
                    printmonster = new string[5] { "Mother Earthquake", "Has tried to punish you", "How dare you step on my ground, thats my nephew", "You cannot eat your pudding if you dont eat your meat", "ROOOOOOOOOOOOOAAAAAAR!!!!!!" };
                    break;
            }
            return printmonster;
        }

        //ARMOR-ATTACK method (magic resist.magical attack)  //add an if that tells if it is magical or normal attack
        static int substractfromattack(int def, int attack)
        {
            int percen, enemy;
            percen = def / 10 * 2;
            enemy = attack * (100 - percen) / 100;
            return enemy;
        }

        //special abilities method
        static string[] abilities()
        {
            string[] ability = new string[6];

            switch (Stats[10])
            {

                case 1:
                    ability = new string[6] { "My body as a temple", "Holy Reinforcement", "Holy Fortress", "Smite", "Holy Light", "God's Retribution" };
                    break;
                case 2:
                    ability = new string[6] { "Fireball", "Meteor Shower", "Hell Fire Meltdown", "Ice Bolt", "Freezing Wind", "Ice Age Avalanche" };
                    break;
                case 3:
                    ability = new string[6] { "Fury in my veins", "Blinded by hatred", "Consumed by Wrath", "Hatred release", "Furious explosion", "Wrath cataclysm" };
                    break;

                case 4:
                    ability = new string[6] { "Poison", "Toxic Invasion", "Black Plague", "Sneak Attack", "Shadow prick", "Nightmare Prick" };
                    break;
                case 5:
                    ability = new string[6] { "Disease", "Nightmare Virus", "Demonic Plague", "Life Drain", "Vital Steal", "Necromancy" };
                    break;
            }
            return ability;
        }

        //Ability damage1 database	
        static int abilitydmg1()
        {
            int dmg = 0;
            switch (Stats[10])
            {
                case 1://(PALADIN
                    switch (ability1)
                    {
                        case 1:
                            return dmg = Stats[0] * 7 / 100;
                        case 2:
                            return dmg = Stats[0] * 8 / 100;
                        case 3:
                            return dmg = Stats[0] * 9 / 100;
                        case 4:
                            return dmg = Stats[0] * 10 / 100;
                        case 5:
                            return dmg = Stats[0] * 11 / 100;
                        case 6:
                            return dmg = Stats[0] * 12 / 100;
                        case 7:
                            return dmg = Stats[0] * 15 / 100;
                        case 8:
                            return dmg = Stats[0] * 16 / 100;
                        case 9:
                            return dmg = Stats[0] * 17 / 100;
                    }
                    break;
                case 2://(MAGE)
                    switch (ability1)
                    {
                        case 1:
                            return dmg = Stats[2] * 160 / 100;
                        case 2:
                            return dmg = Stats[2] * 162 / 100;
                        case 3:
                            return dmg = Stats[2] * 165 / 100;
                        case 4:
                            return dmg = Stats[2] * 180 / 100;
                        case 5:
                            return dmg = Stats[2] * 182 / 100;
                        case 6:
                            return dmg = Stats[2] * 185 / 100;
                        case 7:
                            return dmg = Stats[2] * 200 / 100;
                        case 8:
                            return dmg = Stats[2] * 202 / 100;
                        case 9:
                            return dmg = Stats[2] * 205 / 100;
                    }
                    break;
                case 3: //(WARRIOR)
                    switch (ability1)
                    {
                        case 1:
                            return dmg = Stats[4] * 120 / 100;
                        case 2:
                            return dmg = Stats[4] * 125 / 100;
                        case 3:
                            return dmg = Stats[4] * 130 / 100;
                        case 4:
                            return dmg = Stats[4] * 150 / 100;
                        case 5:
                            return dmg = Stats[4] * 155 / 100;
                        case 6:
                            return dmg = Stats[4] * 165 / 100;
                        case 7:
                            return dmg = Stats[4] * 200 / 100;
                        case 8:
                            return dmg = Stats[4] * 201 / 100;
                        case 9:
                            return dmg = Stats[4] * 202 / 100;
                    }
                    break;
                case 4: //(ROGUE)
                    switch (ability1)
                    {
                        case 1:
                            return dmg = Stats[4] * 20 / 100;
                        case 2:
                            return dmg = Stats[4] * 21 / 100;
                        case 3:
                            return dmg = Stats[4] * 22 / 100;
                        case 4:
                            return dmg = Stats[4] * 25 / 100;
                        case 5:
                            return dmg = Stats[4] * 26 / 100;
                        case 6:
                            return dmg = Stats[4] * 27 / 100;
                        case 7:
                            return dmg = Stats[4] * 35 / 100;
                        case 8:
                            return dmg = Stats[4] * 37 / 100;
                        case 9:
                            return dmg = Stats[4] * 40 / 100;
                    }
                    break;
                case 5: //(WARLOCK)
                    switch (ability1)
                    {
                        case 1:
                            return dmg = Stats[2] * 30 / 100;
                        case 2:
                            return dmg = Stats[2] * 32 / 100;
                        case 3:
                            return dmg = Stats[2] * 35 / 100;
                        case 4:
                            return dmg = Stats[2] * 45 / 100;
                        case 5:
                            return dmg = Stats[2] * 46 / 100;
                        case 6:
                            return dmg = Stats[2] * 47 / 100;
                        case 7:
                            return dmg = Stats[2] * 50 / 100;
                        case 8:
                            return dmg = Stats[2] * 52 / 100;
                        case 9:
                            return dmg = Stats[2] * 55 / 100;
                    }
                    break;
            }
            return dmg;
        }

        //Ability damage database	
        static int abilitydmg2()
        {
            int dmg = 0;
            switch (Stats[10])
            {
                case 1://(PALADIN
                    switch (ability2)
                    {
                        case 1:
                            return dmg = Stats[0] * 10 / 100;
                        case 2:
                            return dmg = Stats[0] * 11 / 100;
                        case 3:
                            return dmg = Stats[0] * 12 / 100;
                        case 4:
                            return dmg = Stats[0] * 15 / 100;
                        case 5:
                            return dmg = Stats[0] * 16 / 100;
                        case 6:
                            return dmg = Stats[0] * 17 / 100;
                        case 7:
                            return dmg = Stats[0] * 20 / 100;
                        case 8:
                            return dmg = Stats[0] * 22 / 100;
                        case 9:
                            return dmg = Stats[0] * 25 / 100;
                    }
                    break;
                case 2://(MAGE)                
                    switch (ability2)
                    {
                        case 1:
                            return dmg = Stats[2] * 145 / 100;
                        case 2:
                            return dmg = Stats[2] * 150 / 100;
                        case 3:
                            return dmg = Stats[2] * 152 / 100;
                        case 4:
                            return dmg = Stats[2] * 165 / 100;
                        case 5:
                            return dmg = Stats[2] * 167 / 100;
                        case 6:
                            return dmg = Stats[2] * 170 / 100;
                        case 7:
                            return dmg = Stats[2] * 180 / 100;
                        case 8:
                            return dmg = Stats[2] * 182 / 100;
                        case 9:
                            return dmg = Stats[2] * 185 / 100;
                    }
                    break;
                case 3: //(WARRIOR)
                    switch (ability2)
                    {
                        case 1:
                            return dmg = Stats[4] * 60 / 100;
                        case 2:
                            return dmg = Stats[4] * 62 / 100;
                        case 3:
                            return dmg = Stats[4] * 65 / 100;
                        case 4:
                            return dmg = Stats[4] * 70 / 100;
                        case 5:
                            return dmg = Stats[4] * 72 / 100;
                        case 6:
                            return dmg = Stats[4] * 73 / 100;
                        case 7:
                            return dmg = Stats[4] * 75 / 100;
                        case 8:
                            return dmg = Stats[4] * 77 / 100;
                        case 9:
                            return dmg = Stats[4] * 80 / 100;
                    }
                    break;
                case 4: //(ROGUE)                    
                    switch (ability2)
                    {
                        case 1:
                            return dmg = Stats[4] * 180 / 100;
                        case 2:
                            return dmg = Stats[4] * 185 / 100;
                        case 3:
                            return dmg = Stats[4] * 190 / 100;
                        case 4:
                            return dmg = Stats[4] * 200 / 100;
                        case 5:
                            return dmg = Stats[4] * 220 / 100;
                        case 6:
                            return dmg = Stats[4] * 225 / 100;
                        case 7:
                            return dmg = Stats[4] * 250 / 100;
                        case 8:
                            return dmg = Stats[4] * 255 / 100;
                        case 9:
                            return dmg = Stats[4] * 260 / 100;
                    }
                    break;
                case 5: //(WARLOCK)                 
                    switch (ability2)
                    {
                        case 1:
                            return dmg = Stats[2] * 75 / 100;
                        case 2:
                            return dmg = Stats[2] * 77 / 100;
                        case 3:
                            return dmg = Stats[2] * 80 / 100;
                        case 4:
                            return dmg = Stats[2] * 90 / 100;
                        case 5:
                            return dmg = Stats[2] * 92 / 100;
                        case 6:
                            return dmg = Stats[2] * 95 / 100;
                        case 7:
                            return dmg = Stats[2] * 120 / 100;
                        case 8:
                            return dmg = Stats[2] * 122 / 100;
                        case 9:
                            return dmg = Stats[2] * 125 / 100;
                    }
                    break;
            }
            return dmg;
        }

        //ability effects database
        static int[] abilityeffects(int option)
        {
            int[] effects = new int[7];
            switch (Stats[10])
            {
                case 1:
                    switch (option)
                    {
                        case 3:
                            effects = new int[7] { FightStats[0], FightStats[1] - abilitycost1(), FightStats[2], FightStats[3] + abilitydmg1(), FightStats[4], FightStats[5] + abilitydmg1(), FightStats[6] };
                            break;
                        case 4:
                            effects = new int[7] { FightStats[0] + abilitydmg2(), FightStats[1] - abilitycost2(), FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                    }
                    break;

                case 2:
                    switch (option)
                    {
                        case 3:
                            effects = new int[7] { FightStats[0], FightStats[1] - abilitycost1(), FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                        case 4:
                            effects = new int[7] { FightStats[0], FightStats[1] - abilitycost2(), FightStats[2], FightStats[3], FightStats[4], FightStats[5] + abilitydmg2(), FightStats[6] };
                            break;
                    }
                    break;
                case 3:
                    switch (option)
                    {
                        case 3:
                            effects = new int[7] { FightStats[0] - abilitycost1(), FightStats[1], FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                        case 4:
                            effects = new int[7] { FightStats[0] + abilitydmg1(), FightStats[1], FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                    }
                    break;
                case 4:
                    switch (option)
                    {
                        case 3:
                            effects = new int[7] { FightStats[0] - abilitycost1(), FightStats[1], FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                        case 4:
                            effects = new int[7] { FightStats[0] - abilitycost1(), FightStats[1], FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                    }
                    break;
                case 5:
                    switch (option)
                    {
                        case 3:
                            effects = new int[7] { FightStats[0], FightStats[1] - abilitycost1(), FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                        case 4:
                            effects = new int[7] { FightStats[0] + abilitydmg1(), FightStats[1] - abilitycost1(), FightStats[2], FightStats[3], FightStats[4], FightStats[5], FightStats[6] };
                            break;
                    }
                    break;
            }
            FightStats = effects;
            return FightStats;
        }

        //ability costs
        static int abilitycost1()
        {
            int abilitycost = 0;

            switch (Stats[10])
            {
                case 1:
                    abilitycost = 40;
                    break;
                case 2:
                    abilitycost = Stats[1] * 10 / 100;
                    break;
                case 3:
                    switch (ability1)
                    {
                        case 1:
                            abilitycost = Stats[0] * 20 / 100;
                            break;
                        case 2:
                            abilitycost = Stats[0] * 21 / 100;
                            break;
                        case 3:
                            abilitycost = Stats[0] * 22 / 100;
                            break;
                        case 4:
                            abilitycost = Stats[0] * 25 / 100;
                            break;
                        case 5:
                            abilitycost = Stats[0] * 26 / 100;
                            break;
                        case 6:
                            abilitycost = Stats[0] * 27 / 100;
                            break;
                        case 7:
                            abilitycost = Stats[0] * 30 / 100;
                            break;
                        case 8:
                            abilitycost = Stats[0] * 31 / 100;
                            break;
                        case 9:
                            abilitycost = Stats[0] * 32 / 100;
                            break;
                    }
                    break;
                case 4:
                    abilitycost = Stats[5] * 5 / 100;
                    break;
                case 5:
                    switch (ability1)
                    {
                        case 1:
                            abilitycost = 20;
                            break;
                        case 2:
                            abilitycost = 22;
                            break;
                        case 3:
                            abilitycost = 25;
                            break;
                        case 4:
                            abilitycost = 50;
                            break;
                        case 5:
                            abilitycost = 52;
                            break;
                        case 6:
                            abilitycost = 55;
                            break;
                        case 7:
                            abilitycost = 60;
                            break;
                        case 8:
                            abilitycost = 65;
                            break;
                        case 9:
                            abilitycost = 66;
                            break;
                    }
                    break;
            }
            return abilitycost;
        }
        static int abilitycost2()
        {
            int abilitycost = 0;

            switch (Stats[10])
            {
                case 1:
                    abilitycost = 75;
                    break;
                case 2:
                    abilitycost = Stats[1] * 10 / 100;
                    break;
                case 4:
                    abilitycost = Stats[0] * 15 / 100;
                    break;
            }
            return abilitycost;
        }

        //After Mob Item
        static int[] AfterMobItem()
        {
            string[] weapons = new string[5] { "Sword and Shield", "Staff", "Two Handed Axe", "Dagger", "Rod and Shield" };
            Random r = new Random();
            switch (r.Next(0, 3))
            {
                case 0:
                    Console.WriteLine("Unfortunatley, all items carried by your enemy were rendered useless after your battle. \nBrave warrior, there is nothing left for you here on this battleground.");//there are no changes in the player's Stats 
                    break;
                case 1://Weapon
                    Console.WriteLine("Brave warrior, you have found a powerful {0}.", weapons[Stats[10]]);//Stats[10] represents the type of creature. For example, if Stats[10] = 5, then it is a Wralock, and therefore its weapon will be the fifth one. 
                    WeaponType();
                    switch (areacounter)
                    {
                        case 1://increase by powers of 2.5
                            Console.WriteLine("The gods were not in your favor, and your luck was certainy limited. \nYour weapon has turned into a wooden {0}.", weapons[Stats[10]]);
                            if (Stats[10] == 1 || Stats[10] == 5)//if the character is a paladin or a warlock
                            {
                                Stats[4] /= 20;
                                Stats[5] /= 20;
                            }
                            else
                            {
                                Stats[4] += 7;
                            }
                            break;
                        case 2:
                            Console.WriteLine("Your weapon has become irradiated. \nAlthough powerful, your {0} cannot reach its full potential.");
                            if (Stats[10] == 1 || Stats[10] == 5)//if the character is a paladin or a warlock
                            {
                                Console.WriteLine("Your attack and defense have increased.");
                                Stats[4] /= 10;
                                Stats[5] /= 10;
                            }
                            else
                            {
                                Stats[4] += 16;
                            }
                            break;
                        case 3:
                            Console.WriteLine("Your weapon has been blessed with the fire of Оташ. It contains great power.");
                            if (Stats[10] == 1 || Stats[10] == 5)//if the character is a paladin or a warlock
                            {
                                Console.WriteLine("Your attack and defense have increased.");
                                Stats[4] = Stats[4] / 3 - 10;
                                Stats[5] = Stats[5] / 3 - 10;
                            }
                            else
                            {
                                Stats[4] += 40;
                            }
                            break;
                        case 4:
                            Console.WriteLine("You have encountered one of the most powerful weapons. \nForged in the fires of Tnoum Mood.");
                            if (Stats[10] == 1 || Stats[10] == 5)//if the character is a paladin or a warlock
                            {
                                Console.WriteLine("Your attack and defense have increased.");
                                Stats[4] = Stats[4] - 50;
                                Stats[5] = Stats[5] - 50;
                            }
                            else
                            {
                                Stats[4] += 98;
                            }
                            break;
                        case 5:
                            Console.WriteLine("Your weapon has achieved a Quantum State. \nIt is the most powerful weapon in the realm of Zeme.");
                            if (Stats[10] == 1 || Stats[10] == 5)//if the character is a paladin or a warlock
                            {
                                Console.WriteLine("Your attack and defense have increased.");
                                Stats[4] = Stats[4] - 25;
                                Stats[5] = Stats[5] - 25;
                            }
                            Stats[4] += 245;
                            break;
                    }
                    break;

                case 2://Armour
                    Console.WriteLine("Your enemy carried a some mighty armour. Perhaps you can use some of it?");
                    switch (areacounter)
                    {
                        case 1:
                            Console.WriteLine("A wooden armour... Unfortunately the most useless of all. \nBut let us see which part of the armour may you use.");
                            Console.ReadLine();
                            switch (Stats[10])
                            {
                                case 1://Paladin: Heavy Armour
                                    Stats[5] += ArmourPart() + 2;
                                    break;
                                case 2://Wizard: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 2) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 2) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                                case 3://Warrior
                                    Stats[5] += ArmourPart() + 2;
                                    break;
                                case 4://Rogue: Light armour
                                    Stats[3] = Stats[3] + (ArmourPart() + 2) / 2;//50% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 2) / 2;//50% of the armour upgrade goes to defense
                                    break;
                                case 5://Warlock: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 2) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 2) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("This armour was irradiated with the Anyag of Zeme. \nIt is rare, but not very powerful. \nYour destiny will choose what part of its armour will be useful.");
                            Console.ReadLine();
                            switch (Stats[10])
                            {
                                case 1://Paladin: Heavy Armour
                                    Stats[5] += ArmourPart() + 8;
                                    break;
                                case 2://Wizard: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 8) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 8) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                                case 3://Warrior
                                    Stats[5] += ArmourPart() + 2;
                                    break;
                                case 4://Rogue: Light armour
                                    Stats[3] = Stats[3] + (ArmourPart() + 8) / 2;//50% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 8) / 2;//50% of the armour upgrade goes to defense
                                    break;
                                case 5://Warlock: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 8) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 8) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine("Your armour has been blessed with the fire of Оташ. It contains great power. \nBut, what part of it can you use?");
                            Console.ReadLine();
                            switch (Stats[10])
                            {
                                case 1://Paladin: Heavy Armour
                                    Stats[5] += ArmourPart() + 32;
                                    break;
                                case 2://Wizard: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 32) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 32) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                                case 3://Warrior: Heavy Armour
                                    Stats[5] += ArmourPart() + 42;
                                    break;
                                case 4://Rogue: Light armour
                                    Stats[3] = Stats[3] + (ArmourPart() + 32) / 2;//50% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 32) / 2;//50% of the armour upgrade goes to defense
                                    break;
                                case 5://Warlock: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 32) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 32) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                            }
                            break;
                        case 4:
                            Console.WriteLine("You have encountered one of the most resistant armours. \nForged in the fires of Tnoum Mood. \nWhat part of it fits you?");
                            Console.ReadLine();
                            switch (Stats[10])
                            {
                                case 1://Paladin: Heavy Armour
                                    Stats[5] += ArmourPart() + 60;
                                    break;
                                case 2://Wizard: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 60) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 60) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                                case 3://Warrior
                                    Stats[5] += ArmourPart() + 60;
                                    break;
                                case 4://Rogue: Light armour
                                    Stats[3] = Stats[3] + (ArmourPart() + 60) / 2;//50% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 60) / 2;//50% of the armour upgrade goes to defense
                                    break;
                                case 5://Warlock: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 60) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 60) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                            }
                            break;
                        case 5:
                            Console.WriteLine("Your armour is in Quantum State. \nBut it is too powerfull to use all at once. \nYour luck will determine what part you may take.");
                            Console.ReadLine();
                            switch (Stats[10])
                            {
                                case 1://Paladin: Heavy Armour
                                    Stats[5] += ArmourPart() + 250;
                                    break;
                                case 2://Wizard: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 250) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 250) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                                case 3://Warrior
                                    Stats[5] += ArmourPart() + 250;
                                    break;
                                case 4://Rogue: Light armour
                                    Stats[3] = Stats[3] + (ArmourPart() + 250) / 2;//50% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 250) / 2;//50% of the armour upgrade goes to defense
                                    break;
                                case 5://Warlock: Robes
                                    Stats[3] = Stats[3] + (ArmourPart() + 250) * 70 / 100;//70% of the armour upgrade goes to magic resist
                                    Stats[5] = Stats[5] + (ArmourPart() + 250) * 30 / 100;//30% of the armour upgrade goes to defense
                                    break;
                            }
                            break;
                    }
                    break;
            }
            return Stats;
        }
        static int ArmourPart()
        {
            Random r = new Random();
            switch (r.Next(1, 5))
            {
                case 1://Head
                    Console.WriteLine("Your enemy's helmet will be of great utility.");
                    return 10;
                case 2://Chest
                    Console.WriteLine("Take the monster's breastplate. It shall protect you in future battles.");
                    return 30;
                case 3://Legs
                    Console.WriteLine("Snatch your enemy's cuisse. It shall protect your legs.");
                    return 5;
                case 4://Boots
                    Console.WriteLine("Your enemy was carrying a pair of War Boots. \nTake them into future battles.");
                    return 1;
                default:
                    return 0;
            }
        }
        static void WeaponType()
        {
            switch (Stats[10])//Paladin, Wizard, Warrior, Rogue, Warlock
            {
                case 1://Paladin
                    Stats[4] = 150;//attack
                    Stats[5] = 150;//defense
                    break;
                case 5://Warlock
                    Stats[4] = 150;//attack
                    Stats[5] = 150;//defense
                    break;
            }
        }

        //Them Routes Main
        static void route()
        {
            Random r = new Random();

            switch (Area)
            {
                case 1://Jungle
                    if (fightcounter == 5)
                    {
                        routestaken = new int[5] { 0, 0, 0, 0, 0 };
                        Console.WriteLine("You have seem to reach the limits of the jungle \nThere is nothing infront of you but the deep blackness of the jungle, you begin to freak out and run until you stumble upon something, a temple, you feel the need to walk towards it, you have no choice, what will you find inside?");
                        fight();
                        ChooseArea();
                    }
                    else
                    {
                        int routes = r.Next(1, 6);
                        foreach (int i in routestaken)
                        {
                            if (routes == i)
                            {
                                route();
                            }
                        }
                        switch (routes)
                        {
                            case 1:
                                actualroute = 1;
                                hole();
                                routestaken[0] = 1;
                                actualroute = 0;
                                break;
                            case 2:
                                actualroute = 2;
                                junglehouse();
                                routestaken[1] = 2;
                                actualroute = 0;
                                break;
                            case 3:
                                actualroute = 3;
                                river();
                                routestaken[2] = 3;
                                actualroute = 0;
                                break;
                            case 4:
                                actualroute = 4;
                                sleep();
                                routestaken[3] = 4;
                                actualroute = 0;
                                break;
                            case 5:
                                actualroute = 5;
                                paranoia();
                                routestaken[4] = 5;
                                actualroute = 0;
                                break;
                        }
                    }
                    DeathMethod();
                    break;
                case 2: //Molten Mountain
                    if (fightcounter == 5)
                    {
                        routestaken = new int[5] { 0, 0, 0, 0, 0 };
                        Console.WriteLine("On your long journey through the Molten Mountains you have finally reached what you where looking for, the highest volcano of them all. \nThe legend tells that one of godly powers lives inside the volcano and that he is the one held responsible for this land. \nToday you shall confront him in battle and so you walk into the volcano");
                        fight();
                        ChooseArea();
                    }
                    else
                    {
                        int routes = r.Next(1, 6);
                        foreach (int i in routestaken)
                        {
                            if (routes == i)
                            {
                                route();
                            }
                        }
                        switch (routes)
                        {
                            case 1:
                                actualroute = 1;
                                mountainroad();
                                routestaken[0] = 1;
                                actualroute = 0;
                                break;
                            case 2:
                                actualroute = 2;
                                Avalanche();
                                routestaken[1] = 2;
                                actualroute = 0;
                                break;
                            case 3:
                                actualroute = 3;
                                Fissure();
                                routestaken[2] = 3;
                                actualroute = 0;
                                break;
                            case 4:
                                actualroute = 4;
                                Cyclops();
                                routestaken[3] = 4;
                                actualroute = 0;
                                break;
                            case 5:
                                actualroute = 5;
                                Earthquake();
                                routestaken[4] = 5;
                                actualroute = 0;
                                break;
                        }
                    }
                    DeathMethod();
                    break;
                case 3://Death Desert
                    if (fightcounter == 5)
                    {
                        routestaken = new int[5] { 0, 0, 0, 0, 0 };
                        Console.WriteLine(userName + ", on your journey through the desert you have struggled to survive the threat of its vicious inhabitants. \nNow you are reaching the far end of the barren lands. \nIn front of you lies a sleeping giant... One of the most terrible monsters of Zeme. \nBrace yourself {0}.", userName);
                        fight();
                        ChooseArea();
                    }
                    else
                    {
                        int routes = r.Next(1, 6);
                        foreach (int i in routestaken)
                        {
                            if (routes == i)
                            {
                                route();
                            }
                        }
                        switch (routes)
                        {
                            case 1:
                                actualroute = 1;
                                Oasis();
                                routestaken[0] = 1;
                                actualroute = 0;
                                break;
                            case 2:
                                actualroute = 2;
                                Sandstorm();
                                routestaken[1] = 2;
                                actualroute = 0;
                                break;
                            case 3:
                                actualroute = 3;
                                Library();
                                routestaken[2] = 3;
                                actualroute = 0;
                                break;
                            case 4:
                                actualroute = 4;
                                nightdesert();
                                routestaken[3] = 4;
                                actualroute = 0;
                                break;
                            case 5:
                                actualroute = 5;
                                desertGoblin();
                                routestaken[4] = 5;
                                actualroute = 0;
                                break;
                        }
                    }
                    DeathMethod();
                    break;
                case 4://City of the Death
                    if (fightcounter == 5)
                    {
                        routestaken = new int[5] { 0, 0, 0, 0, 0 };
                        Console.WriteLine(userName + ", dwelling in the City of the Death is not an easy task. \nAs you may know, you must get out of here. \nIf you stay much longer you may die... permanently... \nAs this land is cursed and no one has ever stayd long enough and lived. \nThe exit of this underground city lies in front of you. \nBut in order to get out you must... FIGHT!");
                        fight();
                        ChooseArea();
                    }
                    else
                    {
                        int routes = r.Next(1, 6);
                        foreach (int i in routestaken)
                        {
                            if (routes == i)
                            {
                                route();
                            }
                        }
                        switch (routes)
                        {
                            case 1:
                                actualroute = 1;
                                warlockLair();
                                routestaken[0] = 1;
                                actualroute = 0;
                                break;
                            case 2:
                                actualroute = 2;
                                dogs();
                                routestaken[1] = 2;
                                actualroute = 0;
                                break;
                            case 3:
                                actualroute = 3;
                                skeletonArmy();
                                routestaken[2] = 3;
                                actualroute = 0;
                                break;
                            case 4:
                                actualroute = 4;
                                gargoyle();
                                routestaken[3] = 4;
                                actualroute = 0;
                                break;
                            case 5:
                                actualroute = 5;
                                zombieRitual();
                                routestaken[4] = 5;
                                actualroute = 0;
                                break;
                        }
                    }
                    DeathMethod();
                    break;
            }
        }

        private static void DeathMethod()
        {
            if (Stats[0] <= 0)//if the player dies, it shall start again from the beginning of the same area
            {
                Console.WriteLine("You have died {0}. But the Tanrilar are amused by your performance. \nThey will bring you back from the death in put you again ate the beginning of the last area you visited. \nGood luck brave warrior.", userName);
                routestaken = new int[5] { 0, 0, 0, 0, 0 };
                route();
            }
            else
            {
                route();
            }
        }

        //areas to take into notice : nightmare jungle, molten mountains, death desert, City of the Death

        //Nightmare jungle events
        static void hole()
        {
            int opcion;
            bool valid = true;

            while (valid)
            {
                Console.WriteLine("It's getting dark out here in the jungle, this specific jungle can be really scary at night, horrible monsters and even animals dominate it when the sun hides.");
                Console.WriteLine("You see some fallen logs that could be used to hide in for the night");
                Console.WriteLine("What do you do?");
                Console.WriteLine("1.Go and setup a temporary base and sleep for the night in the protection of the logs \n2.Keep walking maybe you get to safe ground before the sun sets");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        Random r = new Random();
                        int happens = r.Next(3);
                        switch (happens)
                        {
                            case 1:
                                Console.WriteLine("You where lucky enough and made it through the night, you may now continue on your journey");
                                Enter();
                                break;
                            case 2:
                                Console.WriteLine("The logs where not as safe as you thought, it was the home of a creature, you will have to fight it");
                                Enter();
                                fight();
                                break;
                        }
                        valid = false;
                        break;
                    case 2:
                        Console.WriteLine("You where not that lucky and got attacked during your walk in the dark, you where attacked by mobs");
                        Enter();
                        fight();
                        valid = false;
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option to continue");
                        Enter();
                        break;
                }
            }
        }
        static void junglehouse()
        {
            int opcion;
            bool valid = true;
            while (valid)
            {
                Console.WriteLine("While you walk you find some baby panthers playing around in the grass. What do yo do?");
                Console.WriteLine("1.You are hungry so you grab the babies and eat them \n2.You let them be and continue with your journey");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        valid = false;
                        Console.WriteLine("By eating the babies you get a boost in your manliness (+20 hp) \nBut you also pissed the mother off you have to fight her");
                        Stats[0] += 20;
                        Enter();
                        fight();
                        break;
                    case 2:
                        valid = false;
                        Console.WriteLine("As you let them be they suddenly turn in 3 angels of mercy granting you with a special gift");
                        Enter();
                        AfterMobItem();
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option to continue");
                        break;
                }
            }
        }
        static void river()
        {
            int opcion, opcion2, opcion3;
            int siren;
            bool valid = true, valid2 = true;
            Random r = new Random();
            siren = r.Next(3);
            while (valid)
            {
                Console.WriteLine("You are walking and you get a glimpse of what seems to be a river");
                Console.WriteLine("Do you: \n1.walk up to it and drink some water to continue with your journey \n2. You follow it trying to see if it takes you to a village and/or safety ");
                int.TryParse(Console.ReadLine(), out opcion);
                switch (opcion)
                {
                    case 1:
                        while (valid2)
                        {
                            Console.WriteLine("It wasn´t a simple river as you get near to it you hear the beautiful voice of what seems a woman, you face yet another decision:");
                            Console.WriteLine("1. Follow the beautiful voice \n2.Back off and keep walking ");
                            int.TryParse(Console.ReadLine(), out opcion2);
                            switch (opcion2)
                            {
                                case 1:
                                    valid2 = false;
                                    switch (siren)
                                    {
                                        case 1:
                                            Console.WriteLine("You where lucky finding a beautiful women that wanted no more but to help you in exchange of a kiss");
                                            Console.WriteLine("She has granted you with an item");
                                            AfterMobItem();
                                            Enter();
                                            break;
                                        case 2:
                                            Console.WriteLine("The voice was not of a woman, you find yourself fooled by a monster \nPrepare to fight!");
                                            Enter();
                                            fight();
                                            break;
                                    }
                                    break;
                                case 2:
                                    valid2 = false;
                                    Console.WriteLine("You ignore the voice and continue on your journey");
                                    Enter();
                                    break;

                                default:
                                    Console.WriteLine("Please choose a valid option to continue");
                                    break;
                            }
                        }
                        break;

                    case 2:
                        switch (siren)
                        {
                            case 1:
                                while (valid2)
                                {
                                    Console.WriteLine("As you follow it you see what you have wished for, a village and you run towards it");
                                    Console.WriteLine("As you aproach it, you notice something terribly wrong there is no people just death bodies, you face a choice");
                                    Console.WriteLine("1.You loot the death bodies in search of something usefull \n2.You bury the death in sign of respect");
                                    int.TryParse(Console.ReadLine(), out opcion3);
                                    switch (opcion3)
                                    {
                                        case 1:
                                            valid2 = false;
                                            Console.WriteLine("You gather stuff from the death corpses");
                                            AfterMobItem();
                                            Enter();
                                            break;
                                        case 2:
                                            valid2 = false;
                                            Console.WriteLine("As you finish burying the bodies and paying your respects you find something shiny in the floor \nYou have gotten a lucky talisman");
                                            Console.WriteLine("Luck +10");
                                            Stats[6] += 10;
                                            Enter();
                                            break;
                                        default:
                                            Console.WriteLine("Please choose a valid option to continue");
                                            break;
                                    }
                                }
                                break;
                            case 2:
                                Console.WriteLine("There was no village, no nothing. \nYou are tired but the long walk to nothing has given you a boost in your stamina \nHp + 20");
                                Stats[0] += 20;
                                Enter();
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option to continue");
                        break;
                }
            }
        }
        static void sleep()
        {
            int answer;
            bool valid;
            bool runCycle = false;
            Console.WriteLine("You have been several hours wandering through the jungle. \nNow you may either go to sleep or keep walking.");
            do
            {
                runCycle = false;
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Go to sleep. \n2.Keep wandering through the jungle.");
                valid = int.TryParse(Console.ReadLine(), out answer);
                if (valid)
                {
                    switch (answer)
                    {
                        case 1:
                            Console.WriteLine("Now that you have gone to sleep you are vulnerable.");
                            int enemyDice = Dice(1, 10);
                            Console.WriteLine("The dice has been thrown and it shows... {0} \nYou must get a number bigger than {0} in order to go through the night.", enemyDice);
                            Console.WriteLine("Throw your dice and test your luck. \nPress any key once you're ready to try.");
                            Console.ReadLine();
                            int myDice = Dice(1, 10);
                            Console.WriteLine("You threw {0}", myDice);
                            if (enemyDice >= myDice)
                            {
                                Console.WriteLine("The cold winter's night has overpowered you. \nYou are now freezing to death.");
                                FightStats[0] = 0;
                                Console.WriteLine("Your HP is {0}", FightStats[0]);
                            }
                            else
                            {
                                Console.WriteLine("Your resistance is amazing {0}", userName);
                                Console.WriteLine("You have endured the night.");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Since you won't sleep tonight you will be safe from danger. \nYou will be alert. But you won't be rested. This will decrease your HP.");
                            Stats[0] -= 20;
                            Console.WriteLine("Now your HP is of {0} points.", Stats[0]);
                            break;
                        default:
                            Console.WriteLine("You have not chosen a valid option from the menu. \nPlease press any key and try again.");
                            Enter();
                            runCycle = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You have not chosen a valid option from the menu. \nPlease press any key and try again.");
                    Enter();
                    runCycle = true;
                }
            } while (runCycle);
        }
        static void paranoia()
        {
            int answer;
            bool valid;
            bool runCycle = false;
            Console.WriteLine("As you walk through the jungle you try to make the least amount of noise. \nYou know danger lurks in the shadows, so you might be cautious. \nBut you get distracted and when as you turn around you see an enormous spider has bitten you. \nYour senses will start failing. And only your luck will save you now. \nPress any button to continue.");
            Console.ReadLine();
            Console.WriteLine("A shadow beast has appeared in front of you.");
            do
            {
                runCycle = false;
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Strike the beast. \n2.Run away from the beast.");
                valid = int.TryParse(Console.ReadLine(), out answer);
                if (valid)
                {
                    switch (answer)
                    {
                        case 1:
                            Console.WriteLine("Your luck will determine if the beast was more powerful than you.");
                            int enemyDice = Dice(1, 10);
                            Console.WriteLine("Its dice has been thrown and it shows... {0} \nYou must get a number bigger than {0} in order to go through the night.", enemyDice);
                            Console.WriteLine("Throw your dice and test your luck. \nPress any key once you're ready to try.");
                            Console.ReadLine();
                            int myDice = Dice(1, 10);
                            Console.WriteLine("You threw {0}", myDice);
                            if (enemyDice >= myDice)
                            {
                                Console.WriteLine("The beast is stronger than you. \nIt is grabbing you by your neck and throwing you against a tree.");
                                Stats[0] -= 20;
                                Console.WriteLine("Your HP is {0}", Stats[0]);
                            }
                            else
                            {
                                Console.WriteLine("You are stronger than the beast {0}", userName);
                                Console.WriteLine("You have inflicted damage on it. \nBut guess what, the shadow beast was merely an illusion caused by the spider.");
                            }
                            break;
                        case 2:
                            enemyDice = Dice(1, 10);
                            Console.WriteLine("Throw your dice and see if you are faster than the beast. \nPress any key when you are ready to test your fortune.");
                            Console.ReadLine();
                            myDice = Dice(1, 10);
                            Console.WriteLine("You threw {0}", myDice);
                            if (enemyDice >= myDice)
                            {
                                Console.WriteLine("The beast is faster than you. \nIt is now grabbing you by your neck and throwing you against a tree.");
                                Stats[0] -= 20;
                                Console.WriteLine("Your HP is {0}", Stats[0]);
                            }
                            else
                            {
                                Console.WriteLine("You are faster than the beast {0}", userName);
                                Console.WriteLine("You have fled from it. \nBut guess what, the shadow beast was merely an illusion caused by the spider.");
                            }
                            break;
                        default:
                            Console.WriteLine("You have bot chosen a valid option from the menu. \nPlease press any key and try again.");
                            Enter();
                            runCycle = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You have bot chosen a valid option from the menu. \nPlease press any key and try again.");
                    Enter();
                    runCycle = true;
                }
            } while (runCycle);
        }

        //Molten mountains
        static void mountainroad()
        {
            int choice;
            bool valid = true;
            string answer;
            while (valid)
            {
                Console.WriteLine("As you walk through the molten mountain you begin to feel the burden of such a travel, your shoes have been burned thanks to the hot surface of the rocks");
                Console.WriteLine("And when you finally begin to give up you see something that amazes you, you see a sign and 2 choices \nIn one side the mountains continue until what seems the end of the world, the other one shows a neat little road");
                Console.WriteLine("The Sign says: Beware no all is what it seems");
                Console.WriteLine("What would you choose? \n1.Right(Mountain) \n2.Left(Road)");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("As you begin to climb the steep mountain you begin to notice something \nYou are not as tired as you where before");
                            Console.WriteLine("By choosing the hard road you have gained +10 HP and +5 Armor");
                            Stats[0] += 10;
                            Stats[5] += 5;
                            Enter();
                            valid = false;
                            break;
                        case 2:
                            Console.WriteLine("You walk through the road, you begin to feel fortunate and happy about your decision \nYou start to think what a stupid sign trying to make me climb that stupid mountain");
                            Console.WriteLine("And that´s when you see a huge fissure in the road, you cannot go around it so you begin to freak out, until you see a bridge");
                            Console.WriteLine("As you go through the bridge you hear the voice of what seems to be a troll");
                            Console.WriteLine("The troll then asks you: \nWhats the meaning of life?");
                            answer = Console.ReadLine();
                            if (answer == "42")
                            {
                                Console.WriteLine("You have answered correctly and may cross the bridge");
                            }
                            else
                            {
                                Console.WriteLine("That is not the answer, to cross you will have to defeat me in a battle to the death");
                                fight();
                                Titles("defeat the troll", "Troll Slayer");
                            }
                            valid = false;
                            break;
                        default:
                            Console.WriteLine("Please choose a valid option to continue");
                            Enter();
                            mountainroad();
                            break;
                    }
                }
                else
                {
                    valid = true;
                }
            }
        }
        static void Avalanche()
        {
            int choice;
            bool valid = true;
            int pdice, mdice;
            while (valid)
            {
                Console.WriteLine("As you wander through this molten land, watching lava lakes and huge mountains made of lava, rock and burned corpses");
                Console.WriteLine("You begin to hear something weird, the sound of falling boulders and thats when you see it, a molten rock avalanche");
                Console.WriteLine("You start to panic, while everything happens you face a choice that will decide your fate \n1.To run and see if you can outrun the molten avalanche \n2.To hide in a cave that you saw while walking");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            pdice = Dice(1, 10);
                            Console.WriteLine("You have thrown {0}", pdice);
                            Enter();
                            mdice = Dice(1, 10);
                            Console.WriteLine("Fate has thrown {0}", mdice);
                            if (pdice > mdice)
                            {
                                Titles("outrun the avalanche", "Mighty Runner");
                            }
                            else if (mdice >= pdice)
                            {
                                Console.WriteLine("You where slower than the avalanche and you find yourself trapped below tons of boulders, you see an opening but to get out alive you take of part of your armor");
                                Console.WriteLine("-20 Defense");
                                Stats[5] -= 20;
                                Enter();
                            }
                            break;
                        case 2:
                            Random r = new Random();
                            int happens = r.Next(1, 3);
                            Console.WriteLine("You decide to hide inside of the cave you found earlier on");
                            switch (happens)
                            {
                                case 1:
                                    Console.WriteLine("As you go into the cave you notice something shiny, you get closer and closer to this item and find yourself a chest");
                                    Console.WriteLine("You decide to open the chest");
                                    AfterMobItem();
                                    Enter();
                                    break;
                                case 2:
                                    Console.WriteLine("Seems like you where not the only one that decided to hide in the cave, you are forced to fight what seems to be a mole");
                                    fight();
                                    Enter();
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Please choose a valid option to continue");
                            Avalanche();
                            Enter();
                            break;
                    }
                }
                else
                {
                    valid = true;
                }
            }

        }
        static void Fissure()
        {
            int choice, safe;
            bool valid = true;
            Random r = new Random();
            safe = r.Next(1, 3);

            Console.WriteLine("As you walk calmly accross the molten fields watching lava lakes you stumble into what seems a geyser");
            Console.WriteLine("In a matter of seconds you find yourself falling into a giant fissure, all you can see below is a never ending fall and lava at the very bottom");
            switch (Stats[10])
            {
                case 1://paladin
                    while (valid)
                    {
                        Console.WriteLine("As you fall down you manage to grab a branch, as your life depends on grabbing the branch you need to make a choice");
                        Console.WriteLine("1.To drop part of your armor (-30 armor) \n2.Bet on your strenght and try to climb while wearing your whole armor");
                        valid = int.TryParse(Console.ReadLine(), out choice);
                        if (valid)
                        {
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("You have saved yourself of a horrible death but at a high cost, you have lost 30 armor");
                                    Stats[5] -= 30;
                                    Enter();
                                    valid = false;
                                    break;
                                case 2:
                                    switch (safe)
                                    {
                                        case 1:
                                            Titles("crawl out of the fissure while wearing all of your armor", "Hard Commander");
                                            break;
                                        case 2:
                                            Console.WriteLine("You where too greedy and ended up dying horribly, falling eternally");
                                            FightStats[0] = 0;
                                            Enter();
                                            break;
                                    }
                                    valid = false;
                                    break;
                                default:
                                    Console.WriteLine("Please choose a valid option to continue");
                                    Enter();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please type something usefull");
                            valid = true;
                        }
                    }
                    break;
                case 2://wizard
                    while (valid)
                    {
                        Console.WriteLine("As you fall down you remember a forbidden spell that allows you to teleport but its harder if you are holding too much weight, you face a choice");
                        Console.WriteLine("1.To drop part of your armor (-30 magica resist) \n2.Bet on your wisdom and try to teleport while wearing your whole armor");
                        valid = int.TryParse(Console.ReadLine(), out choice);
                        if (valid)
                        {
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("You have saved yourself of a horrible death but at a high cost, you have lost 30 magic resist");
                                    Stats[3] -= 30;
                                    Enter();
                                    valid = false;
                                    break;
                                case 2:
                                    switch (safe)
                                    {
                                        case 1:
                                            Titles("teleport out of the fissure wearing all of your robes", "Brilliant");
                                            break;
                                        case 2:
                                            Console.WriteLine("You where too greedy and ended up dying horribly, falling eternally");
                                            FightStats[0] = 0;
                                            Enter();
                                            break;
                                    }
                                    valid = false;
                                    break;
                                default:
                                    Console.WriteLine("Please choose a valid option to continue");
                                    Enter();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please type something uselful");
                            valid = true;
                        }
                    }
                    break;
                case 3://warrior
                    while (valid)
                    {
                        Console.WriteLine("As you fall down you manage to grab a branch, as your life depends on grabbing the branch you need to make a choice");
                        Console.WriteLine("1.To drop part of your armor (-30 armor) \n2.Bet on your strenght and try to climb while wearing your whole armor");
                        valid = int.TryParse(Console.ReadLine(), out choice);
                        if (valid)
                        {
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("You have saved yourself of a horrible death but at a high cost, you have lost 30 armor");
                                    Stats[5] -= 30;
                                    Enter();
                                    valid = false;
                                    break;
                                case 2:
                                    switch (safe)
                                    {
                                        case 1:
                                            Titles("crawl out of the fissure while wearing all of your armor", "Unkillable");
                                            break;
                                        case 2:
                                            Console.WriteLine("You where too greedy and ended up dying horribly, falling eternally");
                                            FightStats[0] = 0;
                                            Enter();
                                            break;
                                    }
                                    valid = false;
                                    break;
                                default:
                                    Console.WriteLine("Please choose a valid option to continue");
                                    Enter();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please type something uselful");
                            valid = true;
                        }
                    }
                    break;
                case 4://rogue
                    while (valid)
                    {
                        Console.WriteLine("As you fall down you manage to grab a branch, as your life depends on grabbing the branch you need to make a choice");
                        Console.WriteLine("1.To drop part of your armor (-30 armor) \n2.Bet on your strenght and try to climb while wearing your whole armor");
                        valid = int.TryParse(Console.ReadLine(), out choice);
                        if (valid)
                        {
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("You have saved yourself of a horrible death but at a high cost, you have lost 30 armor");
                                    Stats[5] -= 30;
                                    Enter();
                                    valid = false;
                                    break;
                                case 2:
                                    switch (safe)
                                    {
                                        case 1:
                                            Titles("crawl out of the fissure while wearing all of your armor", "Powerful Modafaka");
                                            break;
                                        case 2:
                                            Console.WriteLine("You where too greedy and ended up dying horribly, falling eternally");
                                            FightStats[0] = 0;
                                            Enter();
                                            break;
                                    }
                                    valid = false;
                                    break;
                                default:
                                    Console.WriteLine("Please choose a valid option to continue");
                                    Enter();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please type something uselful");
                            valid = true;
                        }
                    }
                    break;
                case 5://warlock
                    while (valid)
                    {
                        Console.WriteLine("As you fall down you remember a forbidden spell that allows you to teleport but its harder if you are holding too much weight, you face a choice");
                        Console.WriteLine("1.To drop part of your armor (-30 magica resist) \n2.Bet on your wisdom and try to teleport while wearing your whole armor");
                        valid = int.TryParse(Console.ReadLine(), out choice);
                        if (valid)
                        {
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("You have saved yourself of a horrible death but at a high cost, you have lost 30 magic resist");
                                    Stats[3] -= 30;
                                    Enter();
                                    valid = false;
                                    break;
                                case 2:
                                    switch (safe)
                                    {
                                        case 1:
                                            Titles("teleport out of the fissure while wearing all of your robes", "Dark Mind");
                                            break;
                                        case 2:
                                            Console.WriteLine("You where not that strong and ended up dying horribly, falling eternally");
                                            FightStats[0] = 0;
                                            Enter();
                                            break;
                                    }
                                    valid = false;
                                    break;
                                default:
                                    Console.WriteLine("Please choose a valid option to continue");
                                    Enter();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please type something uselful");
                            valid = true;
                        }
                    }
                    break;
            }
        }
        static void Cyclops()
        {
            int choice;
            string answer;
            bool valid = true;
            while (valid)
            {
                Console.WriteLine("You keep walking through the mountains until you see a lava lake and the only ways around it are not really pleasant");
                Console.WriteLine("1.By following this road you will inevitably face a giant cyclops that is throwing giant molten rocks \n2.By following this road you will have to walk through a thorn filled road");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("I could tell you a whole lot about the giant cyclops and how he throws rocks with the power of 10 thousand suns but lets just get into the fight");
                            fight();
                            Titles("slay the angry cyclops", "Cyclop´s Slayer");
                            valid = false;
                            break;
                        case 2:
                            Console.WriteLine("As you aproach the thorn filled road you see a sign that says:");
                            Console.WriteLine("Riddle me this, riddle me that");
                            Console.WriteLine("As you read the sign, a tex is made out of thorns");
                            Console.WriteLine("How many people do you need to have the odds be in favor (at least 50% chance) of two people having the same birthday?");
                            answer = Console.ReadLine();
                            if (answer == "23")
                            {
                                Titles("solve the riddle", "Riddle Me This Riddle me That");
                                Console.WriteLine("But you dont only get the fancy title, the thorns dissapear and you can walk freely accross the field");
                            }
                            else
                            {
                                Random r = new Random();
                                int fights = r.Next(11);
                                Console.WriteLine("You simply could not answer correctly, you will have to fight your way accros the throny field");
                                for (int i = 0; i <= fights; i++)
                                {
                                    fight();
                                }
                                Console.WriteLine("You finally got out of this nightmerish field, congrats");
                            }
                            valid = false;
                            break;
                        default:
                            Console.WriteLine("Please choose a valid option to continue");
                            Enter();
                            Cyclops();
                            break;
                    }
                }
                else
                {
                    valid = true;
                }
            }
        }
        static void Earthquake()
        {
            Console.WriteLine("Out of nowhere the ground starts to shake, you begin to panic as you notice that you are not in safe grounds, an earthquake in this place might get you burned in lava or completely buried in stone");
            if (Stats[10] == 2 || Stats[10] == 5)
            {
                Console.WriteLine("You have studied magic all for your life and now its the time to make us of your knowledge");
                Console.WriteLine("If you can magically stop the earthquake you will survive");
                fight();
                Titles("defeat Mother Earthquake", "Earthquake Controller");
            }
            else if (Stats[10] == 1 || Stats[10] == 3 || Stats[10] == 4)
            {
                Console.WriteLine("For all of your life you have trained, before you where mad at people for not being able to fight you properly, you have finally found an oponent of your size, nature");
                Console.WriteLine("Prove yourself");
                fight();
                Titles("slay Mother Eartquake", "Motherer Slayer");
            }

        }

        //Death Desert
        static void Oasis()
        {
            int choice;
            bool valid = true;

            while (valid)
            {
                Console.WriteLine("As you walk more into the desert you find yourself more and more dehydrated");
                Console.WriteLine("People have told you that when dehydrated people begin to hallucinate, but not you, you are way too strong and formidable for that \nAnd thats when you spot it in the distance, an OASIS!, you run towards it doubting in the way about your mind health");
                Console.WriteLine("You finally get to it and as you drink water you laugh at your own thoughts for doubting yourself, but then after you finish you notice a crocodile coming towards you \n1.Would you run like a coward \n2.Would you fight the crocodile to the death");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            int pdice = Dice(1, 10);
                            Console.WriteLine("{0} has thrown {1}", userName, pdice);
                            Enter();
                            int mdice = Dice(1, 10);
                            Console.WriteLine("The crocodile has thrown {0}", mdice);
                            if (pdice > mdice)
                            {
                                Console.WriteLine("Congratulations coward you made it out safely");
                                Enter();
                            }
                            else
                            {
                                Console.WriteLine("Better luck next time you lil coward as a punishment you lost 10 points of your defense because the crocodile ate part of it");
                                Stats[5] -= 10;
                                Enter();
                            }
                            valid = false;
                            break;
                        case 2:
                            Console.WriteLine("Like a man you fight the crocodile face to face");
                            fight();
                            Titles("defeat the crocodile and take off its skin", "Crocodile Slayer");
                            Console.WriteLine("By acquiring this title you also get +10 in your defense");
                            Stats[5] += 10;
                            break;
                        default:
                            Console.WriteLine("Please choose a correct option to continue playing, thank you");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please dont make a fool out of yourself and choose a number");
                    valid = true;
                }
            }
        }
        static void Sandstorm()
        {
            bool valid = true;
            int choice;
            Console.WriteLine("All seemed normal until the sandstorm began, a hissing sound was all the warning that you had, now its too late, you only have 2 choices");
            while (valid)
            {
                Console.WriteLine("1.To keep walking in the same direction as the sandstorm \n2.To hide in what seems to be a rocky hole");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            Random r = new Random();
                            int guards = r.Next(20);
                            Console.WriteLine("You keep walking until the sandstorm stoped, but where are you is the main question. \nAs you begin to look around you notice that you have found civilization, a palace above the dead land");
                            Console.WriteLine("You aproach the palace and get invited in by more than 20 beautiful women, you are astonished at their beauty");
                            Console.WriteLine("But it cant be true, and less knowing your luck, as you get comfortable with the women, the sultan comes down, accusing you of raping his wives");
                            Console.WriteLine("You shall defeat all of his guards to get out alive");
                            for (int i = 0; i < guards; i++)
                            {
                                fight();
                            }
                            Console.WriteLine("Congratulations on defeating all of the guards");
                            Titles("anihilate all of the sultan´s force", "Savior of the hoes");
                            break;
                        case 2:
                            Console.WriteLine("To stay outside while a sandstorm roars aint smart, so I will congratulate you on that, the bad news are that this hole aint that safe either");
                            Console.WriteLine("So now I shall tell you what dwells in here, I know bad luck of yours haha \nWhat you can find in here is as old as the desert and its actually a group of bandits, GLHF :)");
                            fight();
                            Titles("kill the centuries old bandits", "Bandits Assasin");
                            break;
                        default:
                            Console.WriteLine("Common you almost get there, now just use a number that actually works like I dont know 1 or 2");
                            Sandstorm();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Dont you get tired of being a smart ass and using number when I specifically tell you to use a numer?, dammit");
                    valid = true;
                }
            }
        }
        static void Library()
        {
            bool valid = true;
            int choice;
            string answer;

            Console.WriteLine("As you walk through the desert you take a glimpse at what appears to be a magnificent building, so as curious as you are you go towards it, its not like you have anything better to do, like I dont know continue with your quest");
            Console.WriteLine("So as you get near you notice its nothing less than a library, you have never seen a library as magnificent and big as this one, there are rock guardians carved into the walls, its beautiful and as you look up you see that it says Alexandria");
            Console.WriteLine("As soon as you finish reading the sign a giant owl comes flying from within the library. \nYou look stupidly at the owl while he talks to you and he says : \nI have a riddle for you, if you can answer you may go in and read for eternity");
            Console.Clear();
            while (valid)
            {
                Console.WriteLine("Complete the next phrase: Demons run when ....");
                Console.WriteLine("1.Try to solve the riddle \n2.Try to outrun the owl and get inside the library");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Go on then, write the complete phrase:");
                            answer = Console.ReadLine();
                            if (answer.ToLower() == "Demons run when a good man goes to war")
                            {
                                Console.WriteLine("Congratulations you are absolutely right you may go in and read my dear");
                                Console.Clear();
                                Console.WriteLine("Once in, you feel the urge to read, to soak in knowledge");
                                if (Stats[10] == 2 || Stats[10] == 5)
                                {
                                    Console.WriteLine("As a master in the arts of sorcery and illusions you sit and meditate while the knowledge of thousands of civilizations flows into your being");
                                    Console.WriteLine("You have gained +40 Magical attack");
                                    Stats[2] += 40;
                                    Titles("read all about sorcery", "Maximus Sorcerus");
                                    Enter();
                                    valid = false;
                                }
                                else if (Stats[10] == 1 || Stats[10] == 3 || Stats[10] == 4)
                                {
                                    Console.WriteLine("As a master in combat you feel nervous, uncomfortable but your wisdom has permitted your entrance. \nThe more you stay in the library the more books you begin to find interesting, theres a whole section for combat information and war strategies, you have found yourself in knowledge paradise.");
                                    Console.WriteLine("You have gained +25 Attack");
                                    Stats[4] += 25;
                                    Enter();
                                    valid = false;
                                }
                            }
                            else
                            {
                                Random r = new Random();
                                int guards = r.Next(20);
                                Console.WriteLine("You are right, you have made it in");
                                Enter();
                                Console.WriteLine("LOL NOPE");
                                Enter();
                                for (int i = 0; i < guards; i++)
                                {
                                    fight();
                                }
                                Console.WriteLine("Your violence is unmesurable, but you have proven yourself worthy, you may come in");
                                Enter();
                                Console.WriteLine("LOL NOPE");
                                Console.WriteLine("Just keep walking you will not come in");
                                valid = false;
                            }
                            break;
                        case 2:
                            Random ran = new Random();
                            int guard = ran.Next(20);
                            Console.WriteLine("So you really thought that you could outrun me, the protector of the knowledge of all mankind? hahahaha \nYou will have to defeat the guards for me to consider your entrance");
                            for (int i = 0; i < guard; i++)
                            {
                                fight();
                            }
                            Console.WriteLine("Your violence is unmesurable, but you have proven yourself worthy, you may come in");
                            Enter();
                            Console.WriteLine("LOL NOPE");
                            Console.WriteLine("Just keep walking you will not come in");
                            valid = false;
                            break;
                        default:
                            Console.WriteLine("Can you please stop wasting my time, just choose a correct answer");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please do yourself a favor and choose a valid option");
                    valid = true;
                }
            }

        }
        static void nightdesert()
        {
            bool valid = true;
            int choice;
            while (valid)
            {
                Console.WriteLine("Its getting dark in here and while at first it wasnt really a problem for you, you notice something strange and scarry");
                Console.WriteLine("As soon as the darkness touches the sand it freezes and everything dies \nSo here you are, scared like a little brat with no clue of what to do until you see something even stranger \n1.There are spots in the desert that are red hot (warm if it wasnt self explanatory....)");
                Console.WriteLine("2.But Knowing that you are such a badass and that those hot spots dont look really safe you can choose to stay and see if you can withstand the cold of the night");
                valid = int.TryParse(Console.ReadLine(), out choice);
                if (valid)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("And yeah the hot spots couldnt be less legit, it was obviously a depredator mechanism duh... \nAnyway now you are here in what seems to be the lair of a giant ass worm (Yeah like the one in *wars, sue me)");
                            Console.WriteLine("You will have to fight it or be eaten");
                            Enter();
                            fight();
                            Titles("kill the huge worm", "Worm Eater");
                            Enter();
                            valid = false;
                            break;
                        case 2:
                            Random r = new Random();
                            int night = r.Next(3);
                            switch (night)
                            {
                                case 1:
                                    Titles("survive the cold of the night", "Cold? I could be naked right now");
                                    Console.WriteLine("By achieving this you also got a boost in your magical defense (+30)");
                                    Stats[3] += 30;
                                    Enter();
                                    valid = false;
                                    break;
                                case 2:
                                    Console.WriteLine("Sorry you couldn´t survive, I told you so..... you died");
                                    FightStats[0] = 0;
                                    Enter();
                                    valid = false;
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Please for Gods sake just choose a valid option !!");
                            Enter();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Its like you never learn right? pff ....");
                    Enter();
                }
            }
        }
        static void desertGoblin()
        {
            Console.WriteLine("The heat of the desert is only as hot as the heat inside my pants. \nIn the wasted barrens of the desert you stare into the horizon... \nAnd when a shadow moves towards you there you see him... Souveihcsim... the desert goblin. \nRiddle me this, riddle me that... \nOr as some do... Google me this... Google me that. \nSouveihcsim usually says nonsense, and it is part of his charm. \nBut, if you answer to his riddle, and you get the answer right, a reward you'll have tonight.");
            Console.WriteLine("The goblin whispers into your ear: I am Souveihcsim, the most...");
            Console.Write("What is he the most? ");
            if (Console.ReadLine().ToLower() == "mischievous")
            {
                Console.WriteLine("Outstanding, you have been keen enough to solve his puzzle. \nFor it, you will be rewarded.");
                AfterMobItem();
            }
            else
            {
                Console.WriteLine("Wasn't it obvious? \nToo bad, you missed... \nAnd Souveihcsim will now claim his prize. \nHe will take 25 points off your Stats.");
                bool runCycle;
                do
                {
                    runCycle = false;
                    Console.WriteLine("What do you want to do? \n1.Fight him, do no let him have your points \n2.Let him take them");
                    int answer;
                    bool valid = int.TryParse(Console.ReadLine(), out answer);
                    if (valid)
                    {
                        switch (answer)
                        {
                            case 1:
                                int enemyDice = Dice(1, 1000);
                                Console.WriteLine("Souveihcsim does not fight. But he does like to throw his 1000-sided dice. \nHe got {0}", enemyDice);
                                Console.WriteLine("Your dice only has 10 sides, but Souveihcsim is mercyful. So whatever number you get, it will be multiplied by one hundred. \nPress any key when you are ready to throw your dice.");
                                Console.ReadLine();
                                int myDice = 100 * Dice(1, 10);
                                Console.WriteLine("You got a {0}.", myDice);
                                if (enemyDice >= myDice)
                                {
                                    Console.WriteLine("Your luck wasn't as good as that of Souveihcsim. \nHe will now drain your Stat Points.");
                                    Console.WriteLine("25 Stat Points will be taken randomly from a specific stat of yours.");
                                    int whatPoints = Dice(0, 7);
                                    Console.WriteLine("You have lost 25 {0} points.", StatNames[whatPoints]);
                                    Stats[whatPoints] -= 25;
                                }
                                else
                                {
                                    Console.WriteLine("You outwitted Souveihcsim. \nYou are free to run, before he tries another scheme on  you.");
                                }
                                break;
                            case 2:
                                Console.WriteLine("25 Stat Points will be taken randomly from a specific stat of yours.");
                                int whatPoints2 = Dice(0, 7);
                                Console.WriteLine("You have lost 25 {0} points.", StatNames[whatPoints2]);
                                Stats[whatPoints2] -= 25;
                                break;
                            default:
                                Console.WriteLine("You did not choose a valid option. \nPlease press any key and try again.");
                                Enter();
                                runCycle = true;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You did not choose a valid option. \nPlease press any key and try again.");
                        Enter();
                        runCycle = true;
                    }
                } while (runCycle);
            }
        }

        //City of the Death
        static void warlockLair()
        {
            bool runCycle;
            int answer;
            bool valid;
            Console.WriteLine("As you walk through the City of the Death you found this old temple. \nYou enter it, and as soon as you enter... \nThe door behind you closes. \nYou are now trapped inside the Warlock Lair. \nAs you explore it you find this old library. \nYou turn to your right, then to your left, and this wall which supports this bookshelf seems to be endless. \nYou aproach one specific book.");
            do
            {
                runCycle = false;
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Pick up the book. \n2.Get out of here as soon as possible.");
                valid = int.TryParse(Console.ReadLine(), out answer);
                if (valid)
                {
                    switch (answer)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("By touching the book you have summoned the old Skeleton Warlock.");
                            if (Stats[10] == 5)
                            {
                                Console.WriteLine("{0}, since both of you are Warlocks you are given the choice of making an alliance with the old Skeleton.", userName);
                                bool runLittleCycle;
                                do
                                {
                                    runLittleCycle = false;
                                    Console.WriteLine("Do you wish to do such a pact? \n1.Yes \n.2No");
                                    int alliance;
                                    valid = int.TryParse(Console.ReadLine(), out alliance);
                                    if (valid)
                                    {
                                        switch (alliance)
                                        {
                                            case 1:
                                                Console.WriteLine("Now that you have chosen to make a pact with the Skeleton Warlock, he will drain half of your HP. \nIn return you will get his forbidden book: The Dark texts of Wrezllt. \nThis will increase your Magic Powers in an unimaginable way.");
                                                Console.WriteLine("Leyend has it that this texts narrate the story of a human who shut down the sun.");
                                                Stats[0] /= 2;
                                                Stats[2] *= 2;
                                                Console.WriteLine("Now you have an HP of {0} and an MP of {1}", Stats[0], Stats[2]);
                                                Titles("get the Dark texts of Wrezllt", "Lord Veto, Master of all that is Forbidden");
                                                break;
                                            case 2:
                                                Console.WriteLine("Your act has been dimmed as treason by the Skeleton Warlock. \nNow he wants to fight you, because, as he put it, you are unworthy.");
                                                Random r = new Random();
                                                for (int i = 0; i <= r.Next(1, 6); i++)
                                                {
                                                    fight();//make it specific for skeletons
                                                }
                                                break;
                                            default:
                                                Console.WriteLine("You did not choose a valid option. \nDo not make the Warlock go mad. \nSo try again and please choose the right option.");
                                                runLittleCycle = true;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You did not choose a valid option. \nDo not make the Warlock go mad. \nSo try again and please choose the right option.");
                                        runLittleCycle = true;
                                    }
                                } while (runLittleCycle);
                            }
                            break;
                        case 2:
                            bool runCase2;
                            Console.WriteLine("Where do you want to move?");
                            do
                            {
                                runCase2 = false;
                                Console.WriteLine("1.To the right. \n2.To the left. \n3.Keep walking.");
                                int move;
                                valid = int.TryParse(Console.ReadLine(), out move);
                                if (valid)
                                {
                                    switch (move)
                                    {
                                        case 1:
                                            Console.WriteLine("You have found a passage to exit the lair. \nYou are now exiting this old temple.");
                                            break;
                                        case 2:
                                            Console.WriteLine("You cannot move any further to the left. \nYou must choose again.");
                                            Enter();
                                            runCase2 = true;
                                            break;
                                        case 3:
                                            Console.WriteLine("You stepped on a vase. The noise has waken up the Skeleton Warlock. \nYou have enraged him for your instrusion. \nYou must fight him.");
                                            Random r = new Random();
                                            for (int i = 0; i <= r.Next(1, 6); i++)
                                            {
                                                fight();//make it specific for skeletons
                                            }
                                            break;
                                        default:
                                            Console.WriteLine("You did not choose a valid option. \nPlease press any key and try again.");
                                            Enter();
                                            runCase2 = true;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You did not choose a valid option. \nPlease press any key and try again.");
                                    Enter();
                                    runCase2 = true;
                                }
                            } while (runCase2);
                            break;
                        default:
                            Console.WriteLine("You didn't choose a valid option from the menu. \nPlease press any key try again.");
                            runCycle = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You didn't choose a valid option from the menu. \nPlease press any key try again.");
                    runCycle = true;
                    break;
                }
            } while (runCycle);
        }
        static void dogs()
        {
            Console.WriteLine("A pack of skeleton dogs is running down a hill... \nTowards you!");
            bool runCycle;
            do
            {
                runCycle = false;
                int answer;
                bool valid;
                Console.WriteLine("What do you want to do? \n1.Run \n2.Hide");
                valid = int.TryParse(Console.ReadLine(), out answer);
                if (valid)
                {
                    switch (answer)
                    {
                        case 1://Run
                            int enemyDice = Dice(1, 10);
                            Console.WriteLine("Throw your dice. If your number is bigger than {0} you will outrun the dogs.", enemyDice);
                            Console.WriteLine("Press any key when you are ready to throw your dice.");
                            Console.ReadLine();
                            int myDice = Dice(1, 10);
                            if (enemyDice >= myDice)//user loses
                            {
                                Console.WriteLine("This hounds from hell are way faster than you. \nYou never stood a chance.");
                                Console.WriteLine("You must fight them.");
                                Random r = new Random();
                                for (int i = 0; i <= r.Next(1, 6); i++)
                                {
                                    fight();//dogs
                                }
                            }
                            else
                            {
                                Console.WriteLine("Fortunately for you, you were able to outrun the pack of dogs.");
                            }
                            break;
                        case 2://Hide
                            enemyDice = Dice(1, 10);
                            Console.WriteLine("Throw your dice. If your number is bigger than {0} you will be hidden from the dogs. \nIf not, they will find you.", enemyDice);
                            Console.WriteLine("Press any key when you are ready to throw your dice.");
                            Console.ReadLine();
                            myDice = Dice(1, 10);
                            if (enemyDice >= myDice)//user loses
                            {
                                Console.WriteLine("This hounds from hell sniffed you. They can sense your fear. \nYou never really stood a chance.");
                                Console.WriteLine("You must fight them.");
                                Random r = new Random();
                                for (int i = 0; i <= r.Next(1, 6); i++)
                                {
                                    fight();//dogs
                                }
                            }
                            else
                            {
                                Console.WriteLine("Fortunately for you, you were able to hide from the dogs.");
                            }
                            break;
                        default:
                            Console.WriteLine("You did not choose a valid option. \nPlease press any key and try again.");
                            Enter();
                            runCycle = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You did not choose a valid option. \nPlease press any key and try again.");
                    Enter();
                    runCycle = true;
                }
            } while (runCycle);
        }
        static void skeletonArmy()
        {
            Console.WriteLine("The City of the Death... \nIt is indeed full of terrors... full of dangers.");
            bool runCycle;
            do
            {
                runCycle = false;
                Console.WriteLine("What do you want to do? \n1.Keep walking forward. \n2.Go through an alley to your right. \n3.Go through an alley to your left.");
                int move;
                bool valid = int.TryParse(Console.ReadLine(), out move);
                if (valid)
                {
                    switch (move)
                    {
                        case 1:
                            bool Case1Cycle;
                            do
                            {
                                Case1Cycle = false;
                                Console.WriteLine("If you keep walking forward you will face an army of skeletons. \nAre you sure you want to do that? \n1.Yes \n2.No");
                                int answer;
                                valid = int.TryParse(Console.ReadLine(), out answer);
                                if (valid)
                                {
                                    switch (answer)
                                    {
                                        case 1:
                                            Console.WriteLine("Prepare for the uncoming storm!");
                                            Random numberOfSkeletons = new Random();
                                            for (int i = 0; i < numberOfSkeletons.Next(1, 6); i++)
                                            {
                                                fight();//skeletons
                                            }
                                            Titles("kill the army of skeletons", "Slayer of the Undead");
                                            break;
                                        case 2:
                                            Console.WriteLine("Only if you are faster than the skeletons you will be able to escape from them.");
                                            int enemyDice = Dice(1, 10);
                                            Console.WriteLine("Roll your dice. If your number is bigger than {0} you will be able to escape. \nPress any key when you are ready to meet your fate.", enemyDice);
                                            Console.ReadLine();
                                            int myDice = Dice(1, 10);
                                            Console.WriteLine("You threw a {0}", myDice);
                                            if (enemyDice > myDice)//user loses
                                            {
                                                Console.WriteLine("You were unable to escape from the skeletons.");
                                                Console.WriteLine("Prepare for the uncoming storm!");
                                                numberOfSkeletons = new Random();
                                                for (int i = 0; i < numberOfSkeletons.Next(1, 6); i++)
                                                {
                                                    fight();//skeletons
                                                }
                                                Titles("kill the army of skeletons", "Slayer of the Undead");
                                            }
                                            else
                                            {
                                                Console.WriteLine("How lucky for you. You have fled the menace.");
                                            }
                                            break;
                                        default:
                                            Console.WriteLine("You did not choose a valid option. Please press any key and try again.");
                                            Case1Cycle = true;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You did not choose a valid option. Please press any key and try again.");
                                    Case1Cycle = true;
                                }
                            } while (Case1Cycle);
                            break;
                        case 2:
                            Console.WriteLine("You can't go there. There is an army of Skeletons approaching. \nYou have to choose again.");
                            runCycle = true;
                            break;
                        case 3:
                            Console.WriteLine("Good... Good... You went through the only safe path in this city. \nIf you had chosen otherwise you would be fighting an army of skeletons right now.");
                            break;
                        default:
                            Console.WriteLine("You did not choose a valid option. Please press any key and try again.");
                            runCycle = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You did not choose a valid option. Please press any key and try again.");
                    runCycle = true;
                }
            } while (runCycle);
        }
        static void gargoyle()
        {
            Console.WriteLine("Now... what do you wish to do? \n1.Turn around and have a look \n2.Keep walking");
            int answer;
            bool valid = int.TryParse(Console.ReadLine(), out answer);
            if (valid)
            {
                switch (answer)
                {
                    case 1:
                        Console.WriteLine("Everything is completely dark. You can only see your own hands.");
                        gargoyle();
                        break;
                    case 2://only option that doesn't use recursion
                        Console.WriteLine("You have stepped into a cemetery... but, why would there be a cemetery in the City of the Death?");
                        Console.WriteLine("There are many aisles of tombs. Where do you want to move? \n1.To the right \n2.To the left \n3.Forward");
                        Console.ReadLine();
                        Console.WriteLine("There is a gargoyle to your far left... \nNow, where do you want to move? \n1.To the right \n2.To the left \n3.Forward \n4.Backwards");
                        Console.WriteLine("There is another gargoyle near you... \nAnd the one you had seen before, it is now closer to you... \nYou are sorrounded by gargoyles.");
                        bool runLittleCycle;
                        do
                        {
                            runLittleCycle = false;
                            Console.WriteLine("What do you want to do? \n1.Fight the gargoyles \n2.Flee from the gargoyles");
                            int choice;
                            valid = int.TryParse(Console.ReadLine(), out choice);
                            if (valid)
                            {
                                switch (choice)
                                {
                                    case 1://Fight
                                        Console.WriteLine("The Tanrilar have decided to intervene in your battle. \nThey find it amusing, so you must throw your dice and according to your number you will have to fight the same amount of gargoyles.");
                                        Console.WriteLine("Press any key when you are ready to throw your dice.");
                                        Console.ReadLine();
                                        int number = Dice(1, 7);
                                        Console.WriteLine("You will have to fight {0} gargoyles. \nBrace yourself {1}.", number, userName);
                                        for (int i = 0; i <= number; i++)
                                        {
                                            fight();//gargoyle
                                        }
                                        break;
                                    case 2://Flee
                                        Console.WriteLine("The gargoyles are really fast. \nAnd there are about a hundred of them. \nThrow your dice {0}, and for each unit you get, a gargoyle will be subtracted from the horde.");
                                        Console.WriteLine("Press any key when you are ready to throw your dice.");
                                        Console.ReadLine();
                                        number = Dice(80, 100);
                                        Console.WriteLine("You will have to run from {0} gargoyles. \nBrace yourself {1}.", 100 - number, userName);
                                        int smallcounter = 1;
                                        for (int i = 0; i <= 100 - number; i++)
                                        {
                                            if (smallcounter % 3 == 0)
                                            {
                                                Console.WriteLine("Now, throw your dice to see if you can outrun the gargoyle to your left.");
                                                int enemyDice = Dice(1, 10);
                                                Console.WriteLine("Press any key when you want to throw your dice.");
                                                Console.ReadLine();
                                                int myDice = Dice(1, 10);
                                                Console.WriteLine("The gargoyle threw {0}... you threw {1}", enemyDice, myDice);
                                                if (enemyDice >= myDice)//user loses
                                                {
                                                    Console.WriteLine("You were not faster than the gargoyle. \nYou will have to fight it.");
                                                    fight();//gargoyle
                                                }
                                                else
                                                {
                                                    Console.WriteLine("{0}... You have been really fast. Good for you.", userName);
                                                }
                                            }
                                            else if (smallcounter % 4 == 0)
                                            {
                                                Console.WriteLine("Now, throw your dice to see if you can outrun the gargoyle to your right.");
                                                int enemyDice = Dice(1, 10);
                                                Console.WriteLine("Press any key when you want to throw your dice.");
                                                Console.ReadLine();
                                                int myDice = Dice(1, 10);
                                                Console.WriteLine("The gargoyle threw {0}... you threw {1}", enemyDice, myDice);
                                                if (enemyDice >= myDice)//user loses
                                                {
                                                    Console.WriteLine("You were not faster than the gargoyle. \nYou will have to fight it.");
                                                    fight();//gargoyle
                                                }
                                                else
                                                {
                                                    Console.WriteLine("{0}... You have been really fast. Good for you.", userName);
                                                }
                                            }
                                            else if (smallcounter % 5 == 0)
                                            {
                                                Console.WriteLine("Now, throw your dice to see if you can outrun the gargoyle behind you.");
                                                int enemyDice = Dice(1, 10);
                                                Console.WriteLine("Press any key when you want to throw your dice.");
                                                Console.ReadLine();
                                                int myDice = Dice(1, 10);
                                                Console.WriteLine("The gargoyle threw {0}... you threw {1}", enemyDice, myDice);
                                                if (enemyDice >= myDice)//user loses
                                                {
                                                    Console.WriteLine("You were not faster than the gargoyle. \nYou will have to fight it.");
                                                    fight();//gargoyle
                                                }
                                                else
                                                {
                                                    Console.WriteLine("{0}... You have been really fast. Good for you.", userName);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("{0}, throw your dice. See if you are faster than the gargoyle.");
                                                int enemyDice = Dice(1, 10);
                                                Console.WriteLine("Press any key when you want to throw your dice.");
                                                Console.ReadLine();
                                                int myDice = Dice(1, 10);
                                                Console.WriteLine("The gargoyle threw {0}... you threw {1}", enemyDice, myDice);
                                                if (enemyDice >= myDice)//user loses
                                                {
                                                    Console.WriteLine("You were not faster than the gargoyle. \nYou will have to fight it.");
                                                    fight();//gargoyle
                                                }
                                                else
                                                {
                                                    Console.WriteLine("{0}... You have been really fast. Good for you.", userName);
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("You did not choose a valid option from the menu. \nPlease press any key and try again.");
                                        Enter();
                                        runLittleCycle = true;
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You did not choose a valid option from the menu. \nPlease press any key and try again.");
                                Enter();
                                runLittleCycle = true;
                            }
                        } while (runLittleCycle);
                        break;
                    default:
                        Console.WriteLine("You did not choose a valid option. \nPlease try again. \nPress any key to continue.");
                        gargoyle();
                        break;
                }
            }
            else
            {
                Console.WriteLine("You may not do this. You must have chosen an invalid option. \nPlease press any key and try again. \nBut be fast, danger lurks in the shadows.");
                gargoyle();
            }
        }
        static void zombieRitual()
        {
            Console.WriteLine("Some ancient texts talk about the possibility of becoming undead. \nAs you may say, the City of the Death is basically that...");
            Console.WriteLine("A group of skeletons is ambushing you! \nThey have taken you to their temple. They are tying you to a stone bed.");
            bool runCycle;
            do
            {
                runCycle = false;
                Console.WriteLine("What will you do? \n1.Try to escape \n2.Chill and see what happens");
                int answer;
                bool valid = int.TryParse(Console.ReadLine(), out answer);
                if (valid)
                {
                    switch (answer)
                    {
                        case 1:
                            int enemyDice = Dice(1, 20);
                            Console.WriteLine("Throw your dice. If your number is bigger than {0} you may escape.", enemyDice);
                            Console.WriteLine("Throw your dice once you are ready.");
                            Console.ReadLine();
                            int myDice = Dice(1, 20);
                            Console.WriteLine("You threw a {0}.", myDice);
                            if (enemyDice >= myDice)
                            {
                                Console.WriteLine("You may not escape. You are too well tied.");
                                Console.WriteLine("The skeletons will perfom a ritual on you. \nThey will try to transform you into an undeath being. \nIf you are like enough, you will endure the process. \nOtherwise, you will die.");
                                enemyDice = Dice(1, 10);
                                Console.WriteLine("Throw your dice whenever you are ready.");
                                Console.ReadLine();
                                myDice = Dice(1, 10);
                                if (enemyDice >= myDice)
                                {
                                    Console.WriteLine("You were too weak to resist. You have died {0}", userName);
                                    Stats[0] = 0;
                                }
                                else
                                {
                                    Console.WriteLine("You are being anointed by the skeletons. \nYour flesh is melting into your bones and your are raising among the fallen warrior. \nYou have become undead.");
                                    Titles("defeat dead", "Demise, the forsaken Ruler");
                                    Stats[0] *= 2;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You have managed to escape from this ritual.");
                            }
                            break;
                        case 2:
                            Console.WriteLine("The skeletons will perfom a ritual on you. \nThey will try to transform you into an undeath being. \nIf you are like enough, you will endure the process. \nOtherwise, you will die.");
                            enemyDice = Dice(1, 10);
                            Console.WriteLine("Throw your dice whenever you are ready.");
                            Console.ReadLine();
                            myDice = Dice(1, 10);
                            if (enemyDice >= myDice)
                            {
                                Console.WriteLine("You were too weak to resist. You have died {0}", userName);
                                Stats[0] = 0;
                            }
                            else
                            {
                                Console.WriteLine("You are being anointed by the skeletons. \nYour flesh is melting into your bones and your are raising among the fallen warrior. \nYou have become undead.");
                                Titles("defeat dead", "Demise, the forsaken Ruler");
                                Stats[0] *= 2;
                            }
                            break;
                        default:
                            Console.WriteLine("You didn't choose a valid option. \nPlease press any key and try again.");
                            Enter();
                            runCycle = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You didn't choose a valid option. \nPlease press any key and try again.");
                    Enter();
                    runCycle = true;
                }
            } while (runCycle);
        }


        //Area choosing method
        static int ChooseArea()//couldn't it be a void?
        {
            int possiblearea;
            Random r = new Random();
            possiblearea = r.Next(5);
            bool repeating = false;

            for (int i = 0; i < 4; i++)
            {
                if (Area == areas[i])
                {
                    repeating = true;
                }
            }
            if (repeating == false)
            {
                switch (possiblearea)
                {
                    case 1:
                        areas[areacounter] = 1;
                        break;
                    case 2:
                        areas[areacounter] = 2;
                        break;
                    case 3:
                        areas[areacounter] = 3;
                        break;
                    case 4:
                        areas[areacounter] = 4;
                        break;
                }
                areacounter++;
            }
            else
            {
                ChooseArea();
            }
            return Area = possiblearea;
        }

        static void PrintMyCurrentStats()
        {
            Console.WriteLine("{0, 65}", "HP = " + FightStats[0] + "/" + Stats[0]);
            Console.WriteLine("{0, 65}", "MP = " + FightStats[1] + "/" + Stats[1]);
            Console.WriteLine("{0, 65}", "AP = " + FightStats[2]);
            Console.WriteLine("{0, 65}", "MR = " + FightStats[3]);
            Console.WriteLine("{0, 65}", "ATK = " + FightStats[4]);
            Console.WriteLine("{0, 65}", "DEF = " + FightStats[5]);
            Console.WriteLine("{0, 65}", "LUCK = " + FightStats[6]);
            Console.WriteLine("{0, 65}", "XP = " + Stats[7]);
            Console.WriteLine("{0, 65}", "LVL = " + Stats[8]);
        }

        static void Titles(string happened, string name)
        {
            string title;
            bool titlevalid = true;
            while (titlevalid)
            {
                Console.WriteLine("You managed to {0} achieving the title {1}, would you like to use it (Y/N)?", happened, name);
                title = Console.ReadLine();
                if (title.ToLower() == "y" || title.ToLower() == "yes")
                {
                    userName = string.Concat(name, " ", initialuserName);
                    Console.WriteLine("From now on you shall be named {0}", userName);
                    titlevalid = false;
                }
                else if (title.ToLower() == "n" || title.ToLower() == "no")
                {
                    Console.WriteLine("You have chosen to keep your name {0}", userName);
                    titlevalid = false;
                }
            }
        }

    }
}

