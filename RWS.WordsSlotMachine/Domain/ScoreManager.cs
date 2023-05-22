using System.Collections.Generic;
using System.Linq;

namespace RWS.WordsSlotMachine.Domain
{
    public class ScoreManager : IScoreManager
    {
        private int score = 0;
        private readonly Dictionary<char, int> scores;
        public ScoreManager(Dictionary<char, int> scores)
        {
            this.scores = scores;
        }

        public void SumWordScore(string scoreLetter)
        {
            scoreLetter.ToList().ForEach(l => score += (scores.ContainsKey(l) ? scores[l] : 0));
        }

        public int GetScore()
        {
            return score;
        }

        public string GetLettersScoreData()
        {
            return string.Join(",", scores.Select(s => $"{s.Key} {s.Value}"));
        }
    }
}
