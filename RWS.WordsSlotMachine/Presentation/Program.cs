using System;
using System.Linq;
using RWS.WordsSlotMachine.Application;
using RWS.WordsSlotMachine.Domain;
using RWS.WordsSlotMachine.Infrastructure;

namespace RWS.WordsSlotMachine.Presentation
{
    public static class Program
    {
        private static IGamePlayMachineService gamePlayMachine;
        private static bool playing = true;
        static void Main(string[] args)
        {           
            IGameDataRepository gameData = new GameDataRepository();
            IScoreManager scoreManager = new ScoreManager(gameData.GetScores());            
            IReelsManager reelsManager = new ReelsManager(gameData.GetReels());
            IWordsTree trie = new WordsTree();
            IWordsTree wordsTree = gameData.GetWords();
            gamePlayMachine = new GamePlayMachineService(reelsManager, scoreManager, wordsTree, trie);
           
            while (playing)
            {                
                PrintGame(gamePlayMachine.GetGameDisplayData());                
            }
        }

        public static void PrintGame(GameDataDisplayInfoDTO gameDisplayData)
        {            
            Console.Clear();
            Console.WriteLine($"Letters Score:{gameDisplayData.ScoresLettersInfo}");
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%$$$WordsSlotMachineGame$$$%%%%%%%%%%%%%%%%%%%%%%%%%");
            Console.WriteLine($"                             Your $CORE:{gameDisplayData.GameScoreInfo}");
            Console.WriteLine($"                         ___________________");
            Console.WriteLine("                        | -- -- -- -- -- -- |");
            Console.WriteLine($"                        |{String.Join(" ",gameDisplayData.ReelsDisplayInfo.ToCharArray().Select(s=> " "+s))}  |");
            Console.WriteLine("                        | -- -- -- -- -- -- |");
            Console.WriteLine("                         ------------------- ");
            Console.WriteLine($"                             Your $CORE:{gameDisplayData.GameScoreInfo}");
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%$$$$$$$$$$$$$$$$$$$$$$$$$$%%%%%%%%%%%%%%%%%%%%%%%%%");
            Console.Write("Type the word to try and good luck:");
            
            string input = Console.ReadLine();
            var gamePlayStatus = gamePlayMachine.PlayGame(input);

            if (!gamePlayStatus.PlayStatus) {
                Console.WriteLine(gamePlayStatus.PlayStatusMessage);
                Console.WriteLine("Press Enter to continue or N to end the game;");
                if (Console.ReadLine() == "N")
                    playing = false;
            }
        }
    }
}