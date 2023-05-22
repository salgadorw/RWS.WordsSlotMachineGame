using RWS.WordsSlotMachine.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RWS.WordsSlotMachine.Infrastructure
{
    public class GameDataRepository : IGameDataRepository
    {
        private const string BASE_PATH = "\\Infrastructure\\Resources\\";
        private const string WORDS_PATH = $"{BASE_PATH}american-english-large.txt";
        private const string REELS_PATH = $"{BASE_PATH}\\reels.txt";
        private const string SCORES_PATH = $"{BASE_PATH}\\scores.txt";

        private List<string> words;
        private List<string> reels;
        private List<string> scores;

        private static List<string> ReadFile(string path)
        {
            return File.ReadAllLines($"{Directory.GetCurrentDirectory()}{path}").Select(s => s.ToUpper()).ToList();
        }

        public WordsTree GetWords()
        {
            words ??= ReadFile(WORDS_PATH);
            var result = new WordsTree();
            words.ForEach(word => { result.Insert(word); });

            return result;
        }
        public Dictionary<char, int> GetScores()
        {
            scores ??= ReadFile(SCORES_PATH);
            var result = new Dictionary<char, int>();
            scores.ForEach(e => result.Add(e.First(), int.Parse(e.Last().ToString())));

            return result;
        }

        public List<string> GetReels()
        {
            reels ??= ReadFile(REELS_PATH);

            return reels;
        }
    }
}
